using System.ComponentModel.DataAnnotations;

namespace Veterinarios.Models {

   /// <summary>
   /// descrição dos dados dos clientes (donos do animais) da clínica
   /// </summary>
   public class Donos {

      public Donos() {
         ListaAnimais = new HashSet<Animais>();
      }



      /// <summary>
      /// PK para o dono
      /// </summary>
      [Key]
      public int Id { get; set; }

      /// <summary>
      /// Nome do dono do animal
      /// </summary>
      [Required(ErrorMessage = "o {0} é de preenchimento obrigatório")]
      [StringLength(50, ErrorMessage = "o {0} não pode ter mais do que {1} caracteres.")]
      [RegularExpression("[A-Za-záéíóúâêîôûàèìòùäëïöüñçãõÁÉÍÓÚÂ '-]+",
                          ErrorMessage = "Só pode usar letras no {0}")]
      public string Nome { get; set; }

      /// <summary>
      /// sexo do dono do animal
      /// Mm - masculino; Ff - feminino
      /// </summary>
      [Required(ErrorMessage = "o {0} é de preenchimento obrigatório")]
      [StringLength(1, ErrorMessage = "o {0} não pode ter mais do que {1} carácter.")]
      [RegularExpression("[MmFf]", ErrorMessage = "só pode escrever F para feminino ou M para masculino")]
      public string Sexo { get; set; }

      /// <summary>
      /// NIF do cliente
      /// </summary>
      [Required(ErrorMessage = "o {0} é de preenchimento obrigatório")]
      [StringLength(9, MinimumLength = 9, ErrorMessage = "o {0} deve ter exatamente {1} caracteres.")]
      [RegularExpression("[0-9]{9}")]
      public string NIF { get; set; }

      /// <summary>
      /// Email do dono do animal
      /// </summary>
      [EmailAddress(ErrorMessage = "Deve escrever um {0} válido.")]
      public string Email { get; set; }


      /// <summary>
      /// lista dos animais do dono
      /// </summary>
      public ICollection<Animais> ListaAnimais { get; set; }


   }
}
