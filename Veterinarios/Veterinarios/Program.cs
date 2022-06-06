using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Text.Json.Serialization;

using Veterinarios.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
   options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// estamos a configurar o 'serviço' que deve usar a classe ApplicationUser como
// gestora dos dados da autenticação
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() // ativa o uso de ROLES (perfis de utilizador)
                .AddEntityFrameworkStores<ApplicationDbContext>();


// inibir a referência circular num documento JSON
// https://gavilan.blog/2021/05/19/fixing-the-error-a-possible-object-cycle-was-detected-in-different-versions-of-asp-net-core/
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-6.0&tabs=aspnetcore2x&viewFallbackFrom=aspnetcore-2.1
builder.Services.Configure<IdentityOptions>(options => {
   // Default Lockout settings.
   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(50);
   options.Lockout.MaxFailedAccessAttempts = 5;
   options.Lockout.AllowedForNewUsers = true;
   // Password settings
   options.Password.RequireDigit = true;
   options.Password.RequiredLength = 6;
   options.Password.RequireNonAlphanumeric = false;
   options.Password.RequireUppercase = false;
   options.Password.RequireLowercase = true;
   options.Password.RequiredUniqueChars = 1;
   // Default User settings.
   options.User.AllowedUserNameCharacters =
           "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
   options.User.RequireUniqueEmail = false;
});


builder.Services.ConfigureApplicationCookie(options => {
   options.AccessDeniedPath = "/Identity/Account/AccessDenied";
   options.Cookie.Name = "YourAppCookieName";
   options.Cookie.HttpOnly = true;
   options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // especifica o tempo de inatividade que implica um 'logout' automático
   options.LoginPath = "/Identity/Account/Login";
   // ReturnUrlParameter requires 
   //using Microsoft.AspNetCore.Authentication.Cookies;
   options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
   options.SlidingExpiration = true;
});


builder.Services.AddControllersWithViews();

// permite criar 'vari�veis de sess�o' (funcionam como 'cookies')
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
   options.IdleTimeout = TimeSpan.FromSeconds(120);
   options.Cookie.HttpOnly = true;
   options.Cookie.IsEssential = true;
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
   app.UseMigrationsEndPoint();
}
else {
   app.UseExceptionHandler("/Home/Error");
   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// usar o servi�o das Vars. de Sess�o
app.UseSession();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
