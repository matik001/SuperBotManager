using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SuperBotManagerBackend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SuperBotManagerBackend.Configuration
{
    public static class RabbitMqConfig
    {
        public static string Hostname { get; private set; }
        public static string Username { get; private set; }
        public static string Password { get; private set; }
        public static string VirtualHost { get; private set; }
        public static void ConfigureRabbitMq(this IServiceCollection services, IConfigurationManager configuration)
        {
            var section = configuration.GetSection("RabbitMQ");
            if(section == null)
            {
                throw new Exception("RabbitMQ section not found in appsettings.json");
            }
            Hostname = section.GetValue<string>("Hostname");
            Username = section.GetValue<string>("Username");
            Password = section.GetValue<string>("Password");
            VirtualHost = section.GetValue<string>("VirtualHost");

            services.AddScoped<IMessageProducer, MessageProducer>();
        }
    }
}
