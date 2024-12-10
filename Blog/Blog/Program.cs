using Blazored.Modal;
using Blazored.Toast;
using Blog;
using Blog.Application;
using Blog.Components;
using Blog.Components.Account;
using Blog.ExceptionHandlers;
using Blog.Infrastructure;
using Blog.Infrastructure.Identity;
using Blog.Infrastructure.Persistence;
using Blog.Shared.Settings;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddScoped<IdentityUserAccessor>();
    builder.Services.AddScoped<IdentityRedirectManager>();
    builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

    builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddIdentityCookies();

    builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddHttpClients(builder.Configuration);

    builder.Services.AddExceptionHandler<CustomExceptionHandler>();

    // NLog: 
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // AutoMapper:
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // MediatR:
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

    // Blazored.Toast:
    builder.Services.AddBlazoredToast();

    // Blazored.Modal:
    builder.Services.AddBlazoredModal();

    // Swashbuckle.AspNetCore:
    builder.Services.AddSwaggerGen();

    builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));
    builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection(nameof(ApplicationSettings)));


    var app = builder.Build();


    // Seed database:
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        DatabaseInitializer.SeedDatabaseAsync(services).Wait();
    }


    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler(opt => { });
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
    }


    app.UseAntiforgery();

    app.MapStaticAssets();
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    app.MapAdditionalIdentityEndpoints();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}