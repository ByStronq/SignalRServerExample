using SignalRServerExample.Business;
using SignalRServerExample.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddTransient<MyBusiness>();

builder.Services.AddSignalR();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/myhub");
    endpoints.MapControllers();
});

app.Run();
