using JwtStore.Core.Contexts.AccountContext;
using JwtStore.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtStore.Api.Extensions
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.Database.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            Configuration.Secrets.ApiKey = builder.Configuration["Secrets:ApiKey"];
            Configuration.Secrets.PasswordSaltKey = builder.Configuration["Secrets:PasswordSaltKey"];
            Configuration.Secrets.JwtPrivateKey = builder.Configuration["Secrets:JwtPrivateKey"];
        }

        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            // pode usar migrations
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(
                Configuration.Database.ConnectionString, b => b.MigrationsAssembly("JwtStore.Api")));
        }

        public static void AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
                );
            builder.Services.AddAuthorization();
        }
    }
}