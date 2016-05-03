using System;

namespace CommandsAndAspects.Application.Validation
{
    public class CreateOrderCommandValidator : IValidator<BookOrderCommand>
    {
        public void Validate(BookOrderCommand command)
        {
            if(command.DeliveryAddres == null)
                throw new ArgumentException("address cannot be empty");
        }
    }

    public class VoidOrderCommandValidator : IValidator<VoidOrderCommand>
    {
        public void Validate(VoidOrderCommand command)
        {
            if (command.OrderId == null)
                throw new ArgumentException("order id is required");
        }
    }
}