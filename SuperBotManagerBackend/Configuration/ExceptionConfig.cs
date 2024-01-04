using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBackend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SuperBotManagerBackend.Configuration
{
    public static class ExceptionConfig
    {
        public static bool ReturnStackTrace { get; private set; }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IConfigurationManager configuration, ILogger logger)
        {
            ReturnStackTrace = configuration.GetValue<bool>("ReturnStackTrace");
            app.UseExceptionHandler(builder => {
                builder.Run(async context => {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        var error = contextFeature.Error;
                        logger.LogError(error.ToString());

                        context.Response.ContentType = "application/json";

                        ErrorDTO errorDto;
                        if(error is ServiceException)
                        {
                            errorDto = (error as ServiceException).Error;
                        }
                        else
                        {
                            errorDto = new ErrorDTO()
                            {
                                StatusCode = 500,
                                Message = error.Message,
                                StackTrace = ReturnStackTrace ? error.StackTrace : null
                            };
                        }
                        context.Response.StatusCode = errorDto.StatusCode;
                        await context.Response.WriteAsync(errorDto.ToString());

                        logger.LogError($"Something went wrong: {error.ToString()}");
                    }
                });
            });
        }
    }
}
