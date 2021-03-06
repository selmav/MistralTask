using System.Text.Json;
using Core;
using Core.Implementation;
using Core.Interface;
using Data;
using Data.Implementation;
using Data.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UserManagementApi.Middleware;

namespace UserManagementApi
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });


            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddAutoMapper(typeof(ServiceProfile));

            services.AddDbContext<UserManagementContext>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("UserManagement")));

            // Repository
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();

            // Service
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPermissionService, PermissionService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagementApi", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementApi v1"));
            }

            app.UseMiddleware<ErrorHandling>();
            app.UseHttpsRedirection();

            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
