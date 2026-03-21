using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.Repositories;
using DemoMVC.Services;
using Microsoft.AspNetCore.Identity;
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

// Configurar Identity
builder.Services.AddIdentity<Persona, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccesoDenegado";
});

// Add repositories to the container
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IMascotaRepository, MascotaRepository>();
builder.Services.AddScoped<IAdopcionRepository, AdopcionRepository>();

// Register application services
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IMascotaService, MascotaService>();
builder.Services.AddScoped<IAdopcionService, AdopcionService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAccountService, AccountService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new PersonaModelBinderProvider());
});

var app = builder.Build();

// Seed de roles y usuario administrador
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = services.GetRequiredService<UserManager<Persona>>();

    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
        }
    }

    var adminEmail = "admin@demo.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new Persona
        {
            UserName = adminEmail,
            Email = adminEmail,
            Cedula = "000000000",
            Nombre = "Administrador",
            Edad = 30,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, "Admin123!");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
