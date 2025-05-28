using System.ComponentModel.DataAnnotations;

namespace Embalagens.Models
{
    public class ProdutoModel
    {
        [Key]
        public string ProdutoId { get; set; }

        public DimensoesModel Dimensoes { get; set; } = new();
    }
}