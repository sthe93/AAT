using AAT;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AAT.Pages;
using AAT.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<EventClientService>();
builder.Services.AddScoped<EventRegistrationClientService>();

// Add session state services
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache(); // Use an in-memory cache for sessions

await builder.Build().RunAsync();
