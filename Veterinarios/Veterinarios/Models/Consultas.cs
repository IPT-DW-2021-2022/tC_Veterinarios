using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinarios.Models {
   public class Consultas {

      public int Id { get; set; }

      public DateTime Data { get; set; }

      public Decimal ValorConsulta { get; set; }

      public string Observacoes { get; set; }

      /// <summary>
      /// FK para o animal que participa na Consulta
      /// </summary>
      public int AnimalFK { get; set; }
      [ForeignKey(nameof(AnimalFK))]
      public Animais Animal { get; set; }


      /// <summary>
      /// FK para o Veterinário que efetua a Consulta
      /// </summary>
     [ForeignKey(nameof(Veterinario))]
      public int VeterinarioFK { get; set; }
      public MedicosVeterinarios Veterinario { get; set; }

   }
}
