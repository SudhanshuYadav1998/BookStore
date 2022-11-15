using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using CommonLayer.Models;

namespace BookStoreApi.Controllers
{
    [Authorize(Roles = Role.User)]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [HttpPost("Add")]
        public IActionResult AddToWishlist(AddToCart addToCart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartBL.AddToCart(addToCart, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added to cart", data = result });

                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpDelete("Delete")]
        public IActionResult RemoveFromlist(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartBL.RemoveFromCart(cartId);
                if (result != null)
                {
                    return this.Ok(new { success = true });

                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
