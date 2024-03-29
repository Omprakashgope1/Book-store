﻿using BusinessLayer.Interface;
using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewBusiness reviewBusiness;
        public ReviewController(IReviewBusiness reviewBusiness)
        {
            this.reviewBusiness = reviewBusiness;
        }
        [Authorize]
        [HttpPost("AddReviews")]
        public IActionResult AddReviews(AddReview reviews)
        {
            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                IEnumerable<ReviewResponse> review = reviewBusiness.AddReviews(userId, reviews);
                return Ok(new {success = true,message = "review added",data = review});
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "not able add the review", data = ex.Message });
            }
        }
        [HttpGet("GetReviews")]
        public IActionResult GetAllReviews(long bookId)
        {
            try
            {
                IEnumerable<ReviewResponse> response = reviewBusiness.GetAllReviews(bookId);
                return Ok(new {success = true,message = "reviews",data = response});
            }
            catch(Exception ex) 
            {
                return BadRequest(new {success = false,message = "review not found",data = ex.Message});
            }
        }
        [HttpGet("GetAllReviews")]
        public IActionResult GetReviews()
        {
            try
            {
                IEnumerable<ReviewResponse> response = reviewBusiness.GetReviews();
                return Ok(new { success = true, message = "reviews found", data = response });
            }catch(Exception ex) 
            {
                return BadRequest(new {success = false,message = "reviews not found",data = ex.Message});
            }
        }
    }
}
