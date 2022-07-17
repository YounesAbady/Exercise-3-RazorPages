using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private static string _baseAddress = "https://localhost:7018";
        public string Category { get; set; }
        public void OnGet(string category)
        {
            Category = category;
        }
        public async Task<IActionResult> OnPost()
        {
            var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            var request = await client.DeleteAsync($"{_baseAddress}/api/delete-category/{Category}");
            if (request.IsSuccessStatusCode)
                return RedirectToPage("ListCategories");
            return RedirectToPage();
        }
    }
}
