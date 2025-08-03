using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Panda.Test.Common;
internal class ConfigurationFactory
{
    internal static ConfigurationManager CreateConfiguration()
    {
        var configuration = new ConfigurationManager();

        configuration
            .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json"), optional: true)
            .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.tests.json"), optional: true);

        return configuration;
    }
}
