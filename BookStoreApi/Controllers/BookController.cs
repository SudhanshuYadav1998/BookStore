using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [HttpPost("Add")]
        [Authorize(Roles = Role.Admin)]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

        public IActionResult AddBook(AddBook addBook)
        {
            try
            {
                var res = bookBL.AddBook(addBook);
                if (res != null)
                {
                    return Created("", new { success = true, message = "Book Added sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Add Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllBook")]
        public IActionResult GetAllBook()
        {
            try
            {
                var res = bookBL.GetAllBooks();
                if (res != null)
                {
                    return Created("", new { data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to getall Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetBookById")]
        public IActionResult GetBookbyId(int BookId)
        {
            try
            {
                var res = bookBL.GetBookById(BookId);
                if (res != null)
                {
                    return Created("", new { data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to get Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
