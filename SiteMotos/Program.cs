using Microsoft.AspNetCore.Authentication.Cookies;
using SiteMotos.Services.Auth;
using SiteMotos.Services.Motos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("Motos", c =>
    c.BaseAddress = new Uri(builder.Configuration["UriApis:Motos"]));

builder.Services.AddHttpClient("Auth", c =>
    c.BaseAddress = new Uri(builder.Configuration["UriApis:Auth"]));

builder.Services.AddScoped<IMotosService, MotosService>();
builder.Services.AddScoped<IAutentificador, Autentifacador>();

// --- Configuração da Autenticação por Cookie no Frontend MVC ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "JWT"; // Nome do cookie da sua aplicação web
        options.Cookie.HttpOnly = true; // Crucial: Impede acesso via JavaScript (XSS)
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Crucial em produção: Apenas via HTTPS
        options.Cookie.SameSite = SameSiteMode.Lax; // Para proteção CSRF
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Tempo de vida do cookie (pode ser ajustado)
        options.SlidingExpiration = true; // Renova o cookie em cada requisição
        options.LoginPath = "/Auth/Login"; // URL para redirecionar em caso de não autenticação
        options.LogoutPath = "/Auth/Logout"; // URL para redirecionar em caso de logout
    });

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
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();

