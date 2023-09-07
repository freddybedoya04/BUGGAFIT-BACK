using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Conexion bd
builder.Services.AddDbContext<MyDBContext>(options =>
options.UseSqlServer("name=ConnectionStrings:Connection"));
// Add services to the container.

builder.Services.AddControllers();
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
if (app.Environment.IsDevelopment())
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
