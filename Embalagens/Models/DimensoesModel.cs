using System.ComponentModel.DataAnnotations;

namespace Embalagens.Models
{
    public class DimensoesModel
    {
        [Required]
        public int Altura { get; set; }
        [Required]
        public int Largura { get; set; }
        [Required]
        public int Comprimento { get; set; }
    }
}
