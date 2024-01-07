using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SuperBotManagerBackend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace SuperBotManagerBackend.Configuration
{
    public static class EncryptionConfig
    {
        public static string KeyBase64 { get; private set; }
        public static byte[] Key { get; private set; }
        public static void ConfigureEncryption(this IServiceCollection services, IConfigurationManager configuration)
        {
            var section = configuration.GetSection("Encryption");
            if(section == null)
            {
                throw new Exception("Encryption section not found in appsettings.json");
            }
            KeyBase64 = section.GetValue<string>("KeyBase64");
            Key = Convert.FromBase64String(KeyBase64);
        }
        public static void GenerateNewKey()
        {
            Aes aes = Aes.Create();
            aes.GenerateKey();
            var base64 = Convert.ToBase64String(aes.Key);
            Console.WriteLine("ENCRYPTION KEY: " + base64);
        }
    }
}
