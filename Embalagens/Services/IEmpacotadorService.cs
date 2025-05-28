using Embalagens.Models;

namespace Embalagens.Services
{
    public interface IEmpacotadorService
    {
        List<PedidoRespostaModel> Empacotar(PedidoEntradaModel pedidoEntrada);
    }
}