using BACKEND02.Services;

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
