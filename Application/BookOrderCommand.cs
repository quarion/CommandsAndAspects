using System;
using System.Collections.Generic;
using CommandsAndAspects.Application.Validation;
using CommandsAndAspects.Domain;

namespace CommandsAndAspects.Application
{
    public class BookOrderCommand
    {
        public string OrderId { get; set; }
        public string DeliveryAddres { get; set; }
        public IEnumerable<string> OrderItemIds { get; set; }
    }

    public class BookOrderCommandHandler : ICommandHandler<BookOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public BookOrderCommandHandler(IOrderRepository orderService)
        {
            _orderRepository = orderService;
        }

        public void Execute(BookOrderCommand command)
        {
            var order = Order.CreateNew(
                id: command.OrderId,
                deliveryAddres: command.DeliveryAddres,
                orderItemIds: command.OrderItemIds);

            _orderRepository.Add(order);
        }
    }

}