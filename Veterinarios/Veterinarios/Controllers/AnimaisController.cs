using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Veterinarios.Data;
using Veterinarios.Models;

namespace Veterinarios.Controllers {
   [Authorize]
   public class AnimaisController : Controller {
      /// <summary>
      /// referência à BD
      /// </summary>
      private readonly ApplicationDbContext _context;

      /// <summary>
      /// acede aos dados da pessoa autenticada
      /// </summary>
      private readonly UserManager<ApplicationUser> _userManager;

      public AnimaisController(
         ApplicationDbContext context,
         UserManager<ApplicationUser> userManager) {
         _context = context;
         _userManager = userManager;
      }




      // GET: Animais
      public async Task<IActionResult> Index() {

         // determinar o ID da pessoa q está autenticada
        string idDaPessoaQueEstaAutenticada=_userManager.GetUserId(User);

         // procurar a lista de animais
         var animais = _context.Animais
                               .Include(a => a.Dono)
                               .Where(a=>a.Dono.UserId==idDaPessoaQueEstaAutenticada);


         return View(await animais.ToListAsync());
      }




      // GET: Animais/Details/5
      public async Task<IActionResult> Details(int? id) {
         if (id == null || _context.Animais == null) {
            return NotFound();
         }

         var animais = await _context.Animais
             .Include(a => a.Dono)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (animais == null) {
            return NotFound();
         }

         return View(animais);
      }

      // GET: Animais/Create
      public IActionResult Create() {
         ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "NIF");
         return View();
      }

      // POST: Animais/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Nome,Especie,Raca,Peso,Foto,DonoFK")] Animais animais) {
         if (ModelState.IsValid) {
            _context.Add(animais);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "NIF", animais.DonoFK);
         return View(animais);
      }

      // GET: Animais/Edit/5
      public async Task<IActionResult> Edit(int? id) {
         if (id == null || _context.Animais == null) {
            return NotFound();
         }

         var animais = await _context.Animais.FindAsync(id);
         if (animais == null) {
            return NotFound();
         }
         ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "NIF", animais.DonoFK);
         return View(animais);
      }

      // POST: Animais/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Especie,Raca,Peso,Foto,DonoFK")] Animais animais) {
         if (id != animais.Id) {
            return NotFound();
         }

         if (ModelState.IsValid) {
            try {
               _context.Update(animais);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
               if (!AnimaisExists(animais.Id)) {
                  return NotFound();
               }
               else {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "NIF", animais.DonoFK);
         return View(animais);
      }

      // GET: Animais/Delete/5
      public async Task<IActionResult> Delete(int? id) {
         if (id == null || _context.Animais == null) {
            return NotFound();
         }

         var animais = await _context.Animais
             .Include(a => a.Dono)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (animais == null) {
            return NotFound();
         }

         return View(animais);
      }

      // POST: Animais/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id) {
         if (_context.Animais == null) {
            return Problem("Entity set 'ApplicationDbContext.Animais'  is null.");
         }
         var animais = await _context.Animais.FindAsync(id);
         if (animais != null) {
            _context.Animais.Remove(animais);
         }

         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool AnimaisExists(int id) {
         return _context.Animais.Any(e => e.Id == id);
      }
   }
}
