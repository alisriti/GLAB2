using GLab.Apps.Accounts;
using GLab.Apps.Laboratoires;
using GLab.Apps.Shared;
using GLab.Apps.Users;
using GLab.Impl.Services.Accounts;
using GLab.Impl.Services.Laboratoires;
using GLab.Impl.Services.Shared;
using GLab.Impl.Services.Users;
using GLAB.Infra.Storages;
using Users.Infra.Storages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<ILabService, LaboratoireService>();
builder.Services.AddScoped<ILabStorage, LabStorage>();
builder.Services.AddScoped<IUnivService, UnivService>();

builder.Services.AddScoped<IUserStorage, UserStorage>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddHttpClient("accounts", (client) =>
{
    client.BaseAddress = new Uri("api/Account");
});

builder.Services.AddAuthentication("Cookies").AddCookie(options =>
{
    options.Cookie.Name = "glab.cookie";
});
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

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();