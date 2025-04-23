using System.Text.Json;

var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions{ WebRootPath = "wwwroot/html"}
);

builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.Run();