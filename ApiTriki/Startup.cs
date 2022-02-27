using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Triki.CI.Tools;
using Triki.Data.Mysql;
using Microsoft.EntityFrameworkCore;
using Triki.CI.Dto;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ApiTriki
{
    public class Startup
    {
        private readonly string myPolice = Constants.MyPolice;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DbContextSqlTriki>(options =>
                options.UseMySQL(
                    Configuration.GetConnectionString(Constants.ConexionString)));

            services.AddCors(options => options.AddPolicy(myPolice,
                builder => builder.WithOrigins(Configuration["Config:OriginCors"])
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()));

            IConfigurationSection appSettingsSection = Configuration.GetSection("Config");
            services.Configure<AppSettingsDto>(appSettingsSection);

            AppSettingsDto appSettings = appSettingsSection.Get<AppSettingsDto>();

            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            string iusser = appSettings.Issuer;
            string audience = appSettings.Audience;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        int userId = int.Parse(context.Principal.Identity.Name);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("Token-Expired", "true");

                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = iusser,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Triki Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();              
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiTriki v1"));

            app.UseRouting();

            app.UseCors(b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
