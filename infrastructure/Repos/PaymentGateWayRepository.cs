using Application.Repositories;
using AutoMapper;
using Domain.Payment;

namespace infrastructure.Repos
{
    public class PaymentGateWayRepository : IPaymentRepository
    {
        private readonly HardwhereDbContext _dbContext;

        public PaymentGateWayRepository(HardwhereDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddPaymentGateWay(PaymentGateWay paymentGateWay)
        {
            try
            {
                _dbContext.PaymentGateWays.Add(paymentGateWay);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PaymentGateWay Retrive(string id)
        {
            return _dbContext.PaymentGateWays.FirstOrDefault(o => o.Id == id);
        }

        public Task<List<PaymentGateWay>> RetriveAllAsync()
        {
            var paymentGateWays = _dbContext.PaymentGateWays.ToList();
            return Task.FromResult(paymentGateWays);
        }

        public PaymentGateWay Update(PaymentGateWay paymentGateWay)
        {
            var paymentGW = _dbContext.PaymentGateWays.FirstOrDefault(
                opt => opt.Id == paymentGateWay.Id
            );
            if (paymentGW != null)
            {
                paymentGW.title = paymentGateWay.title;
                _dbContext.SaveChanges();
                return paymentGateWay;
            }
            return null;
        }
    }
}
