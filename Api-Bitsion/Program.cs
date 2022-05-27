using Api_Bitsion.Core.Business;
using Api_Bitsion.Core.Helper;
using Api_Bitsion.Core.Interfaces;
using Api_Bitsion.DataAccess;
using Api_Bitsion.DataAccess.UnitOfWork;
using Api_Bitsion.DataAccess.UnitOfWork.Interface;
using Api_Bitsion.Repositories;
using Api_Bitsion.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Create Database SQL SERVER
var FicticiaConn = builder.Configuration.GetConnectionString("FicticiaConnection");
builder.Services.AddDbContext<FicticiaDbContext>(x => x.UseSqlServer(FicticiaConn));

// Repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();

//Unit of Work DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services/Business
builder.Services.AddScoped<IClientBusiness, ClientBusiness>();

//Automapper configure service
builder.Services.AddAutoMapper(typeof(ApiMapper));

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
