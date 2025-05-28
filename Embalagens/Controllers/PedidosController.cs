using Embalagens.Models;
using Embalagens.Services;
using Microsoft.AspNetCore.Mvc;

namespace Embalagens.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IEmpacotadorService _empacotadorService;

        public PedidosController(IEmpacotadorService empacotadorService)
        {
            _empacotadorService = empacotadorService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PedidoEntradaModel pedidoModel)
        {
            if (pedidoModel?.Pedidos == null || !pedidoModel.Pedidos.Any())
                return BadRequest("Lista de pedidos está vazia ou inválida.");

            try
            {
                return Ok(_empacotadorService.Empacotar(pedidoModel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao processar pedido: {ex.Message}");
            }
        }
    }
}