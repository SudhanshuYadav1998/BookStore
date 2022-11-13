﻿using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(AddBook addBook);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookById(int bookId);



    }
}
