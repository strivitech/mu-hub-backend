using Duende.IdentityServer;

using Microsoft.AspNetCore.Authentication.Cookies;

using MuHub.IdentityProvider.Data;
using MuHub.IdentityProvider.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MuHub.IdentityProvider.Configurations;
using MuHub.IdentityProvider.Configurations.ApplicationUser;
using MuHub.IdentityProvider.Configurations.Auth;
using MuHub.IdentityProvider.Configurations.Store;
using MuHub.IdentityProvider.Services;

using Serilog;

namespace MuHub.IdentityProvider;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var migrationAssembly = typeof(HostingExtensions).Assembly.FullName;

        void DbOptionsBuilder(DbContextOptionsBuilder options) => options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            optionsBuilder => optionsBuilder.MigrationsAssembly(migrationAssembly));

        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<ApplicationDbContext>(DbOptionsBuilder);

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = PasswordConfiguration.RequireDigit;
                options.Password.RequireLowercase = PasswordConfiguration.RequireLowercase;
                options.Password.RequireNonAlphanumeric = PasswordConfiguration.RequireNonAlphanumeric;
                options.Password.RequireUppercase = PasswordConfiguration.RequireUppercase;
                options.Password.RequiredLength = PasswordConfiguration.RequiredLength;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(c =>
        {
            c.Cookie.Name = CookieAuthenticationConfiguration.CookieName;
            c.Cookie.HttpOnly = true;
            c.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            c.LoginPath = CookieAuthenticationConfiguration.LoginPath;
            c.LogoutPath = CookieAuthenticationConfiguration.LogoutPath;
        });
        
        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = IdentityServerOptionsConfiguration.EmitStaticAudienceClaim;
                options.IssuerUri = IdentityServerOptionsConfiguration.IssuerUri;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = DbOptionsBuilder;
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = DbOptionsBuilder;
                options.EnableTokenCleanup = OperationalStoreConfiguration.EnableTokenCleanup;
                options.TokenCleanupInterval = OperationalStoreConfiguration.TokenCleanupInterval;
            })
            .AddProfileService<ProfileService>()
            .AddAspNetIdentity<ApplicationUser>();

        GoogleConfiguration googleConfiguration = builder.Configuration.GetSection(GoogleConfiguration.SectionName)
            .Get<GoogleConfiguration>();
        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.ClientId = googleConfiguration.ClientId;
                options.ClientSecret = googleConfiguration.ClientSecret;
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
