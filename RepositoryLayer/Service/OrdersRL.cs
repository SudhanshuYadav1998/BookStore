using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Service
{
    public class OrdersRL:IOrdersRL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        public OrdersRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string AddOrder(AddOrder addOrder, int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                try
                {
                    List<CartResponse> cartList = new List<CartResponse>();
                    List<string> orderList = new List<string>();

                    cmd = new SqlCommand("spGetAllCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CartResponse cart = new CartResponse();
                            cart.UserId = Convert.ToInt32(reader["UserId"]);
                            cart.BookId = Convert.ToInt32(reader["BookId"]);
                            cartList.Add(cart);
                        }
                        reader.Close();

                        foreach (var cart in cartList)
                        {
                            cmd = new SqlCommand("spAddOrders", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@AddressId", addOrder.AddressId);
                            int result = Convert.ToInt32(cmd.ExecuteScalar());
                            if (result != 2 && result != 3 && result != 4)
                            {
                                orderList.Add("Item Added to OrderList");
                            }
                            else
                            {
                                return null;
                            }
                        }
                        con.Close();
                        return "Congratulations! Order Placed Successfully";
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<OrdersResponse> GetAllOrders(int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    List<OrdersResponse> ordersResponse = new List<OrdersResponse>();
                    SqlCommand cmd = new SqlCommand("spGetAllOrders", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            OrdersResponse order = new OrdersResponse();
                            order.OrderId = Convert.ToInt32(rdr["OrderId"]);
                            order.AddressId = Convert.ToInt32(rdr["AddressId"]);
                            order.BookId = Convert.ToInt32(rdr["BookId"]);
                            order.UserId = Convert.ToInt32(rdr["UserId"]);
                            order.BooksQty = Convert.ToInt32(rdr["BooksQty"]);
                            order.OrderDateTime = Convert.ToDateTime(rdr["OrderDate"]);
                            order.OrderDate = order.OrderDateTime.ToString("dd-MMM-yyyy");
                            order.OrderPrice = Convert.ToDouble(rdr["OrderPrice"]);
                            order.ActualPrice = Convert.ToDouble(rdr["ActualPrice"]);
                            order.BookName = Convert.ToString(rdr["BookName"]);
                            order.BookImage = Convert.ToString(rdr["BookImage"]);
                            order.Author = Convert.ToString(rdr["Author"]);                           
                            ordersResponse.Add(order);
                        }
                        con.Close();
                        return ordersResponse;
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
        public string DeleteOrder(int OrderId,int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spRemoveFromOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrderId", OrderId);
                    cmd.Parameters.AddWithValue("@UserId", userId);


                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Order Deleted Successfully";
                    }
                    else
                    {
                        return "Failed to Delete the Order";
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
