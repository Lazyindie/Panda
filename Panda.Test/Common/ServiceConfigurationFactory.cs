using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Panda.Test.Common;

internal class ServiceConfigurationFactory
{
    internal static ServiceCollection CreateServiceCollection(
        Action<IServiceCollection, ConfigurationManager> configureServices,
        ConfigurationManager configuration)
    {
        var serviceCollection = new ServiceCollection();
        configureServices(serviceCollection, configuration);

        serviceCollection.AddSingleton<IConfiguration>(configuration);

        var mockWebHostEnvironment = Substitute.For<IWebHostEnvironment>();
        serviceCollection.AddSingleton(mockWebHostEnvironment);

        return serviceCollection;
    }
}
