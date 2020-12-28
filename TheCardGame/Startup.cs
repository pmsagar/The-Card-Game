using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TheCardGame
{
    public static class Startup
    {
        public static IConfigurationRoot Initialize(string[] args)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))

                .AddJsonFile("appsettings.json", optional: true);
            return builder.Build();
        }
    }
}
