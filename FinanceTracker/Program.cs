using System.Text.Json;

var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions{ WebRootPath = "wwwroot/html"}
);

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.Run(async (context) => 
{
    var path = context.Request.Path;
    var response = context.Response;
    var request = context.Request;
});
app.Run();