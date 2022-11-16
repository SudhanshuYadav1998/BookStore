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
    public class FeedBackRL:IFeedBackRL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        public FeedBackRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddFeedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Rating", addFeedback.Rating);
                    cmd.Parameters.AddWithValue("@Comment", addFeedback.Comment);
                    cmd.Parameters.AddWithValue("@BookId", addFeedback.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();

                    if (result != 1)
                    {
                        return addFeedback;
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

        public List<FeedbackResponse> GetAllFeedbacks(int bookId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    List<FeedbackResponse> feedbackResponse = new List<FeedbackResponse>();
                    SqlCommand cmd = new SqlCommand("spGetAllFeedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            FeedbackResponse feedback = new FeedbackResponse();
                            feedback.FeedbackId = Convert.ToInt32(rdr["FeedbackId"]);
                            feedback.BookId = Convert.ToInt32(rdr["BookId"]);
                            feedback.UserId = Convert.ToInt32(rdr["UserId"]);
                            feedback.Comment = Convert.ToString(rdr["Comment"]);
                            feedback.Rating = Convert.ToInt32(rdr["Rating"]);
                            feedback.FullName = Convert.ToString(rdr["FullName"]);
                            feedbackResponse.Add(feedback);
                        }
                        con.Close();
                        return feedbackResponse;
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
