using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Veterinarios.Models;

namespace Veterinarios.Data {

   /// <summary>
   /// representa a base de dados do nosso sistema
   /// </summary>
   public class ApplicationDbContext : IdentityDbContext {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options) {
      }


      protected override void OnModelCreating(ModelBuilder modelBuilder) {

         /*
          * esta instrução importa todas as tarefas associadas ao método
          * qd foi definida na superclasse
          * */
         base.OnModelCreating(modelBuilder);

         // inicializar os dados das tabelas da BD
         modelBuilder.Entity<MedicosVeterinarios>().HasData(
            new MedicosVeterinarios { Id=1, Nome="José Lopes", NumCedulaProf="Vet-8768", Fotografia="Jose.jpg"},
            new MedicosVeterinarios { Id =2, Nome = "Maria dos Santos", NumCedulaProf = "Vet-2568", Fotografia = "Maria.jpg" },
            new MedicosVeterinarios { Id =3, Nome = "Ricardo Gonçalo Silva", NumCedulaProf = "Vet-2344", Fotografia = "Ricardo.jpg" }
            );


      }



      // definir as tabelas
      public DbSet<Animais> Animais { get; set; }
      public DbSet<Donos> Donos { get; set; }
      public DbSet<MedicosVeterinarios> Veterinarios { get; set; }
      public DbSet<Consultas> Consultas { get; set; }


   }
}