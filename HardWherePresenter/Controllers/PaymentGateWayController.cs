using Application.Services.Payment;
using Domain.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGateWayController : ControllerBase
    {
        private readonly IPaymentGateWayService _paymentgateWayService;

        public PaymentGateWayController(IPaymentGateWayService paymentgateWayService)
        {
            _paymentgateWayService = paymentgateWayService;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentGateWay>> Get()
        {
            var list = await _paymentgateWayService.RetriveAllAsync();
            return list;
        }
    }
}
