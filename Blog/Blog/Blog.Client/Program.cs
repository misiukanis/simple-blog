using Blazored.Modal;
using Blazored.Toast;
using Blog.Client;
using Blog.Client.HttpClients;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddHttpClient<BlogHttpClient>(c =>
{
    c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddBlazoredModal();

builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
