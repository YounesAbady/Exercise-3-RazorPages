using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();
        [TempData]
        public string Msg { get; set; }
        [TempData]
        public string Status { get; set; }
        public string Category { get; set; }
        public void OnGet(string category)
        {
            Category = category;
        }
        public async Task<IActionResult> OnPost()
        {
            var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri(config["BaseAddress"]);
            var request = await client.DeleteAsync($"/api/delete-category/{Category}");
            if (request.IsSuccessStatusCode)
            {
                Msg = "Successfully Deleted!";
                Status = "success";
                return RedirectToPage("ListCategories");
            }
            return RedirectToPage();
        }
    }
}
