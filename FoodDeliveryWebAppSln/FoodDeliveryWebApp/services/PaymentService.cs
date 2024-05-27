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
            OrderDetails order_details = await _directOdRepo.GetByOrderId(payment.OId);
            Payment pay = MapOrderToPayment(order_details, order_id, payment);
            pay = await _payRepository.Add(pay);
            return pay;
        }

        private Payment MapOrderToPayment(OrderDetails order_details, Order order_id, PaymentDTO pay)
        {
            Payment payment = new Payment();
            payment.OId = order_id.OId;
            payment.CustomerId = order_id.CustomerId;
            payment.PaymentMethod = pay.PaymentMethod;
            payment.Status = "Paid";
            payment.Amount = order_details.Total;
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
