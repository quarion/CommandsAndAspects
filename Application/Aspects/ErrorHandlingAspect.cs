using System;

namespace CommandsAndAspects.Application.Aspects
{
    public class ErrorHandlingAspect<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _commandHandler;

        public ErrorHandlingAspect(ICommandHandler<TCommand> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public void Execute(TCommand request)
        {
            try
            {
                _commandHandler.Execute(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}