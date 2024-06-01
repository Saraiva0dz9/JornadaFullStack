using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Final.Web;
using MudBlazor.Services;
using Final.Core;
using Final.Core.Services;
using Final.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddHttpClient(WebConfiguration.HttpClientName, opt =>
{
    opt.BaseAddress = new Uri(Configurations.BackendUrl);
});

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

await builder.Build().RunAsync();
