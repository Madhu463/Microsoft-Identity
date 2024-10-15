using WheelFactory.Models;

namespace WheelFactory.Services
{
    public interface IOrdersService
    {
        IQueryable<Orders> GetOrders(int? id = null, string? status = null);
        Orders? UpdateOrder(int id, OrderDTO order);
        Orders? DeleteOrder(int id);
        Orders? AddOrder(OrderDTO order);
        Orders? UpdateOrder(int id, string status);
    }
}
