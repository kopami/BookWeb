using BookWebRazor.DAOs.Data;
using BookWebRazor.DAOs.DbInitializer;
using BookWebRazor.Repositories;
using BookWebRazor.Repositories.Interface;
using BookWebRazor.Services;
using BookWebRazor.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login"; // Redirect here if not authenticated
            options.LogoutPath = "/Account/Logout"; // Redirect here after logout
            options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect here if not authorized
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set cookie expiration
            options.SlidingExpiration = true; // Reset the expiration time on each request
        });

// Add services to the container.
builder.Services.AddRazorPages();

//Add session to service
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

#region Add Services

builder.Services.AddHttpContextAccessor(); // Required to access HttpContext in the view
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

#endregion

var app = builder.Build();

SeedDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // Enable session middleware

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
