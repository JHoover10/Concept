using Concept;
using Concept.Data;
using Concept.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddScoped(typeof(ConceptViewModel));
builder.Services.AddScoped(typeof(MainLayoutViewModel));
builder.Services.AddScoped(typeof(LocalDataStore));

await builder.Build().RunAsync();
