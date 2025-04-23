using System;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase 
{
    [HttpGet("test")]
    public IActionResult Test() 
    {
        return Ok(new { data = "Тест успешен!" });
    }
}