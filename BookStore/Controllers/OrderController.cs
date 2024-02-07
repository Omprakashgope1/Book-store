using BusinessLayer.Interface;
using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderBusiness orderBusiness;
        public OrderController(IOrderBusiness orderBusiness)
        {
            this.orderBusiness = orderBusiness;
        }
        [HttpPost("AddOrder")]
        public IActionResult AddOrder([FromBody]AddOrder order)
        {
            try
            {
                long userId = (long)Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                List<BookResponse> orders = orderBusiness.Addorder(order, userId);
                return Ok(new { success = true, message = "added to orders", data = orders });
            }
            catch(Exception ex) 
            {
               return BadRequest(new {success = false,message = "not able to add order",data = ex.Message});
            }
        }
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                long userId = (long)Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                List<BookResponse> orders = orderBusiness.GetAllOrders(userId);
                return Ok(new { success = true, message = "added to orders", data = orders });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "not able to add order", data = ex.Message });
            }
        }
        [HttpPost("AddAll")]
        public IActionResult AddOrders([FromBody]List<AddOrder> orders) 
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                string email = Convert.ToString(User.Claims.FirstOrDefault(x => x.Type == "Email").Value);
                List<BookResponse> bookList = orderBusiness.OrderCart(orders, userId, email);
                return Ok(new { success = true, message = "ordered", data = bookList });
            }
            catch(Exception ex) 
            {
                return BadRequest(new { success = false, message = "not able to order", data = ex.Message });
            }
        }
    }
}
