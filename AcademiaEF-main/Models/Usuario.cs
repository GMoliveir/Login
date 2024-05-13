using System.ComponentModel.DataAnnotations;

namespace Academia.Models
{
    public class Usuario
    {
      public int UsuarioID { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string Senha { get; set; }
        public string Nome { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Forneça um email valido !")]
        public string Email { get; set; }
    }
}
