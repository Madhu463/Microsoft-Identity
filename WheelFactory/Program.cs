//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using Serilog;
//using WheelFactory.Models;
//using WheelFactory.Services;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Identity.Web;
//using System.Configuration;

//namespace WheelFactory
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            //    .AddJwtBearer(options =>
//            //    {
//            //        options.TokenValidationParameters = new TokenValidationParameters
//            //        {
//            //            ValidateIssuer = true,
//            //            ValidateAudience = true,
//            //            ValidateLifetime = true,
//            //            ValidateIssuerSigningKey = true,
//            //            ValidIssuer = builder.Configuration["JWT:issuer"],
//            //            ValidAudience = builder.Configuration["JWT:audience"],
//            //            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:secret"]))
//            //        };
//            //    });

//            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

//            builder.Services.AddSwaggerGen();
//            builder.Services.AddCors();
//            builder.Services.AddControllers();
//            var logger = new LoggerConfiguration()
//            .WriteTo
//            .File("C:\\Users\\tmadhushalini\\Desktop\\WheelFactory\\Wheel-Factory\\Backend\\WheelFactory\\Logs\\WheelFactoryLogs20240928.txt", rollingInterval: RollingInterval.Day)
//            .CreateLogger();
//            builder.Services.AddSerilog(logger);

//            // Register your services and DbContext
//            builder.Services.AddScoped<IOrdersService, OrdersService>();
//            builder.Services.AddScoped<ITaskService, TaskService>();

//            builder.Services.AddDbContext<WheelContext>(options =>
//                options.UseSqlServer(builder.Configuration.GetConnectionString("wheel")));

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseStaticFiles();
//            app.UseCors(options =>
//            {
//                options.AllowAnyOrigin();
//                options.AllowAnyMethod();
//                options.AllowAnyHeader();
//            });

//            app.UseAuthentication();
//            app.UseAuthorization();

//            app.MapControllers();

//            app.Run();
//        }
//    }
//}
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

namespace WheelFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Configure Microsoft Identity authentication
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
                .WriteTo.File("C:\\Users\\tmadhushalini\\Desktop\\WheelFactory\\Wheel-Factory\\Backend\\WheelFactory\\Logs\\WheelFactoryLogs20240928.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Host.UseSerilog(logger);

            // Register your services and DbContext
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<ITaskService, TaskService>();

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
