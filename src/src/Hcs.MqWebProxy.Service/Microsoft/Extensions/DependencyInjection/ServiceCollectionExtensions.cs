using MassTransit;
using MassTransit.MultiBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            var serviceBusType = configuration["ServiceBusType"];
            if (serviceBusType == "RabbitMQ")
            {
                hcBuilder.AddRabbitMQ(
                    $"amqp://{configuration["ServiceBusConnection"]}",
                    name: "hcs-mqwebproxy-rabbitmq-check",
                    tags: new string[] { "rabbitmq" });
            }

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusType = configuration["EventBusType"];
            if (eventBusType == "RabbitMQ")
            {
                services.AddMassTransit(x =>
                {
                    x.AddBus(_ => Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host(configuration["EventBusConnection"], h =>
                        {
                            h.Username(configuration["EventBusUsername"]);
                            h.Password(configuration["EventBusPassword"]);
                        });
                    }));
                });
            }
            else
            {
                throw new Exception($"Event Bus '{eventBusType}' is not supported.");
            }

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HCS MQ Web Proxy Service",
                    Version = "v1",
                    Description = "The Message Queue Web Proxy Service."
                });
            });

            return services;
        }
    }
}
