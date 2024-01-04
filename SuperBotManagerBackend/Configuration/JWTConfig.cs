using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SuperBotManagerBackend.DB;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SuperBotManagerBackend.Configuration
{
    public static class AuthPolicy
    {
        public const string AdminOnly = "AdminOnly";
    }
    public static class JWTConfig
    {
        public static string Issuer { get; private set; }
        public static string Audience { get; private set; }
        public static string Key { get; private set; }
        public static int LifetimeInMinutes { get; private set; }

        public static void ConfigureJWT(this IServiceCollection services, IConfigurationManager configuration)
        {
            var section = configuration.GetSection("JWT");
            if(section == null)
            {
                throw new Exception("JWT section not found in appsettings.json");
            }
            Issuer = section.GetValue<string>("Issuer") ?? "";
            Audience = section.GetValue<string>("Audience") ?? "";
            Key = section.GetValue<string>("Key") ?? "";
            LifetimeInMinutes = section.GetValue<int>("LifetimeInMinutes");
            var builtServiceProvider = services.BuildServiceProvider();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = Issuer,
                        ValidAudience = Audience,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(
                             Encoding.UTF8.GetBytes(Key)),
                        LifetimeValidator = (before, expires, token, param) =>
                        {
                            if(expires < DateTime.UtcNow)
                                return false;
                            var uow = builtServiceProvider.GetRequiredService<IAppUnitOfWork>();
                            var isRevoked = uow.RevokedTokenRepository.IsTokenRevoked(token.Id);
                            if(isRevoked)
                                return false;
                            return true;
                        }
                    };

                    /// auth for signalr
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options => {
                
                /// Users must be authenticated to access any endpoint
                options.DefaultPolicy = new AuthorizationPolicyBuilder(options.DefaultPolicy) 
                     .RequireAuthenticatedUser()
                     .Build();

                options.AddPolicy(AuthPolicy.AdminOnly, policy => {
                    policy.RequireClaim("Admin", true.ToString());
                });
            });
        }
    }
}
