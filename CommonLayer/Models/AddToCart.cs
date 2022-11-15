using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class AddToCart
    {
        public int BookId { get; set; }

        public int BooksQty { get; set; }
    }
}
