using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinarios.Models {
   public class Consultas {

      public int Id { get; set; }

      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
                     ApplyFormatInEditMode = true)]
      public DateTime Data { get; set; }


      [NotMapped]
      [Required]
      [RegularExpression("[0-9]{1,10}[.,]?[0-9]{0,2}")]
      [Display(Name = "Valor da Consulta")]
      public string AuxValorConsulta { get; set; }
      public Decimal? ValorConsulta { get; set; }   // o ? torna o atributo de preenchimento facultativo

      public string Observacoes { get; set; }

      /// <summary>
      /// FK para o animal que participa na Consulta
      /// </summary>
      [Display(Name = "Animal")]
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
