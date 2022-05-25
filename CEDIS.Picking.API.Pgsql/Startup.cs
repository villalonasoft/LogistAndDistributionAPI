using AutoMapper;
using CEDIS.Core.Pgsql.AutoMapperProfiles;
using CEDIS.Core.Pgsql.Frameworks;
using CEDIS.Core.Pgsql.HubConfig;
using CEDIS.Core.Pgsql.Persistences;
using CEDIS.Core.Pgsql.Services;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Picking.API.Pgsql
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
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CedisPickingProfile());
            });
            var mapper = mappingConfig.CreateMapper();

            #region Services
            services.AddSingleton(mapper);
            services.AddTransient<IBranchOrderService, BranchOrderService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IBranchServices, BranchServices>();
            services.AddTransient<IPickingService, PickingService>();
            services.AddTransient<IUserSevice, UserService>();
            services.AddTransient<IUnitService, UnitService>();
            services.AddTransient<IModeService, ModeService>();
            services.AddTransient<IWarehouseService, WarehouseService>();

            services.AddSingleton(new JwtBuilder(Configuration));
            #endregion

            services.AddSignalR(option =>
            {
                option.EnableDetailedErrors = true;
            });

            services.AddControllers();
            var origin = new string[] { "http://192.168.10.48:4200", "https://192.168.10.48:80", "https://192.168.10.48:443","https://192.168.10.48:5001",
            "192.168.10.106"};

            services.AddCors(c => c.AddPolicy("corsPolicy", builder =>
            {
                builder
                    .WithOrigins(origin)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials();
            }));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                options.UseLowerCaseNamingConvention();
                options.EnableSensitiveDataLogging(true);
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Key),
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            dbContext.Database.Migrate();

            app.UseRouting();
            app.UseCors("corsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<BranchOrderHub>("/hubs/orders");
                endpoints.MapHub<HeadersHub>("/hubs/headers");
            });
        }
    }
}
