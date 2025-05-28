using System.Text.Json.Serialization;

namespace Embalagens.Models
{
    public class PedidoEntradaModel
    {
        public required List<PedidoModel> Pedidos { get; set; }
    }
}