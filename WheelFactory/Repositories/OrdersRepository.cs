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
        public IQueryable<Orders> GetOrders()
        {
            return _context.OrderDetails.AsQueryable<Orders>();
        }
        public Orders UpdateOrder(Orders order)
        {
            _context.OrderDetails.Update(order);
            _context.SaveChanges();
            _context.Entry<Orders>(order).State = EntityState.Detached;
            return order;
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
        public Orders DeleteOrder(int id)
        {
            var order = _context.OrderDetails.Find(id);
            _context.OrderDetails.Remove(order);
            _context.SaveChanges();
            return order;
        }
    }
}