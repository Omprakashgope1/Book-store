using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IOrderBusiness
    {
        public List<BookResponse> Addorder(AddOrder orders, long userId);
        public List<BookResponse> GetAllOrders(long userId);
        public List<BookResponse> OrderCart(List<AddOrder> orders, long userId, string email);
    }
}
