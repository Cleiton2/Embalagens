using Embalagens.Data;
using Embalagens.Models;
using Embalagens.Services;

public class EmpacotadorService : IEmpacotadorService
{
    private readonly EmbalagensDbContext _context;

    public EmpacotadorService(EmbalagensDbContext context)
    {
        _context = context;
    }

    public List<PedidoRespostaModel> Empacotar(PedidoEntradaModel pedidoEntrada)
    {
        List<CaixaModel> caixasDisponiveis = [.. _context.Caixas];

        return [.. pedidoEntrada.Pedidos.Select(pedido => new PedidoRespostaModel
        {
            PedidoId = pedido.Id,
            Caixas = EmpacotarProdutos([.. pedido.Produtos], caixasDisponiveis)
        })];
    }

    private static List<CaixaModel> EmpacotarProdutos(List<ProdutoModel> produtos, List<CaixaModel> caixasDisponiveis)
    {
        List<CaixaModel> caixasUsadas = [];

        foreach (ProdutoModel produto in produtos)
        {
            CaixaModel? caixaExistente = caixasUsadas
                .Where(c => c.Observacao == null)
                .FirstOrDefault(c => CabeNaCaixa(produto, c));

            if (caixaExistente != null)
            {
                caixaExistente.Produtos!.Add(produto.ProdutoId);
                continue;
            }

            CaixaModel? novaCaixa = caixasDisponiveis.FirstOrDefault(c => CabeNaCaixa(produto, c));

            if (novaCaixa != null)
            {
                caixasUsadas.Add(new CaixaModel
                {
                    IdCaixa = novaCaixa.IdCaixa,
                    Dimensoes = novaCaixa.Dimensoes,
                    Produtos = [produto.ProdutoId]
                });
            }
            else
            {
                caixasUsadas.Add(new CaixaModel
                {
                    IdCaixa = null,
                    Produtos = null,
                    Observacao = "Produto não cabe em nenhuma caixa disponível."
                });
            }
        }

        return caixasUsadas;
    }

    private static bool CabeNaCaixa(ProdutoModel produto, CaixaModel caixa) =>
        produto.Dimensoes.Altura <= caixa.Dimensoes.Altura &&
        produto.Dimensoes.Largura <= caixa.Dimensoes.Largura &&
        produto.Dimensoes.Comprimento <= caixa.Dimensoes.Comprimento;
}