using System.Text;
using Api_Bitsion.Core.Business;
using Api_Bitsion.Core.Helper;
using Api_Bitsion.Core.Interfaces;
using Api_Bitsion.DataAccess;
using Api_Bitsion.DataAccess.UnitOfWork;
using Api_Bitsion.DataAccess.UnitOfWork.Interface;
using Api_Bitsion.Entities.Auth;
using Api_Bitsion.Repositories;
using Api_Bitsion.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "Prueba-Técnica-Bitsion", Version = "v1" });
    o.AddSecurityDefinition("Bearer", 
    new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
			new List<string>()
        }
    });
});

// Create Database SQL SERVER
var FicticiaConn = builder.Configuration.GetConnectionString("FicticiaConnection");
    builder.Services.AddDbContext<FicticiaDbContext>(x => x.UseSqlServer(FicticiaConn));
    
var UserConn = builder.Configuration.GetConnectionString("UserConnection");
    builder.Services.AddDbContext<UsersDbContext>(x => x.UseSqlServer(UserConn));

// DI to implement the Login and Register system
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UsersDbContext>()
    .AddDefaultTokenProviders();

// DI to Configure token and Authentication
builder.Services.AddAuthentication(option => 
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(option => 
{
    option.SaveToken = true;
    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"], 
        ValidIssuer = configuration["JWT:ValidIssuer"],        
        IssuerSigningKey = 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

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

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
