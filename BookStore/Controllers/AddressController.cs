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
                IEnumerable<AddressResponse> res=addressBusiness.AddAddress(addAddress);
                return Ok(new { success = true, message = "added address", data = res});
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = true, message = "not able to add the address" });
            }
        }
        [HttpGet("getAddress")]
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
        [HttpPut("updateAddress")]
        public IActionResult UpdateAddress([FromBody]updateAddressRequest req)
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                IEnumerable<AddressResponse> addresses =addressBusiness.UpdateAddress(req,userId);
                return Ok(new { success = true, message = "Address updated", data = addresses });
            }catch(Exception e)
            {
                return BadRequest(new { success = false, message = "Address not updated", data = e.Message });
            }
        }
    }
}
