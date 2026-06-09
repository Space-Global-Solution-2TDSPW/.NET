using Microsoft.EntityFrameworkCore;
using SpaceData.Data;
using SpaceData.Mappers;
using SpaceData.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext com Oracle, configuração
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

// Mappers
builder.Services.AddScoped<AgenteMapper>();
builder.Services.AddScoped<MissaoMapper>();
builder.Services.AddScoped<AgenteMissaoMapper>();

// Services
builder.Services.AddScoped<AgenteService>();
builder.Services.AddScoped<MissaoService>();
builder.Services.AddScoped<AgenteMissaoService>();

// Controllers
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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
