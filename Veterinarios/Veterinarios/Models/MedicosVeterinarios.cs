namespace Veterinarios.Models {
   public class MedicosVeterinarios {

      public MedicosVeterinarios() {
         ListaConsultas = new HashSet<Consultas>();
      }

      public int Id { get; set; }

      /// <summary>
      /// nome do médico veterinário
      /// </summary>
      public string Nome { get; set; }

      /// <summary>
      /// nº da cédula profissional do médico veterinário
      /// </summary>
      public string NumCedulaProf { get; set; }

      /// <summary>
      /// Foto do médico veterinário
      /// </summary>
      public string Fotografia { get; set; }

      /// <summary>
      /// Lista de consultas que são efetuadas pelo Veterinário
      /// </summary>
      public ICollection<Consultas> ListaConsultas { get; set; }

   }
}
