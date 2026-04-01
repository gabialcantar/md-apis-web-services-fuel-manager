using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace md_apis_web_services_fuel_manager.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        // 🔥 RELACIONAMENTO CORRETO
        public ICollection<VeiculoUsuario> VeiculoUsuarios { get; set; }
    }
}