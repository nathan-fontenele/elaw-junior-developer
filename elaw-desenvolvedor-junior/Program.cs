using elaw_desenvolvedor_junior.Application.Interfaces;
using elaw_desenvolvedor_junior.Application.Mapping;
using elaw_desenvolvedor_junior.Application.Services;
using elaw_desenvolvedor_junior.Domain.Interfaces;
using elaw_desenvolvedor_junior.Infrastructure.Persistence;
using elaw_desenvolvedor_junior.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingClient).Assembly);
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("elawDb"));

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

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
