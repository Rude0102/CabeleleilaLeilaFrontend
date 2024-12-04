using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


public class AgendarModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly IHttpClientFactory _clientFactory = clientFactory;

    public List<Servico>? Servicos { get; set; }
    
    public async Task OnGetAsync()
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<List<Servico>>("https://localhost:5001/api/servicos");
        Servicos = response ?? new List<Servico>();
    }

    public async Task<IActionResult> OnPostAsync(int clienteId, List<int> servicoIds, DateTime dataHora)
    {
        var client = _clientFactory.CreateClient();
        var agendamento = new Agendamento
        {
            ClienteId = clienteId,
            DataHora = dataHora,
            Servicos = servicoIds.Select(id => new Servico { Id = id }).ToList()
        };
        
        var response = await client.PostAsJsonAsync("https://localhost:5001/api/agendamento", agendamento);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("/Sucesso");
        }
        else
        {

            return RedirectToPage("/Erro");
        }
    }
}

public class Agendamento
{
    public Agendamento()
    {
    }

    public int ClienteId { get; set; }
    public DateTime DataHora { get; set; }
    public List<Servico> Servicos { get; set; }
}

public class Servico
{
    public int Id { get; set; }
    public string? Nome { get; set; }
}
