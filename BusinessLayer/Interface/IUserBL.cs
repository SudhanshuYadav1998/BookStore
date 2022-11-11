using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public RegistrationModel AddUser(RegistrationModel usermodel);
        public string UserLogin(LoginModel login);
    }
}
