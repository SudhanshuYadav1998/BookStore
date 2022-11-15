using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using CommonLayer.Models;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class WishlistRL:IWishlistRL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        public WishlistRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddToWishList(int bookId, int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddToWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {
                        return "Added to WishList Successfully";
                    }
                    else
                    {
                        return "Failed to Add to WishList";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        public List<WishlistResponse> GetAllWishList(int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    List<WishlistResponse> wishListResponse = new List<WishlistResponse>();
                    SqlCommand cmd = new SqlCommand("spGetAllWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            WishlistResponse wishList = new WishlistResponse
                            {
                                BookId = Convert.ToInt32(rdr["BookId"]),
                                UserId = Convert.ToInt32(rdr["UserId"]),
                                WishListId = Convert.ToInt32(rdr["WishListId"]),
                                BookName = Convert.ToString(rdr["BookName"]),
                                Author = Convert.ToString(rdr["Author"]),
                                BookImage = Convert.ToString(rdr["BookImage"]),
                                DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"]),
                                ActualPrice = Convert.ToDouble(rdr["ActualPrice"])
                            };
                            wishListResponse.Add(wishList);
                        }
                        con.Close();
                        return wishListResponse;
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
        public string RemoveFromWishList(int wishListId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spRemoveFromWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@WishListId", wishListId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Item Removed from WishList Successfully";
                    }
                    else
                    {
                        return "Failed to Remove item from WishList";
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
