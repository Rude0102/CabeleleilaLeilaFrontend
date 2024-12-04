using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class AdminModel : PageModel
{
    private readonly HttpClient _httpClient;

    public AdminModel(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("CabeleleilaApi");
    }

    public List<Agendamento> Agendamentos { get; set; } = new();

    public async Task OnGetAsync()
    {
        var response = await _httpClient.GetAsync("api/agendamentos");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            Agendamentos = JsonSerializer.Deserialize<List<Agendamento>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }

    public async Task<IActionResult> OnPostConfirmarAsync(int id)
    {
        var response = await _httpClient.PostAsync($"api/agendamentos/{id}/confirmar", null);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }

        ModelState.AddModelError(string.Empty, "Erro ao confirmar agendamento.");
        return Page();
    }
}
