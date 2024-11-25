var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions{ WebRootPath = "wwwroot/html"}
);

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
/*
app.Map("/index", appBuilder =>{
    appBuilder.Run(async context => await context.Response.SendFileAsync("wwwroot/html/index.html"));
});
*/
app.Run(async (context) => 
{
    var path = context.Request.Path;
    var response = context.Response;
    var request = context.Request;
    if(path == "/") await response.SendFileAsync("index.html");
    else;
});
app.Run();