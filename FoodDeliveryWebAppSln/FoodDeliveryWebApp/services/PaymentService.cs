using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.paymentDTO;
using FoodDeliveryWebApp.repositories;

namespace FoodDeliveryWebApp.services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<int, Payment> _payRepository;
        private readonly IRepository<int, Order> _orderRepository;
        private readonly IRepository<int, OrderDetails> _orderDetailsRepository;
        private readonly OrderDetailsRepository _directOdRepo;

        public PaymentService(IRepository<int, Payment> payRepository, IRepository<int, Order> orderRepository, IRepository<int, OrderDetails> orderDetailsRepository, OrderDetailsRepository directOdRepo)
        {
            _payRepository = payRepository;
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _directOdRepo = directOdRepo;
            
        }

        public async Task<Payment> AddPayment (PaymentDTO payment)
        { 
            Order order_id = await _orderRepository.Get(payment.OId);
            IEnumerable<OrderDetails> order_details = await _directOdRepo.GetallByOrderId(payment.OId);
            int total = 0;
            foreach(OrderDetails details in order_details)
            {
                total+= details.Total;
            }
            Payment pay = MapOrderToPayment(total, order_id, payment);
            pay = await _payRepository.Add(pay);
            return pay;
        }

        private Payment MapOrderToPayment(int total, Order order_id, PaymentDTO pay)
        {
            Payment payment = new Payment();
            payment.OId = order_id.OId;
            payment.CustomerId = order_id.CustomerId;
            payment.PaymentMethod = pay.PaymentMethod;
            payment.Status = "Paid";
            payment.Amount = total;
            return payment;
        }

        public async Task<Payment> CancelPayment(int payment) 
        {
            Payment pay = await _payRepository.Delete(payment);
            return pay;
        }
        public async Task<Payment> ViewPayment (int payment)
        {
            Payment pay = await _payRepository.Get(payment);
            return pay;
        }
    }
}
