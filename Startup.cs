using EmployeeAPI.Filters;
using EmployeeAPI.Model;
using EmployeeAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EmployeeAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<EmployeeContext>(o => o.UseSqlServer(Configuration.GetConnectionString("EmployeeDB")));
            services.AddTransient<IEmployeeService, EmployeeRepository>();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });


            services.AddSwaggerGen(c =>
            {
                //Following code to avoid swagger generation error 
                //due to same method name in different versions.
                c.ResolveConflictingActions(descriptions =>
                {
                    return descriptions.First();
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                  
                    Title = "Employee API ",
                    Version = "1.0",
                    Description = "A simple API to create or update employees",
                    Contact = new OpenApiContact
                    {
                        Name = "Ed Tshuma",
                        Email = "edtshuma@gmail.com",
                        Url = new Uri("https://www.programmingwithwolfgang.com/")
                    }
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {

                    Title = "Employee API ",
                    Version = "2.0",
                    Description = "A simple API to create or update employees",
                    Contact = new OpenApiContact
                    {
                        Name = "Ed Tshuma",
                        Email = "edtshuma@gmail.com",
                        Url = new Uri("https://www.programmingwithwolfgang.com/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Employee API V2");
                c.RoutePrefix = string.Empty;
            });

            //TODO : Upgrade to NetCore 3.1 for API versioning full support
            // app.UseRouting();          
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
