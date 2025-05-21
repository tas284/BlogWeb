using BlogWeb.Data;
using BlogWeb.ViewModels;
using BlogWeb.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWeb.Controllers;

[Route("v1")]
public class PostController : ControllerBase
{
    [HttpGet("posts")]
    public async Task<IActionResult> Get(
        [FromServices] BlogDataContext context,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25)
    {
        try
        {
            var count = await context.Posts.CountAsync();
            var posts = await context
                .Posts
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Select(x => new ListPostsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    LasUpdateDate = x.LastUpdateDate,
                    Category = x.Category.Name,
                    Author = $"{x.Author.Name} ({x.Author.Email})"
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.LasUpdateDate)
                .ToListAsync();
            
            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page,
                pageSize,
                posts
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("Internal Server Error"));
        }
    }

    [HttpGet("posts/{id:int}")]
    public async Task<IActionResult> GetById(
        [FromServices] BlogDataContext context,
        int id)
    {
        try
        {
            var post = await context
                .Posts
                .AsNoTracking()
                .Include(x => x.Author)
                .ThenInclude(x => x.Roles)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
                return NotFound(new ResultViewModel<string>("Post not found"));

            return Ok(new ResultViewModel<Post>(post));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>($"Internal Server Error: {e.Message}"));
        }
    }
}