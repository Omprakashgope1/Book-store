using BusinessLayer.Interface;
using BusinessLayer.Services;
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
    public class AddressController : ControllerBase
    {
        private IAddressBusiness addressBusiness;
        public AddressController(IAddressBusiness address) 
        {
            this.addressBusiness = address;
        }
        [HttpPost("addAddress")]
        public IActionResult AddAddress([FromBody] AddAddressRequest addAddress)
        {
            try
            {
                addAddress.userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                addressBusiness.AddAddress(addAddress);
                return Ok(new { success = true, message = "added address" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = true, message = "not able to add the address" });
            }
        }
        [HttpGet("get_address")]
        public IActionResult GetAddress() 
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                IEnumerable<AddressResponse> addresses = addressBusiness.GetAddress(userId);
                return Ok(new { success = true, message = "Address found", data = addresses });
            }
            catch (Exception e)
            {
                return BadRequest(new {success = false,message = "Address not found",data = e.Message});
            }
         }
    }
}
