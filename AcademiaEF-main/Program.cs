using Academia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // Adicione este namespace

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Aula_Session";
    options.IdleTimeout = TimeSpan.FromSeconds(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = false; // Ativa a validação de escopo. False evita erro de BD inexistente
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuração da Entity Framework Core
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration["Data:Academia_DB:ConnectionString"],

    // evita que o BD não seja criado por problemas de timeout com o servidor
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Session}/{action=Index}/{id?}");

// Obtenha o IServiceProvider e passe-o para EnsurePopulated
var serviceProvider = app.Services;
SeedData.EnsurePopulated(serviceProvider);

app.Run();
