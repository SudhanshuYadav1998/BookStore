using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class OrdersBL:IOrdersBL
    {
        private readonly IOrdersRL ordersRL;
        public OrdersBL(IOrdersRL ordersRL)
        {
            this.ordersRL = ordersRL;
        }
        public string AddOrder(AddOrder addOrder, int userId)
        {
            return this.ordersRL.AddOrder(addOrder, userId);
        }
        public List<OrdersResponse> GetAllOrders(int userId)
        {
            return this.ordersRL.GetAllOrders(userId);
        }
        public string DeleteOrder(int OrderId, int userId)
        {
            return ordersRL.DeleteOrder(OrderId,userId);
        }


    }
}
