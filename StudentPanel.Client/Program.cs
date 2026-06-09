using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentPanel.Client;
using StudentPanel.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<StudentsApiClient>(client => client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!));

builder.Services.AddScoped<ObservedStudentsState>();

await builder.Build().RunAsync();