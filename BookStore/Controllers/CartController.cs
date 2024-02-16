using BusinessLayer.Interface;
using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartBusiness _cartBusiness;
        public CartController(ICartBusiness cartBusiness)
        {
            _cartBusiness = cartBusiness;
        }
        [HttpPost("addToCart")]
        public IActionResult AddToCart([FromBody]AddToCartRequest addToCartRequest)
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                List<BookResponse> cart_item = _cartBusiness.AddToCart(addToCartRequest, userId);
                return Ok(new { success = true, message = "added to cart" ,data = cart_item});
            }
            catch (Exception ex) 
            {
                return BadRequest(new { success = false, message = "not able to add to cart" ,data = ex.Message});
            }
        }
        [HttpGet("GetCartPrice")]
        public IActionResult GetPriceInCart()
        {
            try
            {
                long userId = (long)(Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value));
                double cart_price = _cartBusiness.GetPriceInCart(userId);
                return Ok(new { success = true, message = "Price of cart", data = cart_price });
            }catch(Exception ex) 
            {
                return BadRequest(new {success = false,message = "Not able to find the cart price",data =  ex.Message});
            }
           

        }
        [HttpGet("GetCart")]
        public IActionResult GetCartBooks()
        {
            try
            {
                long userId = (long)(Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value));
                List<BookResponse> cart_price = _cartBusiness.GetCartBooks(userId);
                return Ok(new { success = true, message = "Price of cart", data = cart_price });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Not able to find the cart price", data = ex.Message });
            }
        }
        [HttpPut("UpdateQuantity")]
        public IActionResult UpdateQuantity([FromBody]QuantityUpdateRequest req) 
        {
            try
            {
                long userId = (long)(Convert.ToInt64((User.Claims.FirstOrDefault(x => x.Type == "UserId").Value)));
                _cartBusiness.UpdateBookQuantity(userId, req);
                return Ok(new { success = true, message = "updated quanity" });
            }
            catch(Exception ex) 
            {
                return BadRequest(new {success = false,message = ex.Message});
            }
        }
        [HttpDelete("removeCart")]
        public IActionResult removeCart([FromQuery] long bookId)
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                _cartBusiness.removeCart(bookId,userId);
                return Ok(new { success = true, message = "cart removed" }); ;
            }
            catch (Exception e)
            {
                return BadRequest(new {success = false,message = "not able to remove the cart",data = e.Message});
            }
        }
    }
}
