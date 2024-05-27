using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.orderDTO;
using FoodDeliveryWebApp.repositories;
using FoodDeliveryWebApp.repositories.dummymodel;

namespace FoodDeliveryWebApp.services
{
    public class OrderService : IOrderService
    {


        private readonly IRepository<int, Order> _orderRepository;
        private readonly IRepository<int, Menu> _menuRepository;
        private readonly IRepository<int, OrderDetails> _orderDetailsRepository;
        private readonly OrderDetailsRepository _directOdRepo;
        private readonly CartDetailsRepository _directCdRepo;
        private readonly IRepository<int, Cart> _cartRepository;
        private readonly IRepository<int, CartDetails> _cartDetailsRepository;


        private readonly ICartService _orderService;
        private readonly IAdminServices _adminService;

        public OrderService(IRepository<int, Order> orderRepository, IRepository<int, OrderDetails> orderDetailsRepository, IRepository<int, Menu> _menu, OrderDetailsRepository directOdRepo, CartDetailsRepository directCdRepo, ICartService orderService, IAdminServices adminService, IRepository<int,Cart> cartRepository, IRepository<int, CartDetails> cartDetailsRepository )
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _menuRepository = _menu;
            _directOdRepo = directOdRepo;
            _orderService = orderService;
            _adminService = adminService;
            _cartRepository = cartRepository;
            _cartDetailsRepository = cartDetailsRepository;
            _directCdRepo = directCdRepo;
        }
        public async Task<string> AddOrderAndDetails(OrderAndDetailsDTO orderAndDetailsDTO)
        {
            try
            {
                Cart cartid = await _cartRepository.Get( orderAndDetailsDTO.CartId );
                Order orderid = new Order() { OId=orderAndDetailsDTO.OId, CartId= cartid.CartId, CustomerId=cartid.CustomerId, Status="Confirmed"};
                var order = await _orderRepository.Add(orderid);
                //IEnumerable<CartDetails> cartinfo = new List<CartDetails>() ;
                IEnumerable<CartDetails> cd = await _directCdRepo.GetallByCartId(order.CartId);
                int sumtotal = 0;

                OrderDetails newOrderDetails;
                OrderDetails orderdetails;
                foreach (var cartdetails in cd )
                {
                 newOrderDetails = MapOrderDTOToDetails( orderAndDetailsDTO.OId, cartdetails.FId, cartdetails.Total, cartdetails.Qty_ordered);
                     orderdetails = await _orderDetailsRepository.Add( newOrderDetails );
                    sumtotal += newOrderDetails.Total;
                    var updatingStock = await _adminService.UpdateStock(newOrderDetails.FId, newOrderDetails.Qty_ordered);
                }
                if(order.Status =="Confirmed")
                {
                    var deletion = await _orderService.DeleteCart(order.CartId);
                }

                return ("Successfully placed order");
               
            }
            catch (Exception ex)
            {
                throw new UnableToRegisterException("Unable to add to order at the moment", ex);
            }
        }

        private OrderDetails MapOrderDTOToDetails(int Oid,int Fid, int total, int quantity)
        {
            OrderDetails cd = new OrderDetails();

            cd.OId = Oid;
            cd.FId = Fid;
            cd.Qty_ordered = quantity;
            cd.Total = total;
            return cd;
        }

        public async Task<Order> GetOrder(int id)
        {
            var cd = await _orderRepository.Get(id);
            return cd;
        }

        public async Task<OrderTotalResult> GetTotal(int id)
        {
            var cd = await _directOdRepo.GetTotalForOrder(id);
            int cid = cd[0].OId;
            int sumtotal = 0;
            List<OrderItem> list = new List<OrderItem>();
            OrderItem ci;
            foreach (var item in cd)
            {
                ci = new OrderItem() { Fid = item.FId, Quantity = item.Qty_ordered, Total = item.Total };
                list.Add(ci);
                sumtotal += item.Total;
            }
            OrderTotalResult result = new OrderTotalResult() { Id = cid, OrderItems = list, Total = sumtotal };
            return (result);
        }
    }
}
