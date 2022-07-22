using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
namespace RazorPages.Pages.Categories
{
    [BindProperties]
    public class ListCategoriesModel : PageModel
    {
        IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();
        [TempData]
        public string Msg { get; set; }
        [TempData]
        public string Status { get; set; }
        public List<string> Categories = new List<string>();
        public async Task OnGet()
        {
            var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri(config["BaseAddress"]);
            var request = await client.GetStringAsync("/api/list-categories");
            if (request != null)
            {
                Categories = JsonSerializer.Deserialize<List<string>>(request);
            }
        }
    }
}
