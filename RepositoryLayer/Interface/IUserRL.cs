using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public RegistrationModel AddUser(RegistrationModel usermodel);
        public string UserLogin(LoginModel login);
    }
}
