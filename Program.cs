using DemoMVC.Data;
using DemoMVC.Repositories;
using DemoMVC.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Configure database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

// Add repositories to the container
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IMascotaRepository, MascotaRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new PersonaModelBinderProvider());
});

// Register application services
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IMascotaService, MascotaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
