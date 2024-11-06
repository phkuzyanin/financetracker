var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


/*app.Run(async (context) =>
    {
        var response = context.Response;
        response.Headers.ContentLanguage = "ru-Ru";
        response.Headers.ContentType = "text/html; charset=utf-8";
        response.Headers.Append("secret-id", "256");
        
        await response.WriteAsync("<h1>OK</h1>");
    }
);*/
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
app.Run();
