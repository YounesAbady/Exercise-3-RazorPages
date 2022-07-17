using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace RazorPages.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private static string _baseAddress = "https://localhost:7018";
        public static string OldCategory;
        public static int Postion;
        public string Category { get; set; }
        public void OnGet(string category, int postion)
        {
            Postion = postion;
            OldCategory = category;
            Category = category;
        }
        public async Task<IActionResult> OnPost(string category)
        {
            var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            var jsonCategory = JsonSerializer.Serialize(category);
            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");
            var request = await client.PutAsync($"{_baseAddress}/api/update-category/{Postion}/{category}", content);
            if (request.IsSuccessStatusCode)
                return RedirectToPage("ListCategories");
            return RedirectToPage();
        }
    }
}
