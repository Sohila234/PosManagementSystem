
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PosManagement.Domain.Interfaces;
using PosManagement.Infrastructure.Data;
using PosManagement.Infrastructure.Repositories;


namespace PosManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<PosDB>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PosDB")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddMediatR(x=>
            x.RegisterServicesFromAssembly(typeof(PosManagement.Application.Common.Result).Assembly));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
       
          builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();   // we dol kman hena 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }// ab2e ektby dol hena 

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
