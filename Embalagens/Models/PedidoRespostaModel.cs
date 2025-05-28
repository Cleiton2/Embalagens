namespace Embalagens.Models
{
    public class PedidoRespostaModel
    {
        public int PedidoId { get; set; }

        public List<CaixaModel> Caixas { get; set; }
    }
}