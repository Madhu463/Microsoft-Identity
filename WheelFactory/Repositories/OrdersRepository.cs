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
            if(order == null)
            {
                return null;
            }
            var existingOrder = _context.OrderDetails.Find(order.OrderId);

            if (existingOrder != null)
            {
                _context.Entry(existingOrder).CurrentValues.SetValues(order);
                try
                {
                    _context.SaveChanges();
                    return existingOrder;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        public Orders? AddOrder(Orders order)
        {
            if( order == null )
            {
                return null;
            }
            try
            {
                _context.OrderDetails.Add(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return order;
        }
        public Orders? DeleteOrder(int id)
        {
            var order = _context.OrderDetails.Find(id);
            if(order != null)
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
            return null;
            
        }
    }
}