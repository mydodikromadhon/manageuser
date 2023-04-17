using CRUD.ManagementUser.Application;
using CRUD.ManagementUser.Bsui.Areas.Identity;
using CRUD.ManagementUser.Infrastructure;
using CRUD.ManagementUser.Infrastructure.Logging;
using CRUD.ManagementUser.Infrastructure.Persistence;
using CRUD.ManagementUser.Application.Common.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using CRUD.ManagementUser.Bsui.Services;
using CRUD.ManagementUser.Bsui.Common.Authorizations;
using CRUD.ManagementUser.Domain.Entities;

Console.WriteLine($"Starting {CommonValueFor.EntryAssemblySimpleName}...");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLoggingService();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddBsui(builder.Configuration);
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
await scope.ServiceProvider.ApplyDatabaseMigrationAsync();
await scope.ServiceProvider.ApplyDatabaseSeedingAsync();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Running {AssemblyName}", CommonValueFor.EntryAssemblySimpleName);

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}

app.UseBsui(app.Configuration);
app.UseHttpsRedirection();  
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseInfrastructure(builder.Configuration);
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
