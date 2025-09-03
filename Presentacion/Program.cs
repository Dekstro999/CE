using Datos;
using Microsoft.EntityFrameworkCore;

using Presentacion.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//contextos

//configuracion de la conexion
var connectionString = builder.Configuration.GetConnectionString("ConexionBD");
?? throw new InvalidOperationException("Connection string 'ConexionBD' not found.");

//ContextBoundObject de base de datos (contextoBD) configurada en la capa de datos
builder.Services.AddDbContext<D_ContextoBD>(options =>options.UseSqlServer(connectionString));

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
