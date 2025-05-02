using BlogWeb.Data;
using BlogWeb.Models;
using BlogWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogWeb.Extensions;

namespace BlogWeb.Controllers
{
    [ApiController]
    [Route("/v1")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context)
        {
            return Ok(new ResultViewModel<List<Category>>(await context.Categories.ToListAsync()));
        }

        [HttpGet("categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return Ok(new ResultViewModel<Category>("Erro ao buscar a categoria"));

            }
        }

        [HttpPost("categories")]
        public async Task<IActionResult> PostAsync(
            [FromServices] BlogDataContext context,
            [FromBody] EditorCategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

                var category = new Category
                {
                    Name = model.Name,
                    Slug = model.Slug
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"/v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException)
            {
                return BadRequest(new ResultViewModel<Category>("Erro ao cadastrar categoria"));
            }
        }

        [HttpPut("categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException)
            {
                return BadRequest(new ResultViewModel<Category>("Erro ao atualizar categoria"));
            }
        }

        [HttpDelete("categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] int id)
        {
            var category = context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(category));
        }
    }
}
