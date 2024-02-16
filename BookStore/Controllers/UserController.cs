using BusinessLayer.Interface;
using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody]UserRequest user)
        {
            try
            {
                UserResponse mainUser = userBusiness.Register(user);
                return Ok(new { success = true, message = "Registration successful",data = mainUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new {success = false,message = "Registeration failed",data = ex.Message});
            }
        }

        [HttpPut("login")]
        public IActionResult Login([FromBody]loginRequest user)
        {
            try
            {
                string val = userBusiness.Login(user);
                return Ok(new { success = true, message = "login successful",data = val});
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "login failed", data = ex.Message});
            }
        }
        
        [HttpPut("ForgetPassword")]
        public IActionResult forgetPassword([FromBody]ForgetPasswordRequest forgetPass)
        {
            try
            {
                string tokken = userBusiness.ForgetPassword(forgetPass);
                return Ok(new { success = true, message = "tokken generate", data = tokken });
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = "give correct email", data = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword([FromBody]ResetPasswordRequest req)
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool val = userBusiness.ResetPassword(req,userId);
                if (val == true)
                {
                    return Ok(new { success = true, message = "User password updated" });
                }
                else
                    return BadRequest(new { success = false, message = "User not found" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("LoggedInUser")]
        public IActionResult getUser()
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                UserResponse user = userBusiness.getUser(userId);
                return Ok(new {success = true,message = "user found",data = user});
            }catch(Exception e)
            {
                return BadRequest(new {success = false,message = "user not found",data = e.Message});
            }
        }
        [Authorize]
        [HttpPut("updateUser")]
        public IActionResult updateUser(UserRequest req)
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                UserResponse user = userBusiness.UpdateUserId(req, userId);
                return Ok(new { success = true, message = "user update", data = user });
            }
            catch(Exception e) 
            {
                return BadRequest(new {success = false,message ="not able to update the user",data = e.Message});
            }
        }
    }
}
