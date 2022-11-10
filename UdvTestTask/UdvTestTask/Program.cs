using Microsoft.EntityFrameworkCore;
using Serilog;
using UdvTestTask.Abstractions;
using UdvTestTask.Data;
using UdvTestTask.Models;
using UdvTestTask.Services;
using VkNet;
using VkNet.Abstractions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Hour, rollOnFileSizeLimit: true)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<IVkApi, VkApi>(provider => new VkApi())
    .AddSingleton<ILetterCountService, LetterCounter>()
    .AddSingleton<IPageService, PageService>()
    .AddDbContext<LettersCountDbContext>(optionsBuilder =>
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("postgres")))
    .AddScoped<IRepository<LettersCount>, LettersCountDbContext>()
    .AddSingleton<IAuthService, AuthService>(provider =>
        new AuthService(ulong.Parse(builder.Configuration["AppId:Id"]), provider.GetRequiredService<IVkApi>()));


var app = builder.Build();

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

Log.CloseAndFlush();