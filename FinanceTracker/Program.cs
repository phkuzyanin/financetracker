using FinanceTracker;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*
Response
app.Run(async (context) =>
    {
        var response = context.Response;
        response.Headers.ContentLanguage = "ru-Ru";
        response.Headers.ContentType = "text/html; charset=utf-8";
        response.Headers.Append("secret-id", "256");
        
        await response.WriteAsync("<h1>OK</h1>");
    }
);
*/
/*
Request
app.Run(async (context) =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        var stringBuilder = new System.Text.StringBuilder("<table>");
        foreach(var header in context.Request.Headers)
        {
            stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
        }
        stringBuilder.Append("</table>");
        await context.Response.WriteAsync(stringBuilder.ToString());
    }
);
*/
async Task responsePage(HttpContext context)
{
    var path = context.Request.Path;
    var fullPath = $"wwwroot/html/{path}";
    var request = context.Request;
    var response = context.Response;
    
    response.ContentType = "text/html; charset=utf-8";
    
    if(request.HasJsonContentType())
    {
        var message = "Некорректные данные";
        try
        {
            var user = await request.ReadFromJsonAsync<User>();
            if(user != null) message = $"Name: {user.Name} Age: {user.Age}";
        }
        catch{ }
        await response.WriteAsJsonAsync(new {text = message});
    }
    else if(path == "/postuser"){
        await responseForm(context);
    }
    else if(path == "/"){
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
