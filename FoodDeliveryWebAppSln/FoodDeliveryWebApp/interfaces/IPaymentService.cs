using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.paymentDTO;

namespace FoodDeliveryWebApp.interfaces
{
    public interface IPaymentService
    {
        public Task<Payment> AddPayment(PaymentDTO payment);
        public Task<Payment> CancelPayment(int payment);
        public Task<Payment> ViewPayment(int payment);
    }
}
