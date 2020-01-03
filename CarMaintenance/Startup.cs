using CarMaintenance.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarMaintenance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationAssembly = typeof(Startup).Assembly.GetName().Name;

            services.AddControllers();
            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = Configuration.GetValue<string>("Authority");
            //        options.RequireHttpsMetadata = Configuration.GetValue<bool>("RequireHttpsMetadata");
            //        options.Audience = Configuration.GetValue<string>("Audience");
            //    });
            services.AddDbContext<ApplicationDbContext>(options => options
                    .UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    mariaDbOptions => mariaDbOptions.MigrationsAssembly(migrationAssembly)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.EnsureMigrationOfContext<ApplicationDbContext>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpStatusCodeExceptionMiddleware();

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
