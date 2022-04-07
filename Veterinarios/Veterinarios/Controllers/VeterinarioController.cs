﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Veterinarios.Data;
using Veterinarios.Models;

namespace Veterinarios.Controllers {

   public class VeterinarioController : Controller {

      /// <summary>
      /// cria uma referência à base de dados do projeto
      /// </summary>
      private readonly ApplicationDbContext _context;

      /// <summary>
      /// esta variável contém os dados do servidor ASP .NET
      /// </summary>
      private readonly IWebHostEnvironment _webHostEnvironment;

      public VeterinarioController(
         ApplicationDbContext context,
         IWebHostEnvironment webHostEnvironment) {
         _context = context;
         _webHostEnvironment = webHostEnvironment;
      }




      // GET: Veterinario
      public async Task<IActionResult> Index() {
         return View(await _context.Veterinarios.ToListAsync());
      }




      // GET: Veterinario/Details/5
      public async Task<IActionResult> Details(int? id) {
         if (id == null) {
            return NotFound();
         }

         var medicosVeterinarios = await _context.Veterinarios
             .FirstOrDefaultAsync(m => m.Id == id);
         if (medicosVeterinarios == null) {
            return NotFound();
         }

         return View(medicosVeterinarios);
      }






      // GET: Veterinario/Create
      /// <summary>
      /// Invoca a View de criação de um novo médico veterinário
      /// </summary>
      /// <returns></returns>
      public IActionResult Create() {
         return View();
      }

      // POST: Veterinario/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Nome,NumCedulaProf,Fotografia")] MedicosVeterinarios medicoVeterinario, IFormFile fotoVet) {
         /* Algoritmo para o processamento da foto
          * 
          * se 'fotoVet' == null
          *     atribui ao novo veterinário uma foto standard
          * caso contrário
          *    se o 'fotoVet' não for realmente uma foto
          *        avisar o utilizador para se deixar de brincadeiras
          *    caso contrário
          *        - definir o nome a atribuir ao ficheiro
          *        - atribuir o nome ao objeto 'medicoVeterinario' que contém os dados vindo do browser
          *        - escrever no disco rígido do servidor o ficheiro
          */

         // será q foi fornecido um ficheiro
         if (fotoVet == null) {
            medicoVeterinario.Fotografia = "noVet.jpg";
         }
         else {
            if (!(fotoVet.ContentType == "image/jpeg" || fotoVet.ContentType == "image/png")) {
               // gerar a mensagem de erro
               ModelState.AddModelError("", "Por favor, deixe-se de brincadeiras... e adicione uma foto!");
               // retorna o controlo à View, devolvendo os dados q receberam dela
               return View(medicoVeterinario);
            }
            else {
               // há ficheiro e é uma imagem
               string nomeImagem = "";
               Guid g;
               g = Guid.NewGuid();
               nomeImagem = g.ToString();
               // determinar a extensão do ficheiro
               string extensaoImagem = Path.GetExtension(fotoVet.FileName).ToLower();
               nomeImagem += extensaoImagem;
               // atribuir aos dados do novo médico o nome da sua foto
               medicoVeterinario.Fotografia = nomeImagem;
            }
         }

         // avaliar se os dados do Modelo estão válido
         if (ModelState.IsValid) {
            // adiciona o novo objeto à BD
            _context.Add(medicoVeterinario);
            // consolida os dados
            await _context.SaveChangesAsync();
            // Vou gravar o ficheiro no disco rígido
            if (fotoVet != null) {
               // se entrar aqui é pq o ficheiro existe e é válido
               // obter o endereço da pasta WWWROOT do projeto
               string localizacaoImagem = _webHostEnvironment.WebRootPath;
               // identificar se a pasta 'Fotos' existe
               if (!Directory.Exists(Path.Combine(localizacaoImagem, "Fotos"))) {
                  Directory.CreateDirectory(Path.Combine(localizacaoImagem, "Fotos"));
               }
               // criar a referência ao ficheiro a guardar
               localizacaoImagem = Path.Combine(localizacaoImagem, "Fotos", medicoVeterinario.Fotografia);
               // guardar efetivamente o ficheiro no disco rígido
               using var stream = new FileStream(localizacaoImagem, FileMode.Create);
               await fotoVet.CopyToAsync(stream);
            }
            return RedirectToAction(nameof(Index));
         }
         return View(medicoVeterinario);
      }

      // GET: Veterinario/Edit/5
      public async Task<IActionResult> Edit(int? id) {
         if (id == null) {
            return NotFound();
         }

         var medicosVeterinarios = await _context.Veterinarios.FindAsync(id);
         if (medicosVeterinarios == null) {
            return NotFound();
         }
         return View(medicosVeterinarios);
      }

      // POST: Veterinario/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,NumCedulaProf,Fotografia")] MedicosVeterinarios medicosVeterinarios) {
         if (id != medicosVeterinarios.Id) {
            return NotFound();
         }

         if (ModelState.IsValid) {
            try {
               _context.Update(medicosVeterinarios);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
               if (!MedicosVeterinariosExists(medicosVeterinarios.Id)) {
                  return NotFound();
               }
               else {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(medicosVeterinarios);
      }

      // GET: Veterinario/Delete/5
      public async Task<IActionResult> Delete(int? id) {
         if (id == null) {
            return NotFound();
         }

         var medicosVeterinarios = await _context.Veterinarios
             .FirstOrDefaultAsync(m => m.Id == id);
         if (medicosVeterinarios == null) {
            return NotFound();
         }

         return View(medicosVeterinarios);
      }

      // POST: Veterinario/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id) {
         var medicosVeterinarios = await _context.Veterinarios.FindAsync(id);
         _context.Veterinarios.Remove(medicosVeterinarios);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool MedicosVeterinariosExists(int id) {
         return _context.Veterinarios.Any(e => e.Id == id);
      }
   }
}
