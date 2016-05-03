namespace CommandsAndAspects.Application
{
    public interface ICommandHandler<in TCommand>
    {
        void Execute(TCommand request);
    }

    public interface ICommandHandler<in TCommand, out TResult>
    {
        TResult Execute(TCommand request);

        //In CQS canonical command do not return any value. But in practice some commands sometimes returns data.
        //For example "CreateOrder" command may return the ID of the created order
    }
}