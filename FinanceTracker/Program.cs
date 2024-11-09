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
app.Run(async (context) => 
{
    var path = context.Request.Path;
    var fullPath = $"view/html/{path}";
    var response = context.Response;

    response.ContentType = "text/html; charset=utf-8";
    if(File.Exists(fullPath)){
        await response.SendFileAsync(fullPath);
    }
    else if(path == "/"){
        await response.SendFileAsync("view/html/index.html");
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsync("<h2>Not Found</h2>");
    }
});
app.Run();
