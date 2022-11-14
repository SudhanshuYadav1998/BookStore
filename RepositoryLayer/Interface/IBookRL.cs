using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(AddBook addBook);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookById(int bookId);
        public string DeleteBook(int bookId);
        public BookModel UpdateBook(BookModel updateBook);

    }
}
