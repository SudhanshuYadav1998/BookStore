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
    public class AddressRL:IAddressRL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        public AddressRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AddAddress AddAddress(AddAddress addAddress, int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Address", addAddress.Address);
                    cmd.Parameters.AddWithValue("@City", addAddress.City);
                    cmd.Parameters.AddWithValue("@State", addAddress.State);
                    cmd.Parameters.AddWithValue("@TypeId", addAddress.TypeId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return addAddress;
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
        public AddressModel UpdateAddress(AddressModel addressModel, int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUpdateAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AddressId", addressModel.AddressId);
                    cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressModel.City);
                    cmd.Parameters.AddWithValue("@State", addressModel.State);
                    cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return addressModel;
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


        public string DeleteAddress(int addressId, int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spDeleteAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AddressId", addressId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Address Deleted Successfully";
                    }
                    else
                    {
                        return "Failed to Delete Address";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        public List<AddressModel> GetAllAddresses(int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    List<AddressModel> addressResponse = new List<AddressModel>();
                    SqlCommand cmd = new SqlCommand("spGetAllAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            AddressModel address = new AddressModel();
                            address.Address = Convert.ToString(rdr["Address"]);
                            address.City = Convert.ToString(rdr["City"]);
                            address.State = Convert.ToString(rdr["State"]);
                            address.TypeId = Convert.ToInt32(rdr["TypeId"]);
                            address.AddressId = Convert.ToInt32(rdr["AddressId"]);
                            addressResponse.Add(address);
                        }
                        con.Close();
                        return addressResponse;
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
