using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using STAJ.Frontend;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient BaseAddress'i API adresine göre ayarla
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5247/api/") });
await builder.Build().RunAsync();