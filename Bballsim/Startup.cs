using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Bballsim.Commish.DatabaseAccess.DatabaseMigrations;
using Bballsim.Commish.Services;
using Bballsim.Commish.DatabaseAccess;

namespace Bballsim
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var serviceProvider = DbMigratorRunner.CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                DbMigratorRunner.UpdateDatabase(scope.ServiceProvider);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITeamOverrider, TeamOverrider>();
            services.AddControllers();

            // services.Add(new ServiceDescriptor(typeof(CommishDbContext), new CommishDbContext(Configuration.GetConnectionString("DefaultConnection")))); 

            var connectionString = Configuration.GetConnectionString("CommishDatabase");

            services.AddDbContext<CommishDbContext>(options => 
            options.UseMySQL(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
