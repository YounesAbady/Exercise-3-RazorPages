using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace RazorPages.Pages.Recipes
{
    [BindProperties]
    public class ListRecipesModel : PageModel
    {
        private static string _baseAddress = "https://localhost:7018";
        public List<Models.Recipe> Recipes { get; set; } = new List<Models.Recipe>();
        public async Task OnGet()
        {
            var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            var request = await client.GetStringAsync($"{_baseAddress}/api/list-recipes");
            if (request != null)
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
                };
                Recipes = JsonSerializer.Deserialize<List<Models.Recipe>>(request,options);
            }
        }
    }
}
