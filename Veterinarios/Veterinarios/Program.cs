using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Veterinarios.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
   options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// estamos a configurar o 'serviço' que deve usar a classe ApplicationUser como
// gestora dos dados da autenticação
builder.Services.AddDefaultIdentity<ApplicationUser>(
   options => options.SignIn.RequireConfirmedAccount = true
)
    .AddEntityFrameworkStores<ApplicationDbContext>();



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
