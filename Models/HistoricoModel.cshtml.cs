using Microsoft.AspNetCore.Mvc.RazorPages;


public class HistoricoModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly IHttpClientFactory _clientFactory = clientFactory;

    public List<Agendamento>? Agendamentos { get; set; }
    
    public async Task OnGetAsync()
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<List<Agendamento>>("https://localhost:5001/api/agendamento");
        Agendamentos = response ?? new List<Agendamento>();
    }
}
