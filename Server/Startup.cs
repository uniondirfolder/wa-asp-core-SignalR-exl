using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Auth;
using Server.Hub;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public class Startup
    {

        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<FakeUsers>(configuration.GetSection("FakeUsers"));
            services.AddControllers();

            //services.AddSignalR();
            services.AddSignalR().AddMessagePackProtocol();
            //services.AddCors();

            services.AddCors(options => 
            {
                options.AddPolicy("MyCorsPolicy", builder => 
                {
                    builder
                    .SetIsOriginAllowed(origin => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                    
          
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Auth.AuthOptions.Issuer,
                        RequireAudience = true,
                        ValidateAudience = true,
                        ValidAudience = Auth.AuthOptions.Audience,
                        RequireExpirationTime = true,
                        ValidateIssuerSigningKey = true,
                        RequireSignedTokens = true,
                        IssuerSigningKey = Auth.AuthOptions.PublicKey

                    };

                    options.Events = new JwtBearerEvents 
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["token"];
                            if(!string.IsNullOrWhiteSpace(accessToken) && context.Request.Path.StartsWithSegments("/messages")) 
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options => 
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("MyCorsPolicy");
            //app.UseCors(policy =>
            //{
            //    policy
            //    .SetIsOriginAllowed(origin => true)
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowCredentials();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/message");
            });
        }
    }
}
