namespace Veterinarios.Models {

   /// <summary>
   /// View model para representar os dados dos Donos
   /// para todos os efeitos é uma 'vista'
   /// </summary>
   public class DonoViewModel {
      public int Id { get; set; }
      public string Nome { get; set; }
   }

   /// <summary>
   /// classe para representar uma 'vista' dos dados dos Animais
   /// </summary>
   public class AnimalViewModel {
      public int Id { get; set; }
      public string Nome { get; set; }
      public string Especie { get; set; }
      public string Raca { get; set; }
      public double Peso { get; set; }
      public string Foto { get; set; }
      public string NomeDono { get; set; }
   }





   public class ErrorViewModel {
      public string RequestId { get; set; }

      public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
   }
}