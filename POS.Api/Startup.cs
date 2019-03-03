using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using POS.Api.Repositories;
using POS.Api.Repositories.Interfaces;
using POS.Api.Utilities;

namespace POS.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(options => {
                options.AddPolicy(Utility.CORE_POLICY, builder => {
                    builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // shows UseCors with any policy builder
            app.UseCors(Utility.CORE_POLICY);

            // status code pages allows to send page response for browsers.
            app.UseStatusCodePages();

            app.UseMvc();

        }
    }
}
