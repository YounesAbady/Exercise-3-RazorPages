using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
namespace RazorPages.Pages.Categories
{
    [BindProperties]
    public class ListCategoriesModel : PageModel
    {
        private static string _baseAdress = "https://localhost:7018";
        public List<string> Categories = new List<string>();
        public async Task OnGet()
        {
            var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            var request = await client.GetStringAsync($"{_baseAdress}/api/list-categories");
            if (request != null)
            {
                Categories = JsonSerializer.Deserialize<List<string>>(request);
            }
        }
    }
}
