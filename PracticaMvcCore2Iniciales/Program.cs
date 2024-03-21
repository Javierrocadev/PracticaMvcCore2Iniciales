using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();




//HABILITAMOS LA SEGURIDAD EN SERVICIOS
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();



// Add services to the container.
builder.Services.AddControllersWithViews();










string connectionString =
    builder.Configuration.GetConnectionString("SqlHospital");
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddTransient<RepositoryLibros>();
builder.Services.AddTransient<RepositoryGeneros>();
builder.Services.AddTransient<RepositoryVistaPedidos>();
builder.Services.AddDbContext<UsuariosContext>
    (options => options.UseSqlServer(connectionString));


//PERSONALIZAMOS NUESTRAS RUTAS
builder.Services.AddControllersWithViews
    (options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//--------------------4----------------------
app.UseAuthentication();
app.UseAuthorization();
//--------------------5----------------------
app.UseSession();
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();