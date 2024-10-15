using WheelFactory.Models;

namespace WheelFactory.Repositories
{
    public interface IOrdersRepository
    {
        IQueryable<Orders>? GetOrders();
        Orders? UpdateOrder(Orders order);
        Orders? AddOrder(Orders order);
        Orders? DeleteOrder(Orders order);
    }
}
