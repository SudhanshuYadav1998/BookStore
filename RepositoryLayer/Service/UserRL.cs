using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL: IUserRL
    {
         SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public UserRL(IConfiguration configuration)
        {
            this.configuration=configuration;
        }

       public RegistrationModel AddUser(RegistrationModel usermodel)
        {
            this.sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using(sqlConnection)
            try
            {

                SqlCommand command = new SqlCommand("spAddUser", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                this.sqlConnection.Open();

                command.Parameters.AddWithValue("@fullname", usermodel.FullName);
                    command.Parameters.AddWithValue("@email", usermodel.Email);
                    command.Parameters.AddWithValue("@password", usermodel.Password);
                    command.Parameters.AddWithValue("@mobilenumber", usermodel.Mobile_Number);
                    var result= command.ExecuteNonQuery();
                   
                if(result!=0)
                {
                    return usermodel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.sqlConnection.Close();
            }

        }
        public string UserLogin(UserLogin userlogin)
        {
            try
            {
                //   if (Decrypt(Enteredlogin.Password)==userlogin.Password)
                if ((Enteredlogin.Password) == userlogin.Password)


                {
                    string token = GenerateSecurityToken(Enteredlogin.Email, Enteredlogin.UserId);
                    return token;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateSecurityToken(string Email, long UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("UserId",UserId.ToString())
            };
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"],
              _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
