using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using WheelFactory.Models;
using WheelFactory.Repositories;

namespace WheelFactory.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _repo;
        private readonly string _basePath = @"C:\Users\ksathvikreddy\Desktop\WheelFactory\Wheel-Factory\Backend\WheelFactory\wwwroot\images\";

        public OrdersService(IOrdersRepository repo)
        {
            _repo = repo;
        }
        public Orders? AddOrder(OrderDTO order)
        {
            if (order.ImageUrl == null || order.ImageUrl.Length == 0)
                return null;

            var originalFileName = Path.GetFileName(order.ImageUrl.FileName);
            var filePath = Path.Combine(_basePath, originalFileName);

            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                order.ImageUrl.CopyTo(stream);
            }
            Orders newOrder = new Orders
            {
                ClientName = order.ClientName,
                Year = (int)order.Year,
                Model = order.Model,
                Make = order.Make,
                DamageType = order.DamageType,
                ImageUrl = "http://localhost:5041/images/" + originalFileName,
                Notes = order.Notes,
                Status = order.Status
            };
            return _repo.AddOrder(newOrder);
        }

        public Orders? DeleteOrder(int id)
        {
            return _repo.DeleteOrder(id);
        }

        public IQueryable<Orders> GetOrders(int? id = null, string? status = null)
        {
            IQueryable<Orders> orders;
            if(id != null)
            {
                orders = _repo.GetOrders().Where<Orders>(o=>o.OrderId == id).AsQueryable<Orders>();
            }
            else
            {
                orders = _repo.GetOrders().AsQueryable<Orders>();
            }
            if(status != null)
            {
                orders = orders.Where<Orders>(o=>o.Status == status);
            }
            return orders;
        }

        public Orders? UpdateOrder(int id, OrderDTO order)
        {
            Orders? orderById =  GetOrders(id: id).FirstOrDefault<Orders>();

            if (orderById == null)
            {
                return null;
            }
            Orders newOrder = new Orders
            {
                OrderId = id,
                ClientName = order.ClientName,
                Year = (int)order.Year,
                Model = order.Model,
                DamageType = order.DamageType,
                ImageUrl = orderById.ImageUrl,
                Notes = order.Notes,
                Status = order.Status,
                CreatedAt = orderById.CreatedAt,
            };
            return _repo.UpdateOrder(newOrder);
        }

        public Orders? UpdateOrder(int id, string status)
        {
            Orders? orderById = GetOrders(id: id).FirstOrDefault<Orders>();
            if (orderById == null)
            {
                return null;
            }
            orderById.Status = status;
            return _repo.UpdateOrder(orderById);
        }
    }
}
