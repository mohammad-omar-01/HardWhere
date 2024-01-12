using Application.DTOs.Order;
using Application.Services;
using Application.Services___Repositores.OrderService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing.Printing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<ActionResult<OrderDtoReturnResult>> GetOrders(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3
        )
        {
            var response = await _orderService.GetAllOrders(pageNumber, pageSize);
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDtoReturnResult>> GetOrderById([FromRoute] int id)
        {
            var response = await _orderService.GetOrderByID(id);
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] OrderDTO order)
        {
            var response = await _orderService.AddNewOrder(order);
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromQuery] string value)
        {
            var response = await _orderService.ChangeOrderStatus(id, value);
            if (response == false)
            {
                return BadRequest(response);
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> Put([FromRoute] int id, [FromBody] OrderDTO order)
        {
            var response = await _orderService.UpdateOrder(id, order);
            if (response == null)
            {
                return NotFound("Order Not Found");
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDTO>> DeleteOrder([FromRoute] int id)
        {
            var response = await _orderService.deleteOrderById(id);
            if (response == null)
            {
                return NotFound("Order Not Found");
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }
    }
}
