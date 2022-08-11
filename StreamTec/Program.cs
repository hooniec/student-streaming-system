using Microsoft.EntityFrameworkCore;
using StreamTec.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the DatabaseContext
builder.Services.AddDbContext<WelTecContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));
// Add services to the session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(120);
});
// Add services to HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Build the web application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    // Getting the required Context data to create the database
    var context = services.GetRequiredService<WelTecContext>();
    // Creating the Database/Tables
    DBInitialize.Initialize(context);
}
 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Timetable}/{id?}");

app.Run();
