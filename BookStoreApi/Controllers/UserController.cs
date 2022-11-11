using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult UserRegistration(RegistrationModel userRegistration)
        {
            try
            {
               RegistrationModel registrationModel= userBL.AddUser(userRegistration);
                if (registrationModel != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull",data=registrationModel});
                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost("Login")]
        public IActionResult UserLogin(LoginModel userlogin)
        {
            try
            {
                var result = userBL.UserLogin(userlogin);


                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
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
