using Basic.Cookies.Controllers;
using Basic.Cookies.CustomAuthProvider;
using Basic.Cookies.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "basic.cookie";
        config.LoginPath = "/Home/Authenticate";
        config.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("Claim.Name", policyBuilder =>
    {
        policyBuilder.RequireCustomClaim(ClaimTypes.Name);
    });
});

builder.Services.AddSingleton<IAuthorizationPolicyProvider, CPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, ClaimHandler>();
builder.Services.AddScoped<IAuthorizationHandler, CookieJarHandler>();
builder.Services.AddScoped<IAuthorizationHandler, SecurityLevelHandler>();

builder.Services.AddControllersWithViews(config =>
{
    var defaultAuthBuilder = new AuthorizationPolicyBuilder();
    var defaultAuthPolicy = defaultAuthBuilder
        .RequireAuthenticatedUser()
        .Build();

    // global authorization filter
    //config.Filters.Add(new AuthorizeFilter(defaultAuthPolicy));
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
