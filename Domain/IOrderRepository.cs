namespace CommandsAndAspects.Domain
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Order Get(string id);
        void Update(Order order);
    }
}