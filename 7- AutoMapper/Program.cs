using BACKEND02.AutoMapper;
using BACKEND02.DTOs;
using BACKEND02.Models;
using BACKEND02.Repository;
using BACKEND02.Services;
using BACKEND02.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Construirlo despues de la inyeccion de dependencias

//Repository-----
builder.Services.AddScoped<IRepository<Auto>, AutoRepository>();
builder.Services.AddScoped<IRepository<Marca>, MarcaRepository>();

//Conexion Entity Frame Work
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//Validaciones
builder.Services.AddScoped<IValidator<AutoInserDto>, AutoInsertValidator>();
builder.Services.AddScoped<IValidator<MarcaDto>, MarcaInsertValidator>();
builder.Services.AddTransient<IValidator<AutoUpdateDto>, AutoUpdateValidation>();

//Services
builder.Services.AddKeyedScoped<ICommonServices<AutoDto, AutoInserDto, AutoUpdateDto>, AutoServices>("AutoService");

//Mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

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
