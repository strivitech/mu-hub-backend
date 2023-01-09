using Duende.IdentityServer;

using Microsoft.AspNetCore.Authentication.Cookies;

using MuHub.IdentityProvider.Data;
using MuHub.IdentityProvider.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Serilog;

namespace MuHub.IdentityProvider;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        var migrationAssembly = typeof(HostingExtensions).Assembly.FullName;

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder => optionsBuilder.MigrationsAssembly(migrationAssembly)));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {// Add Identity options here
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(c =>
        {
            c.Cookie.Name = "MuHub.IdentityProvider";
            c.Cookie.HttpOnly = true;
            c.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            c.LoginPath = "/Account/Login";
            c.LogoutPath = "/Account/Logout";
        });
        
        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
                options.IssuerUri = "https://localhost:5001";
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = dbContextOptionsBuilder =>
                    dbContextOptionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(migrationAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = dbContextOptionsBuilder =>
                    dbContextOptionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(migrationAssembly));
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            })
            .AddAspNetIdentity<ApplicationUser>();

        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}
