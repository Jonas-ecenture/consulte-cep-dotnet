using consulte_cep.Data;
using consulte_cep.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace consulte_cep.Controllers
{
    [Route("/api/enderecos")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/enderecos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressConsulted>>> GetAddressesConsulted()
        {
            return await _context.Addresses.ToListAsync();
        }
    }
}
