using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL: IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public RegistrationModel AddUser(RegistrationModel usermodel)
        {
            try
            {
                return this.userRL.AddUser(usermodel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UserLogin(LoginModel login)
        {
            try
            {
                return this.userRL.UserLogin(login);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgetPassword(string Email)
        {
            try
            {
                return this.userRL.ForgetPassword(Email);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string EmailId, string Password)
        {
            try
            {
                return this.userRL.ResetPassword(EmailId, Password);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
