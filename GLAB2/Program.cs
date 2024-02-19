using GLab.Apps.Laboratoires;
using GLab.Apps.Shared;
using GLab.Impl.Services.Laboratoires;
using GLab.Impl.Services.Shared;
using GLAB.Infra.Storages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<ILabService, LaboratoireService>();
builder.Services.AddScoped<ILabStorage, LabStorage>();
builder.Services.AddScoped<IUnivService, UnivService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();