namespace CommandsAndAspects.Application.Validation
{
    public interface IValidator<in T>
    {
        void Validate(T request);
    }
}