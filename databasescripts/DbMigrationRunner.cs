
using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace commish.databasescripts
{
    public static class DbMigratorRunner
    {
        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        public static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddMySql5()
                    // Set the connection string
                    .WithGlobalConnectionString("Server=127.0.0.1;Port=3306;Database=commishsql;Uid=notso;Pwd=enough123;")
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(M001_script).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        public static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}