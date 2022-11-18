using BusinessLayer.Service;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BookStoreApi.Controllers
{
    [Authorize(Roles = Role.User)]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersBL ordersBL;
        public OrdersController(IOrdersBL ordersBL)
        {
            this.ordersBL = ordersBL;
        }

        [HttpPost("Add")]
        public IActionResult AddOrder(AddOrder addOrder)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = ordersBL.AddOrder(addOrder, userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Order" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("Get")]
        public IActionResult GetAllOrder()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = ordersBL.GetAllOrders( userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to get all Order" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
