using Data.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Mail.Abstract;
using Service.Mail.Concrete;
using Service.RabbitMQ.Abstract;
using Service.RabbitMQ.Concrete;
using System;
using System.Threading.Tasks;

namespace RabbitMq.Consumer
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var consumer = host.Services.GetRequiredService<Consumer>();

            var queue = RabbitMqQueue.EmailSenderQueue.ToString();

            consumer.Start(queue);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((services) =>
            {
                services.AddScoped<IConfiguration>(_ => new ConfigurationBuilder().AddJsonFile($"C:/Users/omer-/Desktop/PayCore_FinalProject/PayCore_Final/appsettings.json").Build());
                services.AddScoped<IPublisherService, PublisherService>();
                services.AddScoped<IRabbitMqService, RabbitMqService>();
                services.AddScoped<ISmtpServer, SmtpServer>();
                services.AddTransient<Consumer>();
            });


    }
}
