using Domain.OrderNS;

namespace Application.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order> AddNewOrder(Order Order);
        public Task<Order> GetOrderByOrderId(int OrderId);
        public Task<List<Order>> GetAllOrderByUserId(int userId);
        public Task<List<Order>> GetAllOrders(int pageNumber, int pageSize);
        public Task<bool> UpdateOrderStatus(int OrderId, string value);
        public Task<Order> UpdateOrder(int OrderId, Order Order);
        public Task<Order> DeleteOrder(int OrderId);
    }
}
