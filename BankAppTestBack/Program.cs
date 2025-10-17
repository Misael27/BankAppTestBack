using BankAppTestBack.Application.Common.Mapping;
using BankAppTestBack.Application.Common.Options;
using BankAppTestBack.Application.Common.ValidationHandle.Behaviours;
using BankAppTestBack.Application.ValidationHandle.Filters;
using BankAppTestBack.Domain.Abstractions;
using BankAppTestBack.Domain.Repositories;
using BankAppTestBack.Domain.Services;
using BankAppTestBack.Infrastructure.Repositories;
using BankAppTestBack.Infrastructure.Services;
using FluentValidation;
using Infrastructure.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var applicationAssembly = typeof(ValidationBehaviour<,>).Assembly;

        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IMovementRepository, MovementRepository>();
        builder.Services.AddScoped<IReportRepository, ReportRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddValidatorsFromAssembly(applicationAssembly);
        builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly);

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(applicationAssembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        builder.Services.AddScoped<IPasswordHasher, BCryptHasher>();


        builder.Services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true
        );


        builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
        {
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));


        builder.Services.AddDbContext<DataContext>(opt =>
            opt.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("Default"))
        );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.Configure<LimitOptions>(
            builder.Configuration.GetSection(LimitOptions.Limit));


        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during database migration.");
            }
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("corsapp");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}