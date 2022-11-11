using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }
        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin(LoginModel userlogin)
        {
            try
            {
                var result = adminBL.AdminLogin(userlogin);


                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Admin Login Successful", data = result });
                }
                else

                    return this.BadRequest(new { success = false, message = "Something Goes Wrong,Login Unsuccessful" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
