using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using CRUD.ManagementUser.Application.Common.Constants;
using CRUD.ManagementUser.Application.Services.Logging.Constants;

namespace CRUD.ManagementUser.Infrastructure.Logging;

public static class DependencyInjection
{
    public static IHostBuilder UseLoggingService(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration.ConfigureSerilog(hostBuilderContext.Configuration));
        SelfLog.Enable(message => Console.WriteLine(message));

        return hostBuilder;
    }

    public static LoggerConfiguration ConfigureSerilog(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        return loggerConfiguration
            .ReadFrom.Configuration(configuration, sectionName: $"{nameof(Logging)}")
            .Enrich.WithProperty(PropertyNameFor.Version, CommonValueFor.EntryAssemblyVersion!)
            .Enrich.WithProperty(PropertyNameFor.AssemblyName, CommonValueFor.EntryAssemblySimpleName!)
            .Enrich.WithProperty(PropertyNameFor.EnvironmentName, CommonValueFor.EnvironmentName);
    }
}
