using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.Services
{
    public interface IRabbitmqChannelProvider
    {
        IModel GetChannel();
    }

    public class RabbitmqChannelProvider : IDisposable, IRabbitmqChannelProvider
	{
        private readonly ILogger<RabbitmqChannelProvider> _logger;
        readonly IRabbitmqConnectionProvider _connectionProvider;
		private IModel _model;

		public RabbitmqChannelProvider(ILogger<RabbitmqChannelProvider> logger, IRabbitmqConnectionProvider connectionProvider)
        {
            _logger = logger;
            _connectionProvider = connectionProvider;
        }

		public IModel GetChannel()
		{
			if(_model == null || !_model.IsOpen)
			{
				_model = _connectionProvider.GetConnection().CreateModel();
			}

			return _model;
		}

		public void Dispose()
		{
			try
			{
				if(_model != null)
				{
					_model?.Close();
					_model?.Dispose();
				}
			}
			catch(Exception ex)
			{
				_logger.LogCritical(ex, "Cannot dispose RabbitMq channel or connection");
			}
		}
	}
}
