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
         string idDaPessoaQueEstaAutenticada = _userManager.GetUserId(User);

         // procurar a lista de animais
         var animais = _context.Animais
                               .Include(a => a.Dono)
                               .Where(a => a.Dono.UserId == idDaPessoaQueEstaAutenticada);


         return View(await animais.ToListAsync());
      }




      // GET: Animais/Details/5
      public async Task<IActionResult> Details(int? id) {
         if (id == null || _context.Animais == null) {
            return RedirectToAction("Index");
         }

         // determinar o ID da pessoa q está autenticada
         string idDaPessoaQueEstaAutenticada = _userManager.GetUserId(User);

         var animal = await _context.Animais
                                    .Include(a => a.Dono)
                                    .Include(a=>a.ListaConsultas)
                                    .Where(m => m.Id == id &&
                                                m.Dono.UserId == idDaPessoaQueEstaAutenticada)
                                    .FirstOrDefaultAsync();

         if (animal == null) {
            return RedirectToAction("Index");
         }

         return View(animal);
      }

      // GET: Animais/Create
      public IActionResult Create() {
         // Não vamos usar a 'dropdown', pois o animal será do 'dono' autenticado
         // ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "Nome");
         return View();
      }

      // POST: Animais/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Nome,Especie,Raca,Peso,Foto")] Animais animal) {

         /* procurar o ID do Dono, que está autenticado:
          * 1- primeiro procurar o UserID da pessoa autenticada
          * 2- usar este valor para selecionar o Dono
          * 3- 'retirar-lhe' o 'Id'
          */
         // (1)
         string idDaPessoaQueEstaAutenticada = _userManager.GetUserId(User);
         // (2 + 3)
         int idDono = _context.Donos
                              .Where(d => d.UserId == idDaPessoaQueEstaAutenticada)
                              .FirstOrDefault()
                              .Id;

         // atribuir o valor obtido (idDono) como FK ao 'animal'
         animal.DonoFK = idDono;

         if (ModelState.IsValid) {
            try {
               _context.Add(animal);
               await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
            }
            catch (Exception) {

               throw;
            }
         }
         //    ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "NIF", animais.DonoFK);
         return View(animal);
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
