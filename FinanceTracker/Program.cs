var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions{ WebRootPath = "wwwroot/html"}
);

var app = builder.Build();
app.UseStaticFiles();
/*
app.Map("/index", appBuilder =>{
    appBuilder.Run(async context => await context.Response.SendFileAsync("wwwroot/html/index.html"));
});
*/
app.Run(async (context) => await context.Response.WriteAsync("Hello World!"));
app.Run();