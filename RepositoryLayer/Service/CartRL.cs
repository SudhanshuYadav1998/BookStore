using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Service
{
    public class CartRL:ICartRL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public AddToCart AddToCart(AddToCart addCart, int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddToCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BooksQty", addCart.BooksQty);
                    cmd.Parameters.AddWithValue("@BookId", addCart.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {
                        return addCart;
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
        }
        public string RemoveFromCart(int cartId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spRemoveFromCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId", cartId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Item Removed from cart Successfully";
                    }
                    else
                    {
                        return "Failed to Remove item from cart";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        public List<CartResponse> GetAllCart(int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    List<CartResponse> cartResponses = new List<CartResponse>();
                    SqlCommand cmd = new SqlCommand("spGetAllCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            CartResponse cart = new CartResponse();
                            cart.BookId = Convert.ToInt32(rdr["BookId"]);
                            cart.UserId = Convert.ToInt32(rdr["UserId"]);
                            cart.CartId = Convert.ToInt32(rdr["CartId"]);
                            cart.BookName = Convert.ToString(rdr["BookName"]);
                            cart.Author = Convert.ToString(rdr["Author"]);
                            cart.BookImage = Convert.ToString(rdr["BookImage"]);
                            cart.DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"]);
                            cart.ActualPrice = Convert.ToDouble(rdr["ActualPrice"]);
                            cart.BooksQty = Convert.ToInt32(rdr["BooksQty"]);
                            cart.Stock = Convert.ToInt32(rdr["Quantity"]);
                            cartResponses.Add(cart);
                        }

                        con.Close();
                        return cartResponses;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

    }
}
