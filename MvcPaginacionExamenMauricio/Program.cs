using Microsoft.EntityFrameworkCore;
using MvcPaginacionExamenMauricio.Data;
using MvcPaginacionExamenMauricio.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<RepositoryZapatillas>();
string connetionString = builder.Configuration.GetConnectionString("SqlServerHospital");
builder.Services.AddDbContext<ZapatillasContext>(options => options.UseSqlServer(connetionString));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
