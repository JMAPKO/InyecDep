using BACKEND02.Models;
using BACKEND02.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKeyedSingleton<IRandomServices, RandomServices>("RandomSingleton");
builder.Services.AddKeyedScoped<IRandomServices, RandomServices>("RandomScoped");
builder.Services.AddKeyedTransient<IRandomServices, RandomServices>("RandomTransient");

builder.Services.AddScoped<IPostsService, PostsService>();

//Construirlo despues de la inyeccion de dependencias
builder.Services.AddHttpClient<IPostsService, PostsService>(
    client => client.BaseAddress = new Uri(builder.Configuration["BaseURL"])
);


//Conexion Entity Frame Work
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

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
