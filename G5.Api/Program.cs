using G5.Infrastructure.Data;
using G5.Application.Features.Permission.Commands;
using Employee.Infrastructure.utils;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var con = builder.Configuration.GetConnectionString("WebApiDatabase");

builder.Services.AddDbContext<G5Context>(m => m.UseSqlServer(con));
builder.Services.AddMediatR(typeof(RequestPermissionCommandHandler).GetTypeInfo().Assembly);

builder.Services.AddRepositories();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

Log.Information("Starting up");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
