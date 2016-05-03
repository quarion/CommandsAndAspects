using System;
using System.Collections.Generic;
using System.Linq;
using CommandsAndAspects.Domain;

namespace CommandsAndAspects.Infrastructure
{
    public class OrderRepositoryMock : IOrderRepository
    {
        public List<Order> Data { get; set; }


        public OrderRepositoryMock()
        {
            Data = new List<Order>();
        }

        public void Add(Order order)
        {
            Data.Add(order);
        }

        public Order Get(string id)
        {
            return Data.FirstOrDefault(o => o.Id == id);
        }

        public void Update(Order order)
        {
            var oldOrder = Data.RemoveAll(o => o.Id == order.Id);

            if(oldOrder < 1)
                throw new Exception("order not found");

            Data.Add(order);
        }
    }
}