using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrdersBL
    {
        public string AddOrder(AddOrder addOrder, int userId);
        public List<OrdersResponse> GetAllOrders(int userId);
        public string DeleteOrder(int OrderId,int userId);


    }
}
