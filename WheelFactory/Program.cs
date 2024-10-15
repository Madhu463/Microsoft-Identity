using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WheelFactory.Models;
using WheelFactory.Services;
using Microsoft.Identity.Web;
using System;
using Microsoft.AspNetCore.Identity;
using WheelFactory.Repositories;

namespace WheelFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            // Set up Serilog for logging
            var logger = new LoggerConfiguration()
                .WriteTo.File("C:\\Users\\ksathvikreddy\\Desktop\\WheelFactory\\Wheel-Factory\\Backend\\WheelFactory\\Logs\\WheelFactoryLogs20240928.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Host.UseSerilog(logger);

            // Register your services and DbContext
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
            builder.Services.AddScoped<ITasksRepository, TasksRepository>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();

            builder.Services.AddDbContext<WheelContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("wheel")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
