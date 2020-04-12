using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using MedAidAPI.Options;
using Swashbuckle.AspNetCore.Swagger;
using MedAidAPI.Models;
using Microsoft.AspNetCore.Identity;
using MedAidAPI.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;

namespace MedAidAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                       .AddDbContext<MedAidAPIContext>((serviceProvider, options) =>
           options.UseSqlServer(Configuration.GetConnectionString("MedAidAPIConnectionString"))
                  .UseInternalServiceProvider(serviceProvider));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = "localhost",
                        ValidIssuer = "localhost",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"))
                    };
                });



            //services.AddTransient<IUserContext, SeedUserContext>();
            services.AddMvc()
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            // Adding Swagger
            services.AddSwaggerGen(option => { option.SwaggerDoc("v1", new Info { Title = "MedAidAPI Api", Version = "v1.0.0" }); });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
                                , UserManager<MedAidAPIUser> userManager)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
            SeedDataInitializer seedDataInitializer = new SeedDataInitializer();
            seedDataInitializer.SeedData(userManager);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var swaggerOptions = new MedAidAPI.Options.SwaggerOptions();
            Configuration.GetSection(nameof(MedAidAPI.Options.SwaggerOptions)).Bind(swaggerOptions);



            //m Swagger Configurations
            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(option => { option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description); });
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/V1/swagger.json", "MedAidAPI Api"));
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors(MyAllowSpecificOrigins);

        }
    }
}
