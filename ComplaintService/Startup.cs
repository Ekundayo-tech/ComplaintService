using ComplaintService.Authorization;
using ComplaintService.DataContext;
using ComplaintService.Interfaces;
using ComplaintService.Provider;
using ComplaintService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<ComplaintDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IComplaint, ComplaintServices>();
            services.AddTransient<IAuthProvider, AuthProvider>();
            services.AddTransient<IComplaintBaseService, ComplaintBaseService>();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ComplaintService", Version = "v1" });
            });



            //var tokenValidatorParameters = new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //    RequireExpirationTime = false,
            //    ValidateLifetime = true,
            //};

            //services.AddSingleton(tokenValidatorParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                //x.TokenValidationParameters = tokenValidatorParameters;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("mabel", policy =>
                {
                    policy.AddRequirements(new AuthorizationRequirement("mabel.com"));
                });
            });
            services.AddSingleton<IAuthorizationHandler, WorkAuthorizationHandlers>();
            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Bankly Cloud",
                    Version = "V2",
                    Description = "An API to perform business automated operations",
                    TermsOfService = new Uri("http://www.bankly.co.uk/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Emmanuel Ekundayo",
                        Email = "gbengahe@gmail.com",
                        Url = new Uri("http://www.bankly.co.uk/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Bankly API LICX",
                        Url = new Uri("http://www.bankly.co.uk/"),
                    },

                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[0] }
                };
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Tweeet Cloud Authorization header using bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {new OpenApiSecurityScheme {Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    } }, new List<string>() }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComplaintService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
