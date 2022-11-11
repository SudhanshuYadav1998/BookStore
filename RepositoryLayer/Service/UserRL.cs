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
        public string UserLogin(LoginModel login)
        {
            this.sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    RegistrationModel registrationmodel = new RegistrationModel();
                    SqlCommand command = new SqlCommand("spUserLogin", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    this.sqlConnection.Open();

                    command.Parameters.AddWithValue("@email", login.Email);
                    command.Parameters.AddWithValue("@password", login.Password);
                    SqlDataReader data=command.ExecuteReader();
                    if(data.FieldCount!=0)
                    {
                        int UserId = 0;

                        while (data.Read())
                        {
                            login.Email = Convert.ToString(data["Email"]);
                            login.Password = Convert.ToString(data["Password"]);
                            UserId = Convert.ToInt32(data["UserId"]);
                        }
                        string token = GenerateSecurityToken(login.Email, UserId);
                        return token;
                    }
                    else
                    {
                        return null ;
                    }
                }
                catch(Exception e)
                {
                    throw e;
                }
                finally { this.sqlConnection.Close(); }
           
        }

        private string GenerateSecurityToken(string Email, int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:SecKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("UserId",UserId.ToString())
            };
            var token = new JwtSecurityToken(this.configuration["Jwt:Issuer"],
              this.configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
