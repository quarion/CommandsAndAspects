using CommandsAndAspects.Application.Validation;

namespace CommandsAndAspects.Application.Aspects
{
    public class ValidationAspect<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        private readonly IValidator<TCommand> _validator; 

        public ValidationAspect(ICommandHandler<TCommand> commandHandler, IValidator<TCommand> validator)
        {
            _commandHandler = commandHandler;
            _validator = validator;
        }

        public void Execute(TCommand request)
        {
            _validator.Validate(request);

            _commandHandler.Execute(request);
        }
    }
}