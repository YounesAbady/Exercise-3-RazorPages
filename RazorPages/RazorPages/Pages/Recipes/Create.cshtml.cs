using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace RazorPages.Pages.Recipes
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private static string _baseAddress = "https://localhost:7018";
        public Models.Recipe Recipe { get; set; }
        public async Task<IActionResult> OnPost(string recipe)
        {
            if (ModelState.IsValid)
            {
                var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
                var client = httpClient.CreateClient();
                var jsonRecipe = JsonSerializer.Serialize(recipe);
                var content = new StringContent(jsonRecipe, Encoding.UTF8, "application/json");
                var request = await client.PostAsync($"{_baseAddress}/api/add-recipe/{recipe}", content);
                if (request.IsSuccessStatusCode)
                    return RedirectToPage("ListRecipes");
            }
            return RedirectToPage();
        }
    }
}
