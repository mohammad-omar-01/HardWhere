using Application.DTOs.Order;
using Domain.OrderNS;

namespace Application.Services___Repositores.OrderService
{
    public interface IOrderService
    {
        public Task<OrderDtoReturnResult> GetOrderByID(int id);
        public Task<bool> ChangeOrderStatus(int id, string value);
        public Task<OrderDtoReturnResult> GetOrderByUserID(int userId);
        public Task<OrderDtoReturnResultList> GetAllOrderByUserID(int userId);
        public Task<OrderDtoReturnResultList> GetAllOrders(int pageNumber, int pageSize);
        public Task<OrderDtoReturnResult> AddNewOrder(OrderDTO Order);
        public Task<OrderDtoReturnResult> UpdateOrder(int OrderId, OrderDTO Order);
        public Task<OrderDtoReturnResult> deleteOrder(Order Order);
        public Task<OrderDtoReturnResult> deleteOrderById(int OrderId);
    }
}
