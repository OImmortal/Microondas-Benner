using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MicroondasMVC_Benner.Models.API
{
    public class UserAuthModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int? Id { get; set; } = null;
        [Required]
        public string? Senha { get; set; }
        [Required]
        public string? User { get; set; }

        public UserAuthModel()
        {
            
        }
    }
}
