using Base.Jwt;
using Data.Model;
using EmailService.Configurations;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PayCore_Final.StartUpExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_Final
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static JwtConfig JwtConfig { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var hangfireConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddHangfire(configuration =>
            {
            var option = new PostgreSqlStorageOptions
            {
                TransactionSynchronisationTimeout = TimeSpan.FromMinutes(5),
                InvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.FromMinutes(5),
                PrepareSchemaIfNecessary = true,
                EnableTransactionScopeEnlistment = true
                
                
            };
             configuration.UsePostgreSqlStorage(hangfireConnectionString, option)
                .WithJobExpirationTimeout(TimeSpan.FromHours(6));
            });
            

            services.AddHangfireServer();


            var connStr = Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddNHibernatePosgreSql(connStr);

            JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            var eMailConfig = Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();

            services.AddSingleton(eMailConfig);
            
            services.AddServices();
            services.AddJwtBearerAuthentication();
            services.AddCustomizeSwagger();
            services.AddControllers();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayCore_Final v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard();
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 5 });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
