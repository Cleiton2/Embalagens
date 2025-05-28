using Microsoft.EntityFrameworkCore;
using Embalagens.Data;
using Embalagens.Models;

namespace Embalagens.Testes
{
    public class EmpacotadorServiceTestes
    {
        private static EmbalagensDbContext DbContextComCaixasFalsas
        {
            get
            {
                DbContextOptions<EmbalagensDbContext> dbContexto = new DbContextOptionsBuilder<EmbalagensDbContext>()
                    .UseInMemoryDatabase(databaseName: "EmbalagensTestDB")
                    .Options;

                EmbalagensDbContext contexto = new(dbContexto);

                contexto.Caixas.AddRange(
                    new CaixaModel
                    {
                        IdCaixa = "Caixa 1",
                        Dimensoes = new DimensoesModel { Altura = 10, Largura = 10, Comprimento = 10 }
                    },
                    new CaixaModel
                    {
                        IdCaixa = "Caixa 2",
                        Dimensoes = new DimensoesModel { Altura = 20, Largura = 20, Comprimento = 20 }
                    }
                );
                contexto.SaveChanges();

                return contexto;
            }
        }

        [Fact]
        public void Empacotar_DeveEmpacotarProdutosEmCaixasCompatíveis()
        {
            // Arrange
            EmbalagensDbContext dbContexto = DbContextComCaixasFalsas;
            EmpacotadorService empacotadorServico = new(dbContexto);

            PedidoEntradaModel pedidoEntrada = new()
            {
                Pedidos =
                [
                    new() {
                    Id = 1,
                    Produtos =
                    [
                        new ()
                        {
                            ProdutoId = "Cadeira Gamer",
                            Dimensoes = new DimensoesModel
                            {
                                Altura = 5,
                                Largura = 5,
                                Comprimento = 5
                            }
                        },
                        new ()
                        {
                            ProdutoId = "joystic",
                            Dimensoes = new DimensoesModel
                            {
                                Altura = 15,
                                Largura = 15,
                                Comprimento = 15
                            }
                        },
                        new() {
                            ProdutoId = "notebook",
                            Dimensoes = new DimensoesModel
                            {
                                Altura = 25,
                                Largura = 25,
                                Comprimento = 25
                            }
                        }
                    ]
                }
                ]
            };

            // Act
            List<PedidoRespostaModel> resultado = empacotadorServico.Empacotar(pedidoEntrada);

            // Assert
            Assert.Single(resultado);
            List<CaixaModel> caixas = resultado[0].Caixas;

            Assert.Equal(3, caixas.Count);
            Assert.Contains(caixas, c => c.Produtos != null && c.Produtos.Contains("Cadeira Gamer"));
            Assert.Contains(caixas, c => c.Produtos != null && c.Produtos.Contains("joystic"));
            Assert.Contains(caixas, c => c.Observacao == "Produto não cabe em nenhuma caixa disponível.");
        }
    }
}