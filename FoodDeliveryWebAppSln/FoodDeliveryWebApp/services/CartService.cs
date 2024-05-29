using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.cartDTO;
using FoodDeliveryWebApp.repositories;
using FoodDeliveryWebApp.repositories.dummymodel;

namespace FoodDeliveryWebApp.services
{
    public class CartService : ICartService
    {
        private readonly IRepository<int, Cart> _cartRepository;
        private readonly IRepository<int, Menu> _menuRepository;
        private readonly IRepository<int, CartDetails> _cartDetailsRepository;
        private readonly CartDetailsRepository _directCdRepo;

        public CartService( IRepository<int, Cart> cartRepository, IRepository<int,CartDetails> cartDetailsRepository, IRepository<int, Menu> _menu, CartDetailsRepository directCdRepo)
        {
            _cartRepository = cartRepository;
            _cartDetailsRepository = cartDetailsRepository;
            _menuRepository = _menu;
            _directCdRepo = directCdRepo;
        }

        //public async Task<CartDetails> AddCartAndDetails(CartAndDetailsDTO cartAndDetailsDTO)
        //{
        //    Cart cart = null;
        //    CartDetails cartDetails = null;

        //    try
        //    {
        //            cart = cartAndDetailsDTO;
        //        Menu menu = new Menu();
        //        CartDetails cdTemp = new CartDetails();
        //        cdTemp = await _cartDetailsRepository.Get(cartAndDetailsDTO.CartId);
        //        menu = await _menuRepository.Get(cartAndDetailsDTO.FId);
        //        cartDetails = MapCartDTOToDetails(cartAndDetailsDTO, cdTemp.Total, menu.UnitPrice);
        //            cart = await _cartRepository.Add(cart);
        //            cartDetails.CartId = cart.CartId;
        //            cartDetails = await _cartDetailsRepository.Add(cartDetails);
        //            return cartDetails;
        //        //if(_cartRepository.Get(cartAndDetailsDTO.CartId) != null)
        //        //{

        //        //}
        //        //else
        //        //{
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        if (_cartRepository.Get(cartAndDetailsDTO.CartId) != null)
        //        {
        //            cart = cartAndDetailsDTO;
        //            Menu menu = new Menu();
        //            CartDetails cdTemp = new CartDetails();
        //            cdTemp = await _cartDetailsRepository.Get(cartAndDetailsDTO.CartId);
        //            menu = await _menuRepository.Get(cartAndDetailsDTO.FId);
        //            cartDetails = MapCartDTOToDetails(cartAndDetailsDTO, cdTemp.Total, menu.UnitPrice);
        //            cart = await _cartRepository.Update(cart);
        //            cartDetails.CartId = cart.CartId;
        //            cartDetails = await _cartDetailsRepository.Add(cartDetails);
        //            return cartDetails;
        //        }
        //        else
        //        throw new UnableToRegisterException("Unable to add to cart at the moment");
        //    }


        public async Task<CartDetails> AddCartAndDetails(CartAndDetailsDTO cartAndDetailsDTO)
        {
            CartDetails newCartDetails=null;
            try
            {
                Cart cart = await _cartRepository.Get(cartAndDetailsDTO.CartId);
                if (cart != null)
                {
                    cart = await _cartRepository.Update(cartAndDetailsDTO);
                }
                else
                {
                    cart = await _cartRepository.Add(cartAndDetailsDTO); 
                }
                CartDetails existingCartDetails = await _directCdRepo.GetByCartId(cartAndDetailsDTO.CartId);

                Menu menu = await _menuRepository.Get(cartAndDetailsDTO.FId);

                newCartDetails = MapCartDTOToDetails(cartAndDetailsDTO, existingCartDetails?.Total ?? 0, menu.UnitPrice);

                newCartDetails.CartId = cart.CartId;

                newCartDetails = await _cartDetailsRepository.Add(newCartDetails);

                return newCartDetails;
            }
            catch (Exception ex)
            {
                if(newCartDetails != null)
                {

                await RevertCartDetailsInsert(newCartDetails);
                }
                await RevertCartInsert(cartAndDetailsDTO);
                throw new UnableToRegisterException("Unable to add to cart at the moment", ex);
            }
        }


        private async Task RevertCartInsert(Cart admin)
        {
            await _cartRepository.Delete(admin.CartId);
        }

        private async Task RevertCartDetailsInsert(CartDetails admin)
        {
            await _cartDetailsRepository.Delete(admin.CartDetailsId);
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

        public async Task<CartTotalResult> GetTotal(int id)
        {
            var cd = await _directCdRepo.GetTotalForCart(id);
            int cid = cd[0].CartId;
            int sumtotal = 0;
            List<CartItem> list = new List<CartItem>();
            CartItem ci;
            foreach (var item in cd)
            {
                ci = new CartItem() {Fid= item.FId,Quantity= item.Qty_ordered,Total= item.Total };
                list.Add(ci);
                sumtotal += item.Total;
            }
            CartTotalResult result = new CartTotalResult() { Id=cid, CartItems = list, Total = sumtotal};
            return(result);
        }

        public async Task<string> DeleteCart(int id)
        {
            var delcart = await _cartRepository.Get(id);
            foreach (var item in delcart.CartDetails)
            {
                if(item.CartId == id)
                {
                    var delete = await _cartDetailsRepository.Delete(item.CartDetailsId);
                }
            }
             var deldetails = _cartRepository.Delete(id);
            return ("Deleted Successfully!");
        }
    }
}



//Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzE2OTEyOTk5fQ.Qh - gn4a6H0V5HEKlo4Xlpy5w9y5OCSA - vWIwYVgEA0I