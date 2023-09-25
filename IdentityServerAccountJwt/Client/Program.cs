using Blazored.LocalStorage;
using IdentityServerAccountJwt.Client;
using IdentityServerAccountJwt.Client.Authentication;
using IdentityServerAccountJwt.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStatesClass>();
builder.Services.AddScoped<IAuthenticationRegister,AuthenticationRegister>();
builder.Services.AddScoped<IRoleService,RoleService>();
builder.Services.AddScoped<IUserRole,UserRole>();
builder.Services.AddScoped<IDashboardservice,DashboardService>();
builder.Services.AddSingleton<LoadingContainer>();

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
