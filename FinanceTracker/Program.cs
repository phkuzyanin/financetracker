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
/*async Task responsePage(HttpContext context)
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
*/
using System.Text.RegularExpressions;
 
// начальные данные
List<Person> users = new List<Person> 
{ 
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
};
 
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
 
app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;
    //string expressionForNumber = "^/api/users/([0-9]+)$";   // если id представляет число
 
    // 2e752824-1657-4c7f-844b-6ec2e168e99c
    string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
    if (path == "/api/users" && request.Method=="GET")
    {
        await GetAllPeople(response); 
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "GET")
    {
        // получаем id из адреса url
        string? id = path.Value?.Split("/")[3];
        await GetPerson(id, response);
    }
    else if (path == "/api/users" && request.Method == "POST")
    {
        await CreatePerson(response, request);
    }
    else if (path == "/api/users" && request.Method == "PUT")
    {
        await UpdatePerson(response, request);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
    {
        string? id = path.Value?.Split("/")[3];
        await DeletePerson(id, response);
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});
 
app.Run();
 
// получение всех пользователей
async Task GetAllPeople(HttpResponse response)
{
    await response.WriteAsJsonAsync(users);
}
// получение одного пользователя по id
async Task GetPerson(string? id, HttpResponse response)
{
    // получаем пользователя по id
    Person? user = users.FirstOrDefault((u) => u.Id == id);
    // если пользователь найден, отправляем его
    if (user != null)
        await response.WriteAsJsonAsync(user);
    // если не найден, отправляем статусный код и сообщение об ошибке
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
    }
}
 
async Task DeletePerson(string? id, HttpResponse response)
{
    // получаем пользователя по id
    Person? user = users.FirstOrDefault((u) => u.Id == id);
    // если пользователь найден, удаляем его
    if (user != null)
    {
        users.Remove(user);
        await response.WriteAsJsonAsync(user);
    }
    // если не найден, отправляем статусный код и сообщение об ошибке
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
    }
}
 
async Task CreatePerson(HttpResponse response, HttpRequest request)
{
    try
    {
        // получаем данные пользователя
        var user = await request.ReadFromJsonAsync<Person>();
        if (user != null)
        {
            // устанавливаем id для нового пользователя
            user.Id = Guid.NewGuid().ToString();
            // добавляем пользователя в список
            users.Add(user);
            await response.WriteAsJsonAsync(user);
        }
        else
        {
            throw new Exception("Некорректные данные");
        }
    }
    catch (Exception)
    {
        response.StatusCode = 400;
        await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
    }
}
 
async Task UpdatePerson(HttpResponse response, HttpRequest request)
{
    try
    {
        // получаем данные пользователя
        Person? userData = await request.ReadFromJsonAsync<Person>();
        if (userData != null)
        {
            // получаем пользователя по id
            var user = users.FirstOrDefault(u => u.Id == userData.Id);
            // если пользователь найден, изменяем его данные и отправляем обратно клиенту
            if (user != null)
            {
                user.Age = userData.Age;
                user.Name = userData.Name;
                await response.WriteAsJsonAsync(user);
            }
            else
            {
                response.StatusCode = 404;
                await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
            }
        }
        else
        {
            throw new Exception("Некорректные данные");
        }
    }
    catch (Exception)
    {
        response.StatusCode = 400;
        await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
    }
}
public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}