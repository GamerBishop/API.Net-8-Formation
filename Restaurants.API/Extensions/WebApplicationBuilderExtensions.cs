using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;
using System.Reflection;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        []
                    }
                });

                // Chemin du fichier de commentaires XML
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // Ajouter le filtre pour les exemples de réponses
                c.OperationFilter<SwaggerResponseStatusCodeFilter>();

            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<TimeLoggingMiddleware>();


            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
