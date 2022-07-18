using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace RazorPages.Pages.Recipes
{
    [BindProperties]
    public class EditModel : PageModel
    {
        IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();
        public List<string> Categories = new List<string>();

        public Models.Recipe Recipe { get; set; }
        public async Task OnGet(Guid id)
        {
            var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri(config["BaseAddress"]);
            var request = await client.GetStringAsync($"/api/get-recipe/{id}");
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

            request = await client.GetStringAsync("/api/list-categories");
            if (request != null)
            {
                Categories = JsonSerializer.Deserialize<List<string>>(request);
            }
        }
        public async Task<IActionResult> OnPost(Models.Recipe recipe, Guid id)
        {
            if (ModelState.IsValid)
            {
                var ing = recipe.Ingredients[0].Split("\r\n");
                recipe.Ingredients = ing.ToList();
                var ins = recipe.Instructions[0].Split("\r\n");
                recipe.Instructions = ins.ToList();
                var httpClient = HttpContext.RequestServices.GetService<IHttpClientFactory>();
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri(config["BaseAddress"]);
                var jsonRecipe = JsonSerializer.Serialize(recipe);
                var content = new StringContent(jsonRecipe, Encoding.UTF8, "application/json");
                var request = await client.PutAsync($"/api/update-recipe/{jsonRecipe}/{id}", content);
                if (request.IsSuccessStatusCode)
                    return RedirectToPage("ListRecipes");
            }
            return RedirectToPage();
        }
    }
}
