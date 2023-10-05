using Bloggie.Web.Controllers.Data;
using Bloggie.Web.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//injek DbContext untuk comunikasi dgn DB
builder.Services.AddDbContext<BloggieDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));

//injek repository untuk komunikasi antar app dgn db menggunakan db context
builder.Services.AddScoped<ITagRepository, TagRepositoryImpl>();
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
