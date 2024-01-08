using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.Services
{
    public interface IRabbitmqConnectionProvider
    {
        IConnection GetConnection();
    }

    public class RabbitmqConnectionProvider : IDisposable, IRabbitmqConnectionProvider
    {
        private readonly IAsyncConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitmqConnectionProvider> _logger;
        private IConnection _connection;

        public RabbitmqConnectionProvider(IAsyncConnectionFactory connectionFactory, ILogger<RabbitmqConnectionProvider> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public void Dispose()
        {
            try
            {
                if(_connection != null)
                {
                    _connection?.Close();
                    _connection?.Dispose();
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "Cannot dispose RabbitMq connection");
            }
        }

        public IConnection GetConnection()
        {
            if(_connection == null || !_connection.IsOpen)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            return _connection;
        }
    }
}
