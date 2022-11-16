using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;

namespace BookStoreApi.Controllers
{
    [Authorize(Roles = Role.User)]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackBL feedBackBL;
        public FeedBackController(IFeedBackBL feedBackBL)
        {
            this.feedBackBL = feedBackBL;
        }
        [HttpPost("Add")]
        public IActionResult AddFeedback(AddFeedback addFeedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = feedBackBL.AddFeedback(addFeedback, userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Feedback Added sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Add Feedback" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllFeedbacks(int bookId)
        {
            try
            {
                var res = feedBackBL.GetAllFeedbacks(bookId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Feedbacks Retrieved sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Retrieve Feedbacks" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
