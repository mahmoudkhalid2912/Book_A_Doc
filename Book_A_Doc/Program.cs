using Book_A_Doc.Application.DependencyInjection;
using Book_A_Doc.DependencyInjection;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard("/jobs",new DashboardOptions
    {
        Authorization = [
            new HangfireCustomBasicAuthenticationFilter{
                User = app.Configuration.GetValue<string>("HangfireSettings:UserName"),
                Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
            }
            ],
       DashboardTitle = "Book-A-Doc  Dashboard"
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();