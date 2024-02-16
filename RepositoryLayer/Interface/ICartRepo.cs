using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICartRepo
    {
        public List<BookResponse> AddToCart(AddToCartRequest cartRequest, long userId);
        public double GetPriceInCart(long userId);
        public List<BookResponse> GetCartBooks(long userId);
        public void UpdateBookQuantity(long userId, QuantityUpdateRequest req);
        public void removeCart(long bookId, long userId);
    }
}
