using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.cartDTO;

namespace FoodDeliveryWebApp.services
{
    public class CartService : ICartService
    {
        private readonly IRepository<int, Cart> _cartRepository;
        private readonly IRepository<int, Menu> _menuRepository;
        private readonly IRepository<int, CartDetails> _cartDetailsRepository;

        public CartService( IRepository<int, Cart> cartRepository, IRepository<int,CartDetails> cartDetailsRepository, IRepository<int, Menu> _menu)
        {
            _cartRepository = cartRepository;
            _cartDetailsRepository = cartDetailsRepository;
            _menuRepository = _menu;
        }

        public async Task<CartDetails> AddCartAndDetails(CartAndDetailsDTO cartAndDetailsDTO)
        {
            Cart cart = null;
            CartDetails cartDetails = null;

            try
            {
                    cart = cartAndDetailsDTO;
                Menu menu = new Menu();
                CartDetails cdTemp = new CartDetails();
                cdTemp = await _cartDetailsRepository.Get(cartAndDetailsDTO.CartId);
                menu = await _menuRepository.Get(cartAndDetailsDTO.FId);
                cartDetails = MapCartDTOToDetails(cartAndDetailsDTO, cdTemp.Total, menu.UnitPrice);
                    cart = await _cartRepository.Add(cart);
                    cartDetails.CartId = cart.CartId;
                    cartDetails = await _cartDetailsRepository.Add(cartDetails);
                    return cartDetails;
                //if(_cartRepository.Get(cartAndDetailsDTO.CartId) != null)
                //{
                   
                //}
                //else
                //{
                //}

            }
            catch (Exception ex)
            {
                if (_cartRepository.Get(cartAndDetailsDTO.CartId) != null)
                {
                    cart = cartAndDetailsDTO;
                    Menu menu = new Menu();
                    CartDetails cdTemp = new CartDetails();
                    cdTemp = await _cartDetailsRepository.Get(cartAndDetailsDTO.CartId);
                    menu = await _menuRepository.Get(cartAndDetailsDTO.FId);
                    cartDetails = MapCartDTOToDetails(cartAndDetailsDTO, cdTemp.Total, menu.UnitPrice);
                    cart = await _cartRepository.Update(cart);
                    cartDetails.CartId = cart.CartId;
                    cartDetails = await _cartDetailsRepository.Add(cartDetails);
                    return cartDetails;
                }
                else
                throw new UnableToRegisterException("Unable to add to cart at the moment");
            }
           
        }

        private CartDetails MapCartDTOToDetails(CartAndDetailsDTO cartAndDetailsDTO, int total, int price)
        {
            CartDetails cd = new CartDetails();
            
            cd.CartId = cartAndDetailsDTO.CartId;
            cd.FId = cartAndDetailsDTO.FId;
            cd.Qty_ordered = cartAndDetailsDTO.Quantity;
            cd.Total = total + (cartAndDetailsDTO.Quantity * price);
            return cd;
        }

        public async Task<Cart> GetCart(int id)
        {
            var cd = await _cartRepository.Get(id);
            return cd;
        }
    }
}



//Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzE2OTEyOTk5fQ.Qh - gn4a6H0V5HEKlo4Xlpy5w9y5OCSA - vWIwYVgEA0I