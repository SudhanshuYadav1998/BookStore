using CommonLayer.Models;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Service
{
    public class AdminRL:IAdminRL
    {
        SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public AdminRL( IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AdminLogin(LoginModel login)
        {
            this.sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand command = new SqlCommand("spAdminLogin", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    this.sqlConnection.Open();

                    command.Parameters.AddWithValue("@email", login.Email);
                    command.Parameters.AddWithValue("@password", login.Password);
                    SqlDataReader data = command.ExecuteReader();
                    if (data.HasRows)
                    {
                        int AdminId = 0;

                        while (data.Read())
                        {
                            login.Email = Convert.ToString(data["Email"]);
                            login.Password = Convert.ToString(data["Password"]);
                            AdminId = Convert.ToInt32(data["AdminId"]);
                        }
                        string token = GenerateSecurityToken(login.Email, AdminId);
                        return token;
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
                finally { this.sqlConnection.Close(); }

        }


        private string GenerateSecurityToken(string Email, int AdminId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:SecKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Email,Email),
                new Claim("AdminId",AdminId.ToString())
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
