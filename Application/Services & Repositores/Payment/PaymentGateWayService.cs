using Application.Repositories;
using Domain.Payment;

namespace Application.Services.Payment
{
    public class PaymentGateWayService : IPaymentGateWayService
    {
        private readonly IPaymentRepository _paymentRepo;

        public PaymentGateWayService(IPaymentRepository paymentRepository)
        {
            this._paymentRepo = paymentRepository;
        }

        public PaymentGateWay AddNewPaymentGateWay(PaymentGateWay paymentGateWay)
        {
            var paymentGW = _paymentRepo.Retrive(paymentGateWay.Id);
            if (paymentGW != null)
            {
                if (_paymentRepo.AddPaymentGateWay(paymentGateWay))
                    return paymentGateWay;
                else
                {
                    return null;
                }
            }
            return null;
        }

        public PaymentGateWay Retrive(string id)
        {
            var paymentGW = _paymentRepo.Retrive(id);
            if (paymentGW != null)
            {
                return paymentGW;
            }
            return null;
        }

        public async Task<List<PaymentGateWay>> RetriveAllAsync()
        {
            var paytmentGWs = await _paymentRepo.RetriveAllAsync();
            return paytmentGWs;
        }

        public PaymentGateWay Update(PaymentGateWay paymentGateWay)
        {
            var PaymentGW = _paymentRepo.Retrive(paymentGateWay.Id);
            if (PaymentGW == null)
            {
                return null;
            }
            PaymentGW.title = paymentGateWay.title;
            _paymentRepo.Update(PaymentGW);
            return PaymentGW;
        }
    }
}
