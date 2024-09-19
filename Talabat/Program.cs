
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build(); 
            using var scope = app.Services.CreateScope();

            var servies = scope.ServiceProvider;
            var _dbContext = servies.GetRequiredService<StoreContext>();

            var loggerFactory = servies.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();//update DataBase
              

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, " an Error has Occure During Apply Migration");

            }
           
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
