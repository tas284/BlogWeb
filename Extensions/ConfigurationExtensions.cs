using BlogWeb.Attributes;
using BlogWeb.Configuration;
using BlogWeb.Data;
using BlogWeb.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace BlogWeb.Extensions
{
    public static class ConfigurationExtensions
    {
        public static WebApplicationBuilder AddDataContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BlogDataContext>(x => x.UseSqlServer(connectionString));
            return builder;
        }

        public static WebApplicationBuilder AddFilters(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ApiKeyFilter>();
            return builder;
        }

        public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection("ApiConfiguration"));
            builder.Services.Configure<SmtpConfiguration>(builder.Configuration.GetSection("SmtpConfiguration"));
            return builder;
        }

        public static WebApplicationBuilder ConfigureJwt(this WebApplicationBuilder builder)
        {
            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("ApiConfiguration:JWTKey").Value);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return builder;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                });
            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<TokenService>();
            services.AddTransient<EmailService>();
            services.AddMemoryCache();
            return services;
        }
    }
}
