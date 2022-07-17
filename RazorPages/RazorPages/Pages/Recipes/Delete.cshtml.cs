using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace RazorPages.Pages.Recipes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private static string _baseAddress = "https://localhost:7018";
        public Models.Recipe Recipe { get; set; }
        public async Task OnGet(Guid id)
        {
            var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            var request = await client.GetStringAsync($"{_baseAddress}/api/get-recipe/{id}");
            if (request != null)
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
                };
                Recipe = JsonSerializer.Deserialize<Models.Recipe>(request, options);
            }
        }
        public async Task<IActionResult> OnPost()
        {
            var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            var request = await client.DeleteAsync($"{_baseAddress}/api/delete-recipe/{Recipe.Id}");
            if (request.IsSuccessStatusCode)
                return RedirectToPage("ListRecipes");
            return RedirectToPage();
        }
    }
}
