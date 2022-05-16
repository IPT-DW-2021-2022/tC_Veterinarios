using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Veterinarios.Data;
using Veterinarios.Models;

namespace Veterinarios.Controllers {

   [Authorize(Roles = "Veterinario")]
   public class ConsultasController : Controller {
      private readonly ApplicationDbContext _context;

      public ConsultasController(ApplicationDbContext context) {
         _context = context;
      }

      // GET: Consultas
      public async Task<IActionResult> Index() {
         var applicationDbContext = _context.Consultas.Include(c => c.Animal).Include(c => c.Veterinario);
         return View(await applicationDbContext.ToListAsync());
      }

      // GET: Consultas/Details/5
      public async Task<IActionResult> Details(int? id) {
         if (id == null) {
            return NotFound();
         }

         var consultas = await _context.Consultas
             .Include(c => c.Animal)
             .Include(c => c.Veterinario)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (consultas == null) {
            return NotFound();
         }

         return View(consultas);
      }





      // GET: Consultas/Create
      public IActionResult Create() {
         ViewData["AnimalFK"] = new SelectList(_context.Animais
                                                       .OrderBy(a => a.DonoFK)
                                                       .ThenBy(a => a.Nome), "Id", "Nome");



         ViewData["VeterinarioFK"] = new SelectList(_context.Veterinarios.OrderBy(v => v.Nome), "Id", "Nome");
         return View();
      }





      // POST: Consultas/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Data,AuxValorConsulta,Observacoes,AnimalFK,VeterinarioFK")] Consultas consulta) {

         // entregar o valor 'auxiliar' ao legítimo 'proprietário'
         consulta.ValorConsulta = Convert.ToDecimal(consulta.AuxValorConsulta.Replace('.', ','));

         if (ModelState.IsValid) {
            _context.Add(consulta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewData["AnimalFK"] = new SelectList(_context.Animais, "Id", "Id", consulta.AnimalFK);
         ViewData["VeterinarioFK"] = new SelectList(_context.Veterinarios, "Id", "Id", consulta.VeterinarioFK);
         return View(consulta);
      }

      // GET: Consultas/Edit/5
      public async Task<IActionResult> Edit(int? id) {
         if (id == null) {
            return NotFound();
         }

         var consultas = await _context.Consultas.FindAsync(id);
         if (consultas == null) {
            return NotFound();
         }
         ViewData["AnimalFK"] = new SelectList(_context.Animais, "Id", "Id", consultas.AnimalFK);
         ViewData["VeterinarioFK"] = new SelectList(_context.Veterinarios, "Id", "Id", consultas.VeterinarioFK);
         return View(consultas);
      }

      // POST: Consultas/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Data,ValorConsulta,Observacoes,AnimalFK,VeterinarioFK")] Consultas consultas) {
         if (id != consultas.Id) {
            return NotFound();
         }

         if (ModelState.IsValid) {
            try {
               _context.Update(consultas);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
               if (!ConsultasExists(consultas.Id)) {
                  return NotFound();
               }
               else {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         ViewData["AnimalFK"] = new SelectList(_context.Animais, "Id", "Id", consultas.AnimalFK);
         ViewData["VeterinarioFK"] = new SelectList(_context.Veterinarios, "Id", "Id", consultas.VeterinarioFK);
         return View(consultas);
      }

      // GET: Consultas/Delete/5
      public async Task<IActionResult> Delete(int? id) {
         if (id == null) {
            return NotFound();
         }

         var consultas = await _context.Consultas
             .Include(c => c.Animal)
             .Include(c => c.Veterinario)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (consultas == null) {
            return NotFound();
         }

         return View(consultas);
      }

      // POST: Consultas/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id) {
         var consultas = await _context.Consultas.FindAsync(id);
         _context.Consultas.Remove(consultas);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool ConsultasExists(int id) {
         return _context.Consultas.Any(e => e.Id == id);
      }
   }
}
