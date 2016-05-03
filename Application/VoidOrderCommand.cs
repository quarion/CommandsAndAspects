using CommandsAndAspects.Domain;

namespace CommandsAndAspects.Application
{
    public class VoidOrderCommand
    {
        public string OrderId { get; set; }
    }

    public class VoidOrderCommandHandler : ICommandHandler<VoidOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public VoidOrderCommandHandler(IOrderRepository orderService)
        {
            _orderRepository = orderService;
        }

        public void Execute(VoidOrderCommand command)
        {
            var order = _orderRepository.Get(id: command.OrderId);

            order.Void();

            _orderRepository.Update(order);
        }
    }
}