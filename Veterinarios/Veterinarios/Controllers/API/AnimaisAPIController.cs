using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinarios.Data;
using Veterinarios.Models;

namespace Veterinarios.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaisAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnimaisAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AnimaisAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalViewModel>>> GetAnimais()
        {
         return await _context.Animais
                              .Include(a => a.Dono)
                              .Select(a => new AnimalViewModel {
                                 Id = a.Id,
                                 Nome = a.Nome,
                                 Raca = a.Raca,
                                 Especie = a.Especie,
                                 Peso = a.Peso,
                                 Foto = a.Foto,
                                 NomeDono = a.Dono.Nome
                              })
                              .ToListAsync();
      }

        // GET: api/AnimaisAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalViewModel>> GetAnimais(int id)
        {
            var animal = await _context.Animais
                              .Include(a => a.Dono)
                              .Select(a => new AnimalViewModel {
                                 Id = a.Id,
                                 Nome = a.Nome,
                                 Raca = a.Raca,
                                 Especie = a.Especie,
                                 Peso = a.Peso,
                                 Foto = a.Foto,
                                 NomeDono = a.Dono.Nome
                              })
                              .Where(a=>a.Id== id)
                              .FirstOrDefaultAsync();

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

      // PUT: api/AnimaisAPI/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutAnimais(int id, Animais animais) {
         if (id != animais.Id) {
            return BadRequest();
         }

         _context.Entry(animais).State = EntityState.Modified;

         try {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException) {
            if (!AnimaisExists(id)) {
               return NotFound();
            }
            else {
               throw;
            }
         }

         return NoContent();
      }

      // POST: api/AnimaisAPI
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<Animais>> PostAnimais(Animais animais) {
         _context.Animais.Add(animais);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetAnimais", new { id = animais.Id }, animais);
      }

      // DELETE: api/AnimaisAPI/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteAnimais(int id) {
         var animais = await _context.Animais.FindAsync(id);
         if (animais == null) {
            return NotFound();
         }

         _context.Animais.Remove(animais);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool AnimaisExists(int id) {
         return _context.Animais.Any(e => e.Id == id);
      }
   }
}
