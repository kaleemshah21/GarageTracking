using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GarageTracking.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddDbContext<TrackingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingContext") ?? throw new InvalidOperationException("Connection string 'TrackingContext' not found.")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // timeout timer
        options.SlidingExpiration = true; 
        options.Cookie.HttpOnly = true; 
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });


builder.Services.AddAuthorization(options =>
{
    // set the policy for the admin role
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));

    // set the policy for the user role
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User", "Admin"));
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TrackingContext>();

    
    context.Database.Migrate();
    DbInitializer.Initialize(context);
}


if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Error");
    app.UseHsts(); 
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint(); 
}

app.UseHttpsRedirection(); 
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapRazorPages();

app.Run();
