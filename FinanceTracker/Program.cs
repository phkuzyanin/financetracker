using FinanceTracker;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

async Task responsePage(HttpContext context)
{
    var path = context.Request.Path;
    var fullPath = $"wwwroot/html/{path}";
    var request = context.Request;
    var response = context.Response;
    
    response.ContentType = "text/html; charset=utf-8";
    

    if(path == "/"){
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("wwwroot/html/index.html");
    
    }
    else if(File.Exists(fullPath)){
        await response.SendFileAsync(fullPath);
    }
    else
    {
        response.StatusCode = 404;
        await response.SendFileAsync("wwwroot/html/error.html");
    }

};
async Task responseForm(HttpContext context)
{
    context.Response.ContentType ="text/html; charset=utf-8";
    var form = context.Request.Form;
    string? name = form["name"];
    string? age = form["age"];
    await context.Response.WriteAsync($"<div><p>Name: {name}</p><p>Age: {age}</p></div>");
}
app.Run(async (context) => 
{
    await responsePage(context);
});
app.Run();