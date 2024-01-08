﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SuperBotManagerBase.Configuration;
using SuperBotManagerBase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.RabbitMq.Core
{
    public interface IQueueConsumerHandler<TMessageConsumer, TQueueMessage>
        where TMessageConsumer : IQueueConsumer<TQueueMessage>
        where TQueueMessage : class, IQueueMessage
    {
        public void RegisterQueueConsumer();
        public void CancelQueueConsumer();
    }
    public class QueueConsumerHandler<TMessageConsumer, TQueueMessage> : IQueueConsumerHandler<TMessageConsumer, TQueueMessage>
        where TMessageConsumer : IQueueConsumer<TQueueMessage>
        where TQueueMessage : class, IQueueMessage
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<QueueConsumerHandler<TMessageConsumer, TQueueMessage>> _logger;
        private string _queueName;
        private IModel _consumerRegistrationChannel;
        private string _consumerTag;
        private readonly string _consumerName;

        public QueueConsumerHandler(IServiceProvider serviceProvider, ILogger<QueueConsumerHandler<TMessageConsumer, TQueueMessage>> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

            _consumerName = typeof(TMessageConsumer).Name;
        }

        public void RegisterQueueConsumer()
        {
            var scope = _serviceProvider.CreateScope();

            // Request a channel for registering the Consumer for this Queue
            var queueProvider = scope.ServiceProvider.GetRequiredService<IRabbitmqQueueProvider<TQueueMessage>>();
            _queueName = queueProvider.QueueName;
            _consumerRegistrationChannel = queueProvider.GetChannel();
            var consumer = new AsyncEventingBasicConsumer(_consumerRegistrationChannel);

            // Register the trigger
            consumer.Received += HandleMessage;
            try
            {
                _consumerTag = _consumerRegistrationChannel.BasicConsume(_queueName, false, consumer);
            }
            catch (Exception ex)
            {
                var exMsg = $"BasicConsume failed for Queue '{_queueName}'";
                _logger.LogError(ex, exMsg);
                throw new QueueingException(exMsg);
            }
        }

        public void CancelQueueConsumer()
        {
            try
            {
                _consumerRegistrationChannel.BasicCancel(_consumerTag);
            }
            catch (Exception ex)
            {
                var message = $"Error canceling QueueConsumer registration for {_consumerName}";
                _logger.LogError(message, ex);
                throw new QueueingException(message, ex);
            }
        }

        private async Task HandleMessage(object ch, BasicDeliverEventArgs ea)
        {
            // Create a new scope for handling the consumption of the queue message
            var consumerScope = _serviceProvider.CreateScope();

            // This is the channel on which the Queue message is delivered to the consumer
            var consumingChannel = ((AsyncEventingBasicConsumer)ch).Model;

            IModel producingChannel = null;
            try
            {
                // Within this processing scope, we will open a new channel that will handle all messages produced within this consumer/scope.
                // This is neccessairy to be able to commit them as a transaction
                producingChannel = consumerScope.ServiceProvider.GetRequiredService<IRabbitmqChannelProvider>()
                    .GetChannel();

                // Serialize the message
                var message = DeserializeMessage(ea.Body.ToArray());

                // Enable transaction mode
                producingChannel.TxSelect();

                // Request an instance of the consumer from the Service Provider
                var consumerInstance = consumerScope.ServiceProvider.GetRequiredService<TMessageConsumer>();

                // Trigger the consumer to start processing the message
                await consumerInstance.ConsumeAsync(message);

                // Ensure both channels are open before committing
                if (producingChannel.IsClosed || consumingChannel.IsClosed)
                {
                    throw new QueueingException("A channel is closed during processing");
                }

                // Commit the transaction of any messages produced within this consumer scope
                producingChannel.TxCommit();

                // Acknowledge successfull handling of the message
                consumingChannel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                var msg = $"Cannot handle consumption of a {_queueName} by {_consumerName}'";
                _logger.LogError(ex, msg);
                RejectMessage(ea.DeliveryTag, consumingChannel, producingChannel);
            }
            finally
            {
                // Dispose the scope which ensures that all Channels that are created within the consumption process will be disposed
                consumerScope.Dispose();
            }
        }

        private void RejectMessage(ulong deliveryTag, IModel consumeChannel, IModel scopeChannel)
        {
            try
            {
                // The consumption process could fail before the scope channel is created
                if (scopeChannel != null)
                {
                    // Rollback any massages within the transaction
                    scopeChannel.TxRollback();
                }

                // Reject the message on the consumption channel
                consumeChannel.BasicReject(deliveryTag, false);
            }
            catch (Exception bex)
            {
                var bexMsg =
                    $"BasicReject failed";
                _logger.LogCritical(bex, bexMsg);
            }
        }

        private static TQueueMessage DeserializeMessage(byte[] message)
        {
            var stringMessage = Encoding.UTF8.GetString(message);
            return JsonConvert.DeserializeObject<TQueueMessage>(stringMessage);
        }
    }


}
