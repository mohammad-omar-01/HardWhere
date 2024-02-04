using Application.Repositories;
using Domain.OrderNS;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace infrastructure.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HardwhereDbContext _dbContext;

        public OrderRepository(HardwhereDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> AddNewOrder(Order order)
        {
            var orderToAdd = await _dbContext.Orders.AddAsync(order);
            orderToAdd.Entity.contentes.ForEach(
                a => a.product = _dbContext.Products.FirstOrDefault(b => b.ProductId == a.ProductId)
            );
            var address = await _dbContext.Addresses.FirstOrDefaultAsync(
                a => a.AddressID == order.BillingAddressId
            );
            if (address.AddressName == string.Empty && address.Address1 == string.Empty) { }
            await _dbContext.SaveChangesAsync();
            orderToAdd.Entity.Customer = await _dbContext.Users.FirstOrDefaultAsync(
                a => a.Id == order.customerId
            );

            return orderToAdd.Entity;
        }

        public async Task<Order> DeleteOrder(int orderId)
        {
            var orderToCheck = await _dbContext.Orders.FirstOrDefaultAsync(
                order => order.orderId == orderId
            );
            if (orderToCheck == null)
            {
                return null;
            }
            var deltedOrder = _dbContext.Orders.Remove(orderToCheck);
            await _dbContext.SaveChangesAsync();
            return deltedOrder.Entity;
        }

        public async Task<Order> GetOrderByOrderId(int orderId)
        {
            var order = await _dbContext.Orders
                .Include(c => c.contentes)
                .Include(c => c.Customer)
                .Include(c => c.ShippingAddress)
                .Include(c => c.BillingAdress)
                .FirstOrDefaultAsync(order => order.orderId == orderId);
            return order;
        }

        public async Task<List<Order>> GetAllOrderByUserId(int userId)
        {
            return await _dbContext.Orders
                .Include(c => c.contentes)
                .Include(c => c.Customer)
                .Include(c => c.ShippingAddress)
                .Include(c => c.BillingAdress)
                .Where(order => order.customerId == userId)
                .ToListAsync();
        }

        public async Task<Order> UpdateOrder(int orderId, Order order)
        {
            var orderToCheck = await _dbContext.Orders
                .Include(o => o.contentes)
                .FirstOrDefaultAsync(order => order.orderId == orderId);
            if (orderToCheck == null)
            {
                return null;
            }
            foreach (PropertyInfo property in typeof(Order).GetProperties().Where(p => p.CanWrite))
            {
                if (property.Name.Equals("orderId"))
                    continue;
                property.SetValue(orderToCheck, property.GetValue(order, null), null);
            }
            await _dbContext.SaveChangesAsync();
            return orderToCheck;
        }

        public async Task<List<Order>> GetAllOrders(int pageNumber, int pageSize)
        {
            int recordsToSkip = (pageNumber - 1) * pageSize;

            return await _dbContext.Orders
                .Include(c => c.contentes)
                .Include(c => c.Customer)
                .Include(c => c.ShippingAddress)
                .Include(c => c.BillingAdress)
                .Skip(recordsToSkip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatus(int OrderId, string value)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(a => a.orderId == OrderId);
            if (order == null)
            {
                return false;
            }
            order.orderStatus = value;
            _dbContext.SaveChangesAsync().Wait();
            return true;
        }
    }
}
