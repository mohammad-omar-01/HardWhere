using Application.DTOs;
using Application.DTOs.Notfication;
using Application.DTOs.Order;
using Application.Repositories;
using Application.Services___Repositores.Mail;
using Application.Services___Repositores.NotficationNS;
using Application.Services___Repositores.OrderService;
using BasicNotification;
using Domain.OrderNS;
using HardWherePresenter;
using Microsoft.AspNetCore.SignalR;
using System.Text;

namespace Application.Services___Repositores.OrderNs
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMailService _mailService;
        private readonly INotficationService _notficationService;
        private readonly IHubContext<NotificationHub, IClientNotificationHub> _hubContext;

        MailData mailData;

        public OrderService(
            IOrderRepository orderRepository,
            IMailService mailService,
            INotficationService notficationService,
            IHubContext<NotificationHub, IClientNotificationHub> hubContext
        )
        {
            this._orderRepository = orderRepository;
            this._mailService = mailService;
            this._notficationService = notficationService;
            this._hubContext = hubContext;

            mailData = new MailData();
        }

        private void NotifyUser(Order order)
        {
            StringBuilder builder = new StringBuilder();
            order.contentes.ForEach(a =>
            {
                builder.Append(a.OrderProductName);
                builder.AppendLine();
            });
            mailData.EmailToId = order.Customer.Email;
            mailData.EmailToName = order.Customer.FirstName + " " + order.Customer.LastName;
            mailData.EmailBody =
                $"A new Order Has Been Placed for your this list of Products: \n" + $"{builder} ";
            mailData.EmailSubject = "Your Products are being orderd!!";
            CreateNotficationForOrder(order, mailData.EmailBody);
        }

        private void MailUserByOrderChange(Order order, string value)
        {
            StringBuilder builder = new StringBuilder();

            mailData.EmailToId = order.BillingAdress.user.Email;
            mailData.EmailToName =
                order.BillingAdress.user.FirstName + " " + order.BillingAdress.user.LastName;
            mailData.EmailBody = $"Your Order is : \n" + $"{value} ";
            mailData.EmailSubject = "Order New Status";
        }

        private async void CreateNotficationForOrder(Order order, string value)
        {
            NotficationDTO notficationDTO = new NotficationDTO();
            notficationDTO.NotficationType = "New Order Status";
            notficationDTO.NotficationBody =
                $"Your Order #{order.orderId} has a New State, it's now {value}";
            notficationDTO.userId = order.customerId;
            notficationDTO.NotficationTitle = "Your order has a new update";
            var notif = await _notficationService.CreateNotfication(notficationDTO);
            var user = "";
            try
            {
                user = ConnectionMapping<string>._connections[
                    order.customerId.ToString()
                ].LastOrDefault();
            }
            catch (Exception ex)
            {
                user = "";
            }

            await _hubContext.Clients.Client(user.ToString()).ClientReceiveNotification(notif);
        }

        public async Task<OrderDtoReturnResult> AddNewOrder(OrderDTO order)
        {
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var orderToAdd = _mapper.Map<Order>(order);
            orderToAdd.orderStatus = "Processing";
            orderToAdd.orderDate = DateTime.Now;
            orderToAdd = await _orderRepository.AddNewOrder(orderToAdd);
            NotifyUser(orderToAdd);
            //    _mailService.SendMail(mailData);
            var orderToReturn = _mapper.Map<OrderDTO>(orderToAdd);

            return new OrderDtoReturnResult
            {
                result = orderToReturn,
                OrderDate = orderToAdd.orderDate.ToShortDateString(),
                OrderStatus = orderToAdd.orderStatus,
                OrderId = orderToAdd.orderId
            };
        }

        public async Task<bool> ChangeOrderStatus(int id, string value)
        {
            var order = await _orderRepository.GetOrderByOrderId(id);
            if (order == null)
            {
                return false;
            }
            MailUserByOrderChange(order, value);
            //  _mailService.SendMail(mailData);
            CreateNotficationForOrder(order, value);
            return await _orderRepository.UpdateOrderStatus(id, value);
        }

        public async Task<OrderDtoReturnResult> deleteOrder(Order Order)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDtoReturnResult> deleteOrderById(int OrderId)
        {
            var userOrderDeleated = await _orderRepository.DeleteOrder(OrderId);
            if (userOrderDeleated == null)
            {
                return null;
            }
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var userOrderToReturnList = _mapper.Map<OrderDTO>(userOrderDeleated);
            return new OrderDtoReturnResult { result = userOrderToReturnList, OrderId = OrderId };
        }

        public async Task<OrderDtoReturnResultList> GetAllOrderByUserID(int userId)
        {
            var userOrders = await _orderRepository.GetAllOrderByUserId(userId);
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var userOrderToReturnList = _mapper.Map<List<OrderDtoReturnResult>>(userOrders);
            return new OrderDtoReturnResultList { result = userOrderToReturnList };
        }

        public async Task<OrderDtoReturnResultList> GetAllOrders(int pageNumber, int pageSize)
        {
            var orders = await _orderRepository.GetAllOrders(pageNumber, pageSize);
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var OrderToReturnList = _mapper.Map<List<OrderDtoReturnResult>>(orders);
            return new OrderDtoReturnResultList { result = OrderToReturnList };
        }

        public async Task<OrderDtoReturnResult> GetOrderByID(int id)
        {
            var userOrder = await _orderRepository.GetOrderByOrderId(id);
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var userOrderToReturn = _mapper.Map<OrderDtoReturnResult>(userOrder);
            return userOrderToReturn;
        }

        public async Task<OrderDtoReturnResult> GetOrderByUserID(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDtoReturnResult> UpdateOrder(int OrderId, OrderDTO order)
        {
            var OrderToCheck = await _orderRepository.GetOrderByOrderId(OrderId);
            if (OrderToCheck == null)
            {
                return null;
            }

            var _mapper = ApplicationMapper.InitializeAutomapper();
            var orderToAdd = _mapper.Map<Order>(order);
            orderToAdd.orderDate = OrderToCheck.orderDate;
            orderToAdd = await _orderRepository.UpdateOrder(OrderId, orderToAdd);
            var orderToReturn = _mapper.Map<OrderDTO>(orderToAdd);
            return new OrderDtoReturnResult { result = orderToReturn, OrderId = OrderId };
        }
    }
}
