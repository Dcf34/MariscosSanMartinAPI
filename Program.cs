using MariscosSanMartinAPI;
using Microsoft.EntityFrameworkCore;
using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI.Middleware;
using System.Web.Services.Description;

var builder = WebApplication.CreateBuilder(args);

var ambiente = builder.Configuration["Ambiente"] ?? "";
var connectionString = builder.Configuration["ConnectionDB:MARISCOS:" + ambiente];

builder.Services.AddDbContext<AppDBContext>(opciones =>
{
    opciones.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    opciones.LogTo(Console.WriteLine, LogLevel.Information);
}
);

ServiceRegistrations.RegisterServices(builder.Services);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = new JsonNamingPolicyHandler());

//Cambia a minúsculas propiedades
builder.Services.AddControllers(options => options.Conventions.Add(new AddAuthorizeFiltersControllerConvention()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true); // Cambia a minúsculas URL

builder.Services.AddCors(option => option.AddPolicy("AllowAnyOrigin", builder => {
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true);
}));


// Valida el token proporcionado en el header
Auth.ValidaJWT(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Asegúrate de añadir esto en el lugar correcto
app.UseCors("AllowAnyOrigin");

// La autenticación debe ir antes de la autorización
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
