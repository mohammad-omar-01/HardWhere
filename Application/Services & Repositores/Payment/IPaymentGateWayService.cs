using Domain.Payment;

namespace Application.Services.Payment
{
    public interface IPaymentGateWayService
    {
        public PaymentGateWay AddNewPaymentGateWay(PaymentGateWay paymentGateWay);
        public PaymentGateWay Update(PaymentGateWay paymentGateWay);
        public PaymentGateWay Retrive(string id);
        public Task<List<PaymentGateWay>> RetriveAllAsync();
    }
}
