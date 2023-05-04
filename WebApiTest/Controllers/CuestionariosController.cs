using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassLibrary1;
using WebApiTest.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CuestionariosController : ControllerBase
    {
        private readonly CuestionarioContext _context;

        public CuestionariosController(CuestionarioContext context)
        {
            _context = context;
        }

        // GET: api/Cuestionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuestionario>>> GetCuestionarioItems()
        {
          if (_context.Cuestionario == null)
          {
              return NotFound();
          }
            return await _context.Cuestionario.ToListAsync();
        }

        // GET: api/Cuestionarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuestionario>> GetCuestionario(long id)
        {
          if (_context.Cuestionario == null)
          {
              return NotFound();
          }
            var cuestionario = await _context.Cuestionario.FindAsync(id);

            if (cuestionario == null)
            {
                return NotFound();
            }

            return cuestionario;
        }

        // PUT: api/Cuestionarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuestionario(long id, Cuestionario cuestionario)
        {
            if (id != cuestionario.Id)
            {
                return BadRequest();
            }

            _context.Entry(cuestionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuestionarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cuestionarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuestionario>> PostCuestionario(Cuestionario cuestionario)
        {
          if (_context.Cuestionario == null)
          {
              return Problem("Entity set 'CuestionarioContext.CuestionarioItems'  is null.");
          }
            _context.Cuestionario.Add(cuestionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuestionario", new { id = cuestionario.Id }, cuestionario);
        }

        // DELETE: api/Cuestionarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuestionario(long id)
        {
            if (_context.Cuestionario == null)
            {
                return NotFound();
            }
            var cuestionario = await _context.Cuestionario.FindAsync(id);
            if (cuestionario == null)
            {
                return NotFound();
            }

            _context.Cuestionario.Remove(cuestionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuestionarioExists(long id)
        {
            return (_context.Cuestionario?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
