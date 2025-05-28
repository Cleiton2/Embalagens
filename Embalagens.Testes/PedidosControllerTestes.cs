using Embalagens.Controllers;
using Embalagens.Models;
using Embalagens.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Embalagens.Testes
{
    public class PedidosControllerTestes
    {
        private readonly Mock<IEmpacotadorService> _serviceMock;
        private readonly PedidosController _controller;

        public PedidosControllerTestes()
        {
            _serviceMock = new Mock<IEmpacotadorService>();
            _controller = new PedidosController(_serviceMock.Object);
        }

        [Fact]
        public void Post_PedidoNuloOuVazio_RetornaBadRequest()
        {
            // Arrange
            PedidoEntradaModel pedidoNulo = null!;
            PedidoEntradaModel pedidoVazio = new() { Pedidos = [] };

            // Act
            IActionResult resultNulo = _controller.Post(pedidoNulo);
            IActionResult resultVazio = _controller.Post(pedidoVazio);

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultNulo);
            Assert.IsType<BadRequestObjectResult>(resultVazio);
        }

        [Fact]
        public void Post_PedidoValido_RetornaOkComResultado()
        {
            // Arrange
            PedidoEntradaModel pedidoEntrada = new()
            {
                Pedidos = [new PedidoModel { Id = 1, Produtos = [] }]
            };

            List<PedidoRespostaModel> respostaEsperada = [ new() { PedidoId = 1, Caixas = [] } ];

            _serviceMock.Setup(s => s.Empacotar(pedidoEntrada)).Returns(respostaEsperada);

            // Act
            IActionResult result = _controller.Post(pedidoEntrada);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(respostaEsperada, okResult.Value);
        }

        [Fact]
        public void Post_ServicoLancaExcecao_RetornaStatusCode500()
        {
            // Arrange
            PedidoEntradaModel pedidoEntrada = new()
            {
                Pedidos = [new PedidoModel { Id = 1, Produtos = [] }]
            };

            _serviceMock.Setup(s => s.Empacotar(pedidoEntrada)).Throws(new Exception("Erro interno"));

            // Act
            IActionResult result = _controller.Post(pedidoEntrada);

            // Assert
            ObjectResult statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Contains("Erro interno", statusResult.Value.ToString());
        }
    }
}