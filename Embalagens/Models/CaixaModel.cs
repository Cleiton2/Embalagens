using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Embalagens.Models
{
    public class CaixaModel
    {
        [Key]
        [Required]
        [JsonPropertyName("caixa_id")]
        public required string IdCaixa { get; set; }

        [NotMapped]
        [JsonPropertyName("produtos")]
        public List<string> Produtos { get; set; } = [];

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DimensoesModel Dimensoes { get; set; } = new();

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("observacao")]
        public string? Observacao { get; set; }
    }
}