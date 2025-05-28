using System.Text.Json.Serialization;

namespace Embalagens.Models
{
    public class PedidoModel
    {
        [JsonPropertyName("pedido_id")]
        public int Id { get; set; }

        public ICollection<ProdutoModel> Produtos { get; set; } = [];
    }
}