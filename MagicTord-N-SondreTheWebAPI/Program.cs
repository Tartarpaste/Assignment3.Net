using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Services;
using MagicTord_N_SondreTheWebAPI.Services.Characters;
using MagicTord_N_SondreTheWebAPI.Services.Franchises;
using MagicTord_N_SondreTheWebAPI.Services.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

using (var context = new DBContext())
{
    context.Database.EnsureCreated();
}

var builder = WebApplication.CreateBuilder(args);

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "THE NCU API",
        Description = "Simple API to manage movie franchises and characters",
    });
    options.DocumentFilter<SchemaFilter>();
    options.IncludeXmlComments(xmlPath);
});

// Adding services to the container
builder.Services.AddDbContext<DBContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("THENCU")
        )
    );

// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Adding logging through ILogger
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Transient is the default behaviour and means a new instance is made when injected.*/
builder.Services.AddTransient<ICharacterService, CharacterServiceImpl>();
builder.Services.AddTransient<IMovieService, MovieServiceImpl>();
builder.Services.AddTransient<IFranchiseService, FranchiseServiceImpl>();

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

// Scaffold-DbContext "Data Source = ACC-NLENNOX\SQLEXPRESS; Initial Catalog = PostgradEf; Integrated Security = True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
