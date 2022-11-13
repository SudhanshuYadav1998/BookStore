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
    public class BookRL:IBookRL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;

        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookModel AddBook(AddBook addBook)
        {

            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName", addBook.BookName);
                    cmd.Parameters.AddWithValue("@Author", addBook.Author);
                    cmd.Parameters.AddWithValue("@BookImage", addBook.BookImage);
                    cmd.Parameters.AddWithValue("@BookDetail", addBook.BookDetail);
                    cmd.Parameters.AddWithValue("@DiscountPrice", addBook.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", addBook.ActualPrice);
                    cmd.Parameters.AddWithValue("@Quantity", addBook.Quantity);
                    cmd.Parameters.AddWithValue("@Rating", addBook.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", addBook.RatingCount);
                    cmd.Parameters.Add("@BookId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    int bookId = Convert.ToInt32(cmd.Parameters["@BookId"].Value.ToString());
                    con.Close();

                    if (result != 0)
                    {
                        BookModel bookModel = new BookModel
                        {
                            BookId = bookId,
                            BookName = addBook.BookName,
                            Author = addBook.Author,
                            BookImage = addBook.BookImage,
                            BookDetail = addBook.BookDetail,
                            DiscountPrice = addBook.DiscountPrice,
                            ActualPrice = addBook.ActualPrice,
                            Quantity = addBook.Quantity,
                            Rating = addBook.Rating,
                            RatingCount = addBook.RatingCount
                        };
                        return bookModel;
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
        public List<BookModel> GetAllBooks()
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    List<BookModel> bookModel = new List<BookModel>();
                    SqlCommand cmd = new SqlCommand("spGetAllBooks", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            BookModel book = new BookModel();
                            BookModel model;
                            model = ReadData(book, rdr);
                            bookModel.Add(model);
                        }
                        con.Close();
                        return bookModel;
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
        public BookModel GetBookById(int bookId)
        {

            try
            {
                this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
                using (con)
                {
                    BookModel bookModel = new BookModel();
                    SqlCommand cmd = new SqlCommand("spGetBookById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            bookModel = ReadData(bookModel, rdr);
                        }
                        con.Close();
                        return bookModel;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BookModel ReadData(BookModel bookModel, SqlDataReader rdr)
        {
            bookModel.BookId = Convert.ToInt32(rdr["BookId"]);
            bookModel.BookName = Convert.ToString(rdr["BookName"]);
            bookModel.Author = Convert.ToString(rdr["Author"]);
            bookModel.BookImage = Convert.ToString(rdr["BookImage"]);
            bookModel.BookDetail = Convert.ToString(rdr["BookDetail"]);
            bookModel.DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"]);
            bookModel.ActualPrice = Convert.ToDouble(rdr["ActualPrice"]);
            bookModel.Quantity = Convert.ToInt32(rdr["Quantity"]);
            bookModel.Rating = Convert.ToDouble(rdr["Rating"]);
            bookModel.RatingCount = Convert.ToInt32(rdr["RatingCount"]);

            return bookModel;
        }
    }
}
