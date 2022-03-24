namespace Veterinarios.Models {
   public class MedicosVeterinarios {

      public MedicosVeterinarios() {
         ListaConsultas = new HashSet<Consultas>();
      }

      public int Id { get; set; }

      public string Nome { get; set; }

      public string NumCedulaProf { get; set; }

      public string Fotografia { get; set; }

      /// <summary>
      /// Lista de consultas que são efetuadas pelo Veterinário
      /// </summary>
      public ICollection<Consultas> ListaConsultas { get; set; }

   }
}
