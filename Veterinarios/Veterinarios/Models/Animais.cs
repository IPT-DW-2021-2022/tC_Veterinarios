using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinarios.Models {
   public class Animais {

      public Animais() {
         ListaConsultas = new HashSet<Consultas>();
      }


      public int Id { get; set; }

      public string Nome { get; set; }

      [Display(Name ="Espécie")]
      public string Especie { get; set; }

      [Display(Name = "Raça")]
      public string Raca { get; set; }

      public double Peso { get; set; }

      public string Foto { get; set; }

      /// <summary>
      /// FK para o Dono
      /// </summary>
      [ForeignKey(nameof(Dono))]
      public int DonoFK { get; set; }
      public Donos Dono { get; set; }

      /// <summary>
      /// Lista de consultas em que o Animal participa
      /// </summary>
      public ICollection<Consultas> ListaConsultas { get; set; }

   }
}
