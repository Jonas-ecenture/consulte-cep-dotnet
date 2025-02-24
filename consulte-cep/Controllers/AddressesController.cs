using consulte_cep.Data;
using consulte_cep.Dto;
using consulte_cep.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace consulte_cep.Controllers
{
    [Route("/api/enderecos")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public AddressesController(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        // GET: api/enderecos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressConsulted>>> GetAddressesConsulted()
        {
            return await _context.Addresses.ToListAsync();
        }

        [HttpGet("buscar-endereco/{cep}")]
        public async Task<ActionResult<AddressConsulted>> GetSearchAddress(string cep)
        {
            string normalizedCep = new string(cep.Where(char.IsDigit).ToArray());

            if (normalizedCep.Length != 8)
            {
                return BadRequest("Cep Inválido! Deve conter 8 dígitos");
            }

            string formattedCep = $"{normalizedCep.Substring(0, 5)}-{normalizedCep.Substring(5, 3)}";

            AddressConsulted existAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.Postcode == formattedCep);

            if (existAddress != null)
            {
                return existAddress;
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Cep não encontrado");
            }

            var stringResponse = await response.Content.ReadAsStringAsync();

            var viaCepData = JsonSerializer.Deserialize<ViaCepResponse>(
                stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            if (viaCepData == null)
            {
                return BadRequest("Erro ao processar resposta do ViaCep. Contate o suporte");
            }

            var address = new AddressConsulted(
                viaCepData.Cep,
                viaCepData.Logradouro,
                viaCepData.Complemento,
                viaCepData.Bairro,
                viaCepData.Localidade,
                viaCepData.Uf
            );

            await _context.Addresses.AddAsync(address);

            await _context.SaveChangesAsync();

            return address;
        }
    }
}
