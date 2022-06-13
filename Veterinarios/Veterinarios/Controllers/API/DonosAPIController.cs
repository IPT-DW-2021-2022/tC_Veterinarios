using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Veterinarios.Data;
using Veterinarios.Models;

namespace Veterinarios.Controllers.API {
   [Route("api/[controller]")]
   [ApiController]
   public class DonosAPIController : ControllerBase {
      private readonly ApplicationDbContext _context;

      public DonosAPIController(ApplicationDbContext context) {
         _context = context;
      }

      // GET: api/DonosAPI
      [HttpGet]
      public async Task<ActionResult<IEnumerable<DonoViewModel>>> GetDonos() {
         return await _context.Donos
                              .OrderBy(d => d.Nome)
                              .Select(d => new DonoViewModel {
                                 Id = d.Id,
                                 Nome = d.Nome + " (NIF: " + d.NIF + ")"
                              })
                               .ToListAsync();
      }

      // GET: api/DonosAPI/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Donos>> GetDonos(int id) {
         var donos = await _context.Donos.FindAsync(id);

         if (donos == null) {
            return NotFound();
         }

         return donos;
      }

      // PUT: api/DonosAPI/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutDonos(int id, Donos donos) {
         if (id != donos.Id) {
            return BadRequest();
         }

         _context.Entry(donos).State = EntityState.Modified;

         try {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException) {
            if (!DonosExists(id)) {
               return NotFound();
            }
            else {
               throw;
            }
         }

         return NoContent();
      }

      // POST: api/DonosAPI
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<Donos>> PostDonos(Donos donos) {
         _context.Donos.Add(donos);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetDonos", new { id = donos.Id }, donos);
      }

      // DELETE: api/DonosAPI/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteDonos(int id) {
         var donos = await _context.Donos.FindAsync(id);
         if (donos == null) {
            return NotFound();
         }

         _context.Donos.Remove(donos);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool DonosExists(int id) {
         return _context.Donos.Any(e => e.Id == id);
      }
   }
}
