
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Errors;
using Talabat.Extensions;
using Talabat.Helpers;
using Talabat.MiddelWares;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddApplicationServices();
            builder.Services.AddSwaggerServices();
            builder.Services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            var app = builder.Build(); 
            using var scope = app.Services.CreateScope();

            var servies = scope.ServiceProvider;
            var _dbContext = servies.GetRequiredService<StoreContext>();

            var loggerFactory = servies.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();//update DataBase
                await StoreContextSeed.SeedAsync(_dbContext);//Data Seeding

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, " an Error has Occure During Apply Migration");

            }


            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddelWare>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddelWares();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
