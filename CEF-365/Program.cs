using CEF_365.Components;
using Datos.Contexto;
using Entidades.PerfilesDTO.PlanesDeEstudio;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


/*==========================================================================================*/
//                          Servicios agregados para el proyecto                            //
/*==========================================================================================*/

// Contextos

// Configuración de la cadena de conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("ContextDB")
    ?? throw new InvalidOperationException("Connection string 'ContextDB' not found.");

// Contexto de la base de datos confugurada en la capa de datos
builder.Services.AddDbContext<D_ContextDB>(options => options.UseSqlServer(connectionString));


// Contexto para la gestion de usuario creado para las cuentas individuales
// builder.Services.AddDbContext<Application>

// builder.Services.AddScoped<ICarrera>
builder.Services.AddScoped<Negocios.Repositorios.PlanesDeEstudio.CarreraNegocios>();

builder.Services.AddScoped<Datos.IRepositorios.PlanesDeEstudio.ICarreraRepositorios, Datos.Repositorios.PlanesDeEstudio.CarreraRepositorio>();
builder.Services.AddScoped<Datos.IRepositorios.PlanesDeEstudio.IPlanDeEstudioRepositorio, Datos.Repositorios.PlanesDeEstudio.PlanDeEstudioRepositorio>();

// En ConfigureServices el automapper
builder.Services.AddAutoMapper(cfg => { }, typeof(PlanEstudiosProfile).Assembly);


// Servicio para Quill Editor
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
