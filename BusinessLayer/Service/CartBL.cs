using BusinessLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CartBL:ICartBL
    {
        private readonly CartRL cartRL;
        public CartBL(CartRL cartRL)
        {
            this.cartRL = cartRL;
        }

    }
}
