using Domain.Payment;

namespace Application.Repositories
{
    public interface IPaymentRepository
    {
        public bool AddPaymentGateWay(PaymentGateWay paymentGateWay);
        public PaymentGateWay Update(PaymentGateWay paymentGateWay);
        public PaymentGateWay Retrive(string id);
        public Task<List<PaymentGateWay>> RetriveAllAsync();
    }
}
