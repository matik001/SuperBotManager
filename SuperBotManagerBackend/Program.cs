using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using SuperBotManagerBackend.Configuration;
using SuperBotManagerBackend.Services;
using SuperBotManagerBase.Configuration;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;


//using SuperBotManagerBackend.Hubs;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(context.Configuration));

// Add services to the container.
var services = builder.Services;

services.AddAutoMapper(typeof(AutomapperProfile));
services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddDbContext<AppDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("SuperBotManagerBackend"));
});
services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
services.AddScoped<IActionDefinitionSeederService, ActionDefinitionSeederService>();
services.AddScoped<IAuthService, AuthService>();
services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.ConfigureJWT(builder.Configuration);
services.ConfigureCORS(builder.Configuration);
services.ConfigureEncryption(builder.Configuration);
services.AddSignalR()
        .AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.Converters
               .Add(new JsonStringEnumConverter());
        });
;
services.ConfigureRabbitMq(builder.Configuration);


// Configure the HTTP request pipeline.
var app = builder.Build();
Seed(app.Services);


app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.ConfigureCORS();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); /// done by nginx reverse proxy
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();
//app.MapHub<UserHub>("/hub/user");

app.ConfigureExceptionHandler(builder.Configuration, app.Logger);

app.Run();



static async Task Seed(IServiceProvider services)
{
    using(var scope = services.CreateScope())
    {
        var definitionsSeeder = scope.ServiceProvider.GetRequiredService<IActionDefinitionSeederService>();
        await definitionsSeeder.Seed(true);
    }
}