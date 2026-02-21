
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.IoC;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();



            var universityConnectionString = builder.Configuration.GetConnectionString("UniversityDbConnection") ?? throw new InvalidOperationException("Connection string 'UniversityDbConnection' not found.");
            builder.Services.AddDbContext<UniversityDbContext>(options =>
                options.UseSqlServer(universityConnectionString));


            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "University Api", Version = "v1" });
            });

            RegisterServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "University Api v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            DependancyContainer.RegisterServices(services);
        }
    }
}
