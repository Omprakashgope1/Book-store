using BusinessLayer.Interface;
using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookBusiness bookBusiness;
        public BookController(IBookBusiness bookBusiness) 
        {
            this.bookBusiness = bookBusiness;
        }
        [HttpGet("BookById")]
        public IActionResult getBookById(int id)
        {
            try
            {
                BookResponse book = bookBusiness.getBookById(id);
                return Ok(new { success = true, message = "book.title", data = book });
            }
            catch (Exception ex) 
            {
                return BadRequest(new {success = false,message = "book not found",data = ex.Message});
            }
        }
        [HttpGet("GetAll")]
        public IActionResult getAllBooks()
        {
            try
            {
                List<BookResponse> books = new List<BookResponse>();
                books = bookBusiness.getAllBooks();
                return Ok(new { success = true, message = "book", data = books });
            }
            catch(Exception ex)
            {
                return Ok(new { success = false, message = "not able to extract details", data = ex.Message });
            }
        }
        [HttpGet("get_by_author")]
        public IActionResult bookDetailsByAuthor([FromQuery]BookAuthorRequest req)
        {
            try
            {
                BookResponse book = bookBusiness.bookDetailsByAuthor(req);
                return Ok(new { success = true, message = book.Title, data = book });
            }catch(Exception e)
            {
                return BadRequest(new { success = false, message = "not able to find that particular book", data = e.Message });
            }
        }
    }
}
