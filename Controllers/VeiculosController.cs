using md_apis_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace md_apis_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/veiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetAll()
        {
            var veiculos = await _context.Veiculos.ToListAsync();
            return Ok(veiculos);
        }

        // GET: api/veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetById(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if (veiculo == null)
                return NotFound("Veículo não encontrado");

            return Ok(veiculo);
        }

        // POST: api/veiculos
        [HttpPost]
        public async Task<ActionResult<Veiculo>> Create([FromBody] Veiculo model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/veiculos/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Veiculo model)
        {
            if (id != model.Id)
                return BadRequest("ID da URL diferente do ID do objeto");

            var exists = await _context.Veiculos.AnyAsync(v => v.Id == id);
            if (!exists)
                return NotFound("Veículo não encontrado");

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erro ao atualizar o veículo");
            }

            return NoContent();
        }

        // DELETE: api/veiculos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if (veiculo == null)
                return NotFound("Veículo não encontrado");

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}