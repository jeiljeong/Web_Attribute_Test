using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // In a real application, this logic would save the user to a database
        // For simplicity, we'll just return a success message
        return Ok($"User '{userModel.Username}' created successfully!");
    }
}

public class CreateUserModel
{
    // Required username, minimum length of 3 characters
    [Required(ErrorMessage = "Username is required.")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters.")]
    public string? Username { get; set; }

    // Required age, must be at least 18 years old
    [Required(ErrorMessage = "Age is required.")]
    [Range(18, int.MaxValue, ErrorMessage = "Age must be at least 18.")]
    public int? Age { get; set; }

    // Required email, must be a valid email format
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }
}
