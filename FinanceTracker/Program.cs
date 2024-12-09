using System.Text.Json;

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
    else if(path == "/updateChart") {
        Chart itemObj = new Chart();
        string item = JsonSerializer.Serialize(itemObj);
        Console.WriteLine(itemObj.count);
        Console.WriteLine(item);
        await response.WriteAsJsonAsync(item);
    }
    else{
        Console.WriteLine(10);
    }
});
app.Run();
class Chart{
    public int count {get;}
    public Chart(){
        count = 100;
    }
};