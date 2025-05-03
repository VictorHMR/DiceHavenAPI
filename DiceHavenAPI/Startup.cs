using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using DiceHavenAPI;
using DiceHavenAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DiceHavenAPI.Services;
using DiceHavenAPI.Interfaces;
using Microsoft.AspNetCore.SignalR;
using DiceHaven_API.Hubs;
using DotNetEnv;
namespace DiceHavenAPI
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        static Startup()
        {
            SQLitePCL.Batteries.Init();
        }


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            Env.Load();
#endif

            services.AddDbContext<DiceHavenBDContext>(options =>
                options.UseSqlite(Environment.GetEnvironmentVariable("CONNECTION_STRING")));


            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")); 
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();

                });
            });

            services.AddSignalR()
                .AddNewtonsoftJsonProtocol(options =>
                {
                    options.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.PayloadSerializerSettings.Converters.Add(new StringEnumConverter());
                });


            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.Configure<FormOptions>(options =>
            {
                // Set the limit to 256 MB
                options.MultipartBodyLengthLimit = 268435456;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Geral", new OpenApiInfo { Title = "Geral", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
                c.EnableAnnotations();
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddCors();

            services.AddRouting(options => options.LowercaseUrls = true);

            #region Injeção de Dependência 

            services.AddScoped<ICampanha, Campanha>();
            services.AddScoped<IChat, Chat>();
            services.AddScoped<IContato, Contato>();
            services.AddScoped<IFicha, Ficha>();
            services.AddScoped<IPersonagem, Personagem>();
            services.AddScoped<IUsuario, Usuario>();


            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/Geral/swagger.json", "Geral");
            });

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CampanhaChatHub>("/CampanhaChatHub").RequireAuthorization();
            });
        }

    }
}
