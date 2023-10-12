using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Modelos.Entidad;
using BUGGAFIT_BACK.Security;
using BUGGAFIT_BACK.Security.interfaces;
using BUGGAFIT_BACK.Security.Interfaces.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
//Conexion bd
//builder.Services.AddDbContext<MyDBContext>(options =>
//options.UseSqlServer("name=ConnectionStrings:Connection"));
var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});

#region Bloque para agregar las DI

// agregamos las interfaces
builder.Services.AddScoped<ICatalogoUsuarios, CatalogoUsuario>();
builder.Services.AddScoped<ICatalogoCompras, CatalogoCompras>();
builder.Services.AddScoped<ICatalogoVentas, CatalogoVentas>();
builder.Services.AddScoped<ICatalogoGastos, CatalogoGastos>();
builder.Services.AddScoped<ICatalogoInventario, CatalogoInventario>();
builder.Services.AddScoped<ICatalogoClientes, CatalogoClientes>();
builder.Services.AddScoped<ICatalogoTipoCuenta, CatalogoTipoCuenta>();
#endregion

#region Configuracion de Json Web Tokens
var key = Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero,
    };
});

// Initialize the Auth Service with the roles.
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
    config.AddPolicy(Policies.User, Policies.UserPolicy());
});

// Initialize the Authentication Interface service.
builder.Services.AddScoped<IAuthenticationSystem>(optins =>
{
    var authenticationSystem = new AuthenticationSystem(builder.Configuration["Authentication:SecretKey"], builder.Configuration["Authentication:Audience"],
        builder.Configuration["Authentication:Issuer"], builder.Configuration["Authentication:ExpireTime"]);
    return authenticationSystem;
});

#endregion

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//politica de cors creando
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//habilitar los nuevos cors
app.UseCors("NuevaPolitica");
app.UseAuthorization();

app.MapControllers();

app.Run();
