using BusinessLayer.Interface;
using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CartBusiness :ICartBusiness
    {
        private ICartRepo _cartRepo;
        public CartBusiness(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public List<BookResponse> AddToCart(AddToCartRequest cartRequest, long userId)
        {
           return _cartRepo.AddToCart(cartRequest, userId);
        }

        public List<BookResponse> GetCartBooks(long userId)
        {
            return _cartRepo.GetCartBooks(userId);
        }

        public double GetPriceInCart(long userId)
        {
            return _cartRepo.GetPriceInCart(userId);
        }
        public void UpdateBookQuantity(long userId, QuantityUpdateRequest req)
        {
            _cartRepo.UpdateBookQuantity(userId, req);
        }
        public void removeCart(long bookId, long userId)
        {
            _cartRepo.removeCart(bookId, userId);
        }
    }
}
