using md_apis_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace md_apis_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsumoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/consumo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consumo>>> GetAll()
        {
            var consumo = await _context.Consumos
                .Include(c => c.Veiculo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(consumo);
        }

        // GET: api/consumo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consumo>> GetById(int id)
        {
            var consumo = await _context.Consumos
                .Include(c => c.Veiculo)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (consumo == null)
                return NotFound("Consumo não encontrado");

            return Ok(consumo);
        }

        // POST: api/consumo
        [HttpPost]
        public async Task<ActionResult<Consumo>> Create([FromBody] Consumo model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Consumos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/consumo/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Consumo model)
        {
            if (id != model.Id)
                return BadRequest("ID da URL diferente do ID do objeto");

            var exists = await _context.Consumos.AnyAsync(c => c.Id == id);
            if (!exists)
                return NotFound("Consumo não encontrado");

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erro ao atualizar o consumo");
            }

            return NoContent();
        }

        // DELETE: api/consumo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var consumo = await _context.Consumos.FindAsync(id);

            if (consumo == null)
                return NotFound("Consumo não encontrado");

            _context.Consumos.Remove(consumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}