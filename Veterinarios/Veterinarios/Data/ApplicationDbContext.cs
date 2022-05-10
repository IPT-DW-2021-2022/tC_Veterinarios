using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Veterinarios.Models;

namespace Veterinarios.Data {

   /// <summary>
   /// esta classe irá conter os dados da pessoa que se autentica,
   /// e regista, na nossa aplicação.
   /// Além de dados próprios, irá herdar todos os atributos da IdentityUser
   /// Aqui, há algumas alterações a fazer na nossa app
   ///    - trocar a referência ao Utilizador no ficheiro 'Program.cs'
   ///    - 'informar' a base de dados que há um novo tipo de utilizador
   ///    - trocar em todos os ficheiros da App a referência a IdentityUser por ApplicationUser
   ///    - não esquecer que estas alterações só se tornam efetivas ao nivel da BD
   ///          se existir uma Migration
   /// </summary>
   public class ApplicationUser : IdentityUser {

      /// <summary>
      /// data em que o utilizador criou o registo
      /// </summary>
      public DateTime DataRegisto { get; set; }

      /* aqui poderiam ser adicionados mais atributos,
      * se isso fosse considerado necessário
      * exemplo
      *   nome
      *   morada
      *   cod postal
      *   nif
      *   ...
      */
   }



   /// <summary>
   /// representa a base de dados do nosso sistema
   /// </summary>
   public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
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
            new MedicosVeterinarios { Id = 1, Nome = "José Lopes", NumCedulaProf = "Vet-8768", Fotografia = "Jose.jpg" },
            new MedicosVeterinarios { Id = 2, Nome = "Maria dos Santos", NumCedulaProf = "Vet-2568", Fotografia = "Maria.jpg" },
            new MedicosVeterinarios { Id = 3, Nome = "Ricardo Gonçalo Silva", NumCedulaProf = "Vet-2344", Fotografia = "Ricardo.jpg" }
            );

         modelBuilder.Entity<Donos>().HasData(
            new Donos { Id = 1, Nome = "Luís Freitas", Sexo = "M", NIF = "813635582" },
            new Donos { Id = 2, Nome = "Andreia Gomes", Sexo = "F", NIF = "854613462" },
            new Donos { Id = 3, Nome = "Cristina Sousa", Sexo = "F", NIF = "265368715" },
            new Donos { Id = 4, Nome = "Sónia Rosa", Sexo = "F", NIF = "835623190" }
         );

         modelBuilder.Entity<Animais>().HasData(
            new Animais { Id = 1, Nome = "Bubi", Especie = "Cão", Raca = "Pastor Alemão", Peso = 24, Foto = "animal1.jpg", DonoFK = 1 },
            new Animais { Id = 2, Nome = "Pastor", Especie = "Cão", Raca = "Serra Estrela", Peso = 50, Foto = "animal2.jpg", DonoFK = 3 },
            new Animais { Id = 3, Nome = "Tripé", Especie = "Cão", Raca = "Serra Estrela", Peso = 4, Foto = "animal3.jpg", DonoFK = 2 },
            new Animais { Id = 4, Nome = "Saltador", Especie = "Cavalo", Raca = "Lusitana", Peso = 580, Foto = "animal4.jpg", DonoFK = 3 },
            new Animais { Id = 5, Nome = "Tareco", Especie = "Gato", Raca = "siamês", Peso = 1, Foto = "animal5.jpg", DonoFK = 3 },
            new Animais { Id = 6, Nome = "Cusca", Especie = "Cão", Raca = "Labrador", Peso = 45, Foto = "animal6.jpg", DonoFK = 2 },
            new Animais { Id = 7, Nome = "Morde Tudo", Especie = "Cão", Raca = "Dobermann", Peso = 39, Foto = "animal7.jpg", DonoFK = 4 },
            new Animais { Id = 8, Nome = "Forte", Especie = "Cão", Raca = "Rottweiler", Peso = 20, Foto = "animal8.jpg", DonoFK = 2 },
            new Animais { Id = 9, Nome = "Castanho", Especie = "Vaca", Raca = "Mirandesa", Peso = 652, Foto = "animal9.jpg", DonoFK = 3 },
            new Animais { Id = 10, Nome = "Saltitão", Especie = "Gato", Raca = "Persa", Peso = 2, Foto = "animal10.jpg", DonoFK = 1 }
         );
      }



      // definir as tabelas
      public DbSet<Animais> Animais { get; set; }
      public DbSet<Donos> Donos { get; set; }
      public DbSet<MedicosVeterinarios> Veterinarios { get; set; }
      public DbSet<Consultas> Consultas { get; set; }


   }
}