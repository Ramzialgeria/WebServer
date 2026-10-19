using ClientMonitoring.Web.Data;
using ClientMonitoring.Web.Domain.Identity;
using ClientMonitoring.Web.Hubs;
using ClientMonitoring.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// Database
// ---------------------------------------------------------
builder.Services.AddDbContext<StructureDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("StructureDb")));

//builder.Services.AddDbContext<ElectionDbContext>(options =>
//    options.UseNpgsql(
//        builder.Configuration.GetConnectionString("ElectionDb")));

builder.Services.AddDbContext<ApplicationDbContext1>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Existing election database — read only
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "PostgresqlReadAppConnection")));
builder.Services.AddDbContext<ReadApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "PostgresqlReadAppConnection")));

// Existing identity database
builder.Services.AddDbContext<
    IdentityDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "IdentityConnection")));


// ---------------------------------------------------------
// ASP.NET Core Identity - Web users
// ---------------------------------------------------------

builder.Services
    .AddIdentityCore<ClientMonitoring.Web.Domain.Identity.AppUser>()
    .AddRoles<ClientMonitoring.Web.Domain.Identity.AppRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

//builder.Services
//    .AddIdentityCore<ApplicationUser>(options =>
//    {
//        options.Password.RequiredLength = 8;
//        options.Password.RequireNonAlphanumeric = false;

//        options.SignIn.RequireConfirmedAccount = false;
//    })
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddSignInManager();

// ---------------------------------------------------------
// Client secret hashing
// ---------------------------------------------------------

builder.Services.AddScoped<IPasswordHasher<Client>, PasswordHasher<Client>>();


// ---------------------------------------------------------
// JWT - WinForms clients
// ---------------------------------------------------------

var jwtSection = builder.Configuration.GetSection("Jwt");

var jwtKey = jwtSection["Key"]
    ?? throw new InvalidOperationException("JWT Key is missing.");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            IdentityConstants.ApplicationScheme;

        options.DefaultSignInScheme =
            IdentityConstants.ApplicationScheme;

        options.DefaultChallengeScheme =
            IdentityConstants.ApplicationScheme;
    })
    .AddCookie(
        IdentityConstants.ApplicationScheme,
        options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        })
    .AddJwtBearer(
        JwtBearerDefaults.AuthenticationScheme,
        options =>
        {
            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey))
                };
        });

builder.Services.AddAuthorization();


// ---------------------------------------------------------
// MVC
// ---------------------------------------------------------

builder.Services.AddControllersWithViews();



// ---------------------------------------------------------
// SignalR
// ---------------------------------------------------------

builder.Services.AddSignalR();

// Activation files (keys + generator)
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ClientMonitoring.Web.Services.ActivationIssuer>();


var app = builder.Build();


// ---------------------------------------------------------
// Database migration + seed
// ---------------------------------------------------------

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var db = services.GetRequiredService<ApplicationDbContext>();

//    await db.Database.MigrateAsync();


//    // -----------------------------------------------------
//    // Seed admin user
//    // -----------------------------------------------------

//    //var userManager =
//    // services.GetRequiredService<UserManager<ApplicationUser>>();

//    //var admin = await userManager.FindByNameAsync("admin");

//    //if (admin == null)
//    //{
//    //    admin = new ApplicationUser
//    //    {
//    //        UserName = "admin",
//    //        Email = "admin@clientmonitoring.local",
//    //        EmailConfirmed = true
//    //    };

//    //    var result = await userManager.CreateAsync(
//    //        admin,
//    //        "Admin1234");

//    //    if (!result.Succeeded)
//    //    {
//    //        var errors = string.Join(
//    //            ", ",
//    //            result.Errors.Select(x => x.Description));

//    //        throw new Exception(
//    //            $"Unable to create admin user: {errors}");
//    //    }
//    //}
//    //else
//    //{
//    //    // Make sure the existing user has the expected email
//    //    admin.Email = "admin@clientmonitoring.local";
//    //    admin.EmailConfirmed = true;

//    //    var updateResult = await userManager.UpdateAsync(admin);

//    //    if (!updateResult.Succeeded)
//    //    {
//    //        var errors = string.Join(
//    //            ", ",
//    //            updateResult.Errors.Select(x => x.Description));

//    //        throw new Exception(
//    //            $"Unable to update admin user: {errors}");
//    //    }

        
//    //}


//    // -----------------------------------------------------
//    // Seed test client
//    // -----------------------------------------------------

//    //if (!await db.Clients.AnyAsync())
//    //{
//    //    var client = new Client
//    //    {
//    //        ClientCode = "PC-001",
//    //        //MachineName = "PC-001"
//    //    };

//    //    client.ClientSecretHash =
//    //        new PasswordHasher<Client>()
//    //            .HashPassword(
//    //                client,
//    //                "Client1234!");

//    //    db.Clients.Add(client);

//    //    await db.SaveChangesAsync();
//    //}
//}


// ---------------------------------------------------------
// HTTP pipeline
// ---------------------------------------------------------

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


// MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// API
app.MapControllers();



// SignalR
app.MapHub<MonitoringHub>("/hubs/monitoring");


app.Run();