using EasyKPI.Core;
using EasyKPI.Core.Services;
using EasyKPI.Core.Services.AccessDatawh;
using EasyKPI.Core.Services.AccessReport;
using EasyKPI.Core.Services.Dashboard;
using EasyKPI.Core.Services.DataWarehouse;
using EasyKPI.Core.Services.Embeds;
using EasyKPI.Core.Services.Profile;
using EasyKPI.Core.Services.Reports;
using EasyKPI.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;

namespace EasyKPI.WebApi
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

            services.AddControllers();

            //Configuration for SQL server

            services.AddDbContext<AppDbContext>();

            // Services

            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IDWHService, DWHService>();

            services.AddTransient<IReportService, ReportService>();

            services.AddTransient<IProfileService, ProfileService>();

            services.AddTransient<IAccessReport, AccessReport>();

            services.AddTransient<IAccessDatawh, AccessDatawh>();

            services.AddTransient<IEmbedService, EmbedService>();

            services.AddTransient<IDashboardService, DashboardService>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            //swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { Title = "Put title here", Description = "DotNet Core Api 3 - with swagger" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("EasyKpiPolicy",
                    builder =>
                    {
                        builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            // Jwt
            
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET");

            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(opts =>
           {
               opts.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = issuer,
                   ValidateAudience = false,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret))
               };
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = "/Images"
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("EasyKpiPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Easy KPI V1");
            });
        }
    }
}
