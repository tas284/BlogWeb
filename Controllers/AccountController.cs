using BlogWeb.Data;
using BlogWeb.Extensions;
using BlogWeb.Models;
using BlogWeb.Services;
using BlogWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace BlogWeb.Controllers;

[ApiController]
[Route("v1")]
public class AccountController(TokenService tokenService) : ControllerBase
{
    [HttpPost("accounts")]
    public async Task<IActionResult> Post(
        [FromBody] RegisterViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email.Replace("@", "-").Replace(".", "-")
        };

        var password = PasswordGenerator.Generate(25);
        user.PasswordHash = PasswordHasher.Hash(password);

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.Email, password
            }));
        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewModel<string>(e.Message));
        }
    }

    [HttpPost("accounts/login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginViewModel model,
        [FromServices] BlogDataContext context)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(400, new ResultViewModel<string>(ModelState.GetErrors()));
            
            var user = await context.Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);
            
            if (user == null)
                return StatusCode(400, new ResultViewModel<string>($"Email {model.Email} not found"));
            
            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return StatusCode(400, new ResultViewModel<string>($"Password {model.Password} not match"));

            var token = tokenService.GenerateToken(user);
            return StatusCode(200, new ResultViewModel<string>(token, null));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }
}