using CagnotteSolidaire.Blazor.Components;
using CagnotteSolidaire.Blazor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:5009/") });

builder.Services.AddScoped<UtilisateurApiService>();
builder.Services.AddScoped<AssociationApiService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ← ça OUI en .NET 8
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
