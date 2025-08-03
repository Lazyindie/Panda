using Microsoft.Extensions.DependencyInjection;
using Panda.Api.Configurations;
using Panda.Api.Controllers;
using Panda.Test.Common;
using System.Linq;
namespace Panda.Test;

public class ControllerTests
{
    [Fact]
    public void Can_Build_All_Controllers()
    {
        var configuration = ConfigurationFactory.CreateConfiguration();

        var serviceCollection = ServiceConfigurationFactory.CreateServiceCollection(ServiceConfiguration.Configure, configuration);

        var controllerAssembly = typeof(AppointmentController).Assembly;
        var controllerTypes = controllerAssembly.GetTypes()
            // Get all classes that inherit from ControllerBase (All our controllers).
            .Where(type => typeof(Microsoft.AspNetCore.Mvc.ControllerBase)
            .IsAssignableFrom(type))
            .ToArray();

        controllerTypes.ShouldNotBeEmpty();

        foreach (var type in controllerTypes)
        {
            serviceCollection.AddTransient(type);
        }

        using var serviceProvider = serviceCollection.BuildServiceProvider();

        foreach (var type in controllerTypes)
        {
            serviceProvider.GetRequiredService(type).ShouldNotBeNull($"instance of type {type.Name} should be resolvable.");
        }
    }
}
