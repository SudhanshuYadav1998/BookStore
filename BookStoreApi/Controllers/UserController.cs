using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
        [HttpPost("ForgotPassword")]
        public IActionResult ForgetPassword(ForgotPassword forgotpassword)
        {
            try
            {
                var result = userBL.ForgetPassword(forgotpassword.Email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Mail Sent Succesfully" });
                }
                else
                    return this.BadRequest(new { success = false, message = "Something Goes Wrong" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(email, resetPassword.Password);
                if (result != false)
                {
                    return this.Ok(new { success = true, message = "Password Changed Succesfully" });
                }
                else
                    return this.BadRequest(new { success = false, message = "Something Goes Wrong" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
