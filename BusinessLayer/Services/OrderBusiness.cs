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
    public class OrderBusiness:IOrderBusiness
    {
        private IOrderRepo _orderRepo;
        public OrderBusiness(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public List<BookResponse> Addorder(AddOrder orders, long userId)
        {
            return _orderRepo.Addorder(orders, userId);
        }
        public List<BookResponse> GetAllOrders(long userId)
        {
            return _orderRepo.GetAllOrders(userId);
        }
        public List<BookResponse> OrderCart(List<AddOrder> orders, long userId, string email)
        {
            return _orderRepo.OrderCart(orders, userId, email);
        }
    }
}
