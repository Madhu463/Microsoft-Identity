using Microsoft.EntityFrameworkCore;
using WheelFactory.Models;

namespace WheelFactory.Repositories
{
    public class OrdersRepository: IOrdersRepository
    {
        private readonly WheelContext _context;
        public OrdersRepository(WheelContext context)
        {
            _context = context;
        }
        public IQueryable<Orders>? GetOrders()
        {
            try
            {
                return _context.OrderDetails.AsQueryable<Orders>();
            }
            catch
            {
                return null;
            }
        }
        public Orders? UpdateOrder(Orders order)
        {
            _context.OrderDetails.Update(order);
            try
            {
                _context.SaveChanges();
                return order;
            }
            catch
            {
                return null;
            }
        }
        public Orders? AddOrder(Orders order)
        {
            _context.OrderDetails.Add(order);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return order;
        }
        public Orders DeleteOrder(Orders order)
        {
            try
            {
                var deletedOrder = _context.OrderDetails.Remove(order);
                _context.SaveChanges();
                return order;
            }
            catch
            {
                return null;
            }
        }
    }
}