using BeanOfTheDay.Components;
using BeanOfTheDay.Library.Services;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents().AddCircuitOptions(o => o.DetailedErrors = true); ;

builder.Services.AddScoped<BeanService>();
builder.Services.AddSingleton<HttpClient>();

//Syncfusion
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwNzc4MkAzMjM4MmUzMDJlMzBWWGdxdXk0WHp3eFNndE15QzRXbXZBaEE4RnVCWWh4YVcwUjFTZDJQSmlvPQ==");
builder.Services.AddSyncfusionBlazor();

builder.Services.AddSingleton<SfDialogService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
