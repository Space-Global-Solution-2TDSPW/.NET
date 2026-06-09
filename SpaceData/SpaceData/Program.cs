using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SpaceData.Data;
using SpaceData.Repositories;
using SpaceData.Services;

var builder = WebApplication.CreateBuilder(args);

// ── DbContext ────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

// ── Repositórios ─────────────────────────────────────────────────────────────
builder.Services.AddScoped<IAgenteRepository, AgenteRepository>();
builder.Services.AddScoped<IMissaoRepository, MissaoRepository>();
builder.Services.AddScoped<IAgenteMissaoRepository, AgenteMissaoRepository>();

// ── Serviços ──────────────────────────────────────────────────────────────────
builder.Services.AddScoped<AgenteService>();
builder.Services.AddScoped<MissaoService>();
builder.Services.AddScoped<AgenteMissaoService>();

// ── Controllers + Swagger ─────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SpaceData API",
        Version = "v1",
        Description = "API para gerenciamento de missões espaciais — cadastro de agentes, missões e vínculos.",
        Contact = new OpenApiContact
        {
            Name = "FIAP",
            Url = new Uri("https://www.fiap.com.br"),
            Email = "contato@fiap.com.br"
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);

    c.OrderActionsBy(description => description.RelativePath);
    c.UseInlineDefinitionsForEnums();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpaceData API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();