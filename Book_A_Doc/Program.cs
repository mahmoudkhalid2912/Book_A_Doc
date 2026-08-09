using Book_A_Doc.Application.DependencyInjection;
using Book_A_Doc.DependencyInjection;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Infrastructre.Persistence;
using Book_A_Doc.Infrastructre.Persistence.Seed;
using Book_A_Doc.Infrastructure.Persistence.Seed;
using Hangfire;
using Hangfire.Dashboard;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard("/jobs", new DashboardOptions
    {
        Authorization = [
            new HangfireCustomBasicAuthenticationFilter{
                User = app.Configuration.GetValue<string>("HangfireSettings:UserName"),
                Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
            }
            ],
        DashboardTitle = "Book-A-Doc  Dashboard",
        IsReadOnlyFunc = (DashboardContext context) => true
    });
}
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<ApplicationRole>>();

    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<ApplicationUser>>();

    var context = scope.ServiceProvider
        .GetRequiredService<Book_A_Doc_Context>();

    await RoleSeeder.SeedAsync(roleManager);

    await UserSeeder.SeedAsync(
        userManager,
        context);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();