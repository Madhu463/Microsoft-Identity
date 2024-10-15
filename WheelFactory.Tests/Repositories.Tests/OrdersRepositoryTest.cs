using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelFactory.Models;
using WheelFactory.Repositories;

namespace WheelFactory.Tests.Repositories.Tests
{
    internal class OrdersRepositoryTest
    {
        private WheelContext _context;
        private OrdersRepository _repo;
        [SetUp]
        public void SetUp()
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<WheelContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging();
            _context = new WheelContext(dbContextOptionBuilder.Options);
            _context.OrderDetails.Add(new Orders
            {
                OrderId = 1,
                ClientName = "John Doe",
                Year = 2021,
                Make = "Toyota",
                Model = "Corolla",
                DamageType = "Scratch",
                ImageUrl = "https://example.com/images/toyota-corolla.jpg",
                Notes = "Minor scratch on the front bumper.",
                Status = "Pending",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            });
            _context.OrderDetails.Add(new Orders
            {
                OrderId = 2,
                ClientName = "Jane Smith",
                Year = 2019,
                Make = "Honda",
                Model = "Civic",
                DamageType = "Dent",
                ImageUrl = "https://example.com/images/honda-civic.jpg",
                Notes = "Dent on rear door.",
                Status = "In Progress",
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            });
            _context.OrderDetails.Add(new Orders
            {
                OrderId = 3,
                ClientName = "Michael Brown",
                Year = 2020,
                Make = "Ford",
                Model = "F-150",
                DamageType = "Broken Mirror",
                ImageUrl = "https://example.com/images/ford-f150.jpg",
                Notes = "Right side mirror is broken.",
                Status = "Completed",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            });
            _context.SaveChanges();
            _repo = new OrdersRepository(_context);
        }
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
        [Test]
        public void TestAddOrder_ShouldAddOrder()
        {
            var beforeCount = _context.OrderDetails.ToList().Count;
            _repo.AddOrder(new Orders
            {
                OrderId = 4,
                ClientName = "Sathvik Reddy",
                Year = 2020,
                Make = "Ford",
                Model = "F-150",
                DamageType = "Broken Mirror",
                ImageUrl = "https://example.com/images/ford-f150.jpg",
                Notes = "Right side mirror is broken.",
                Status = "Completed",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            });
            var result = _context.OrderDetails.Find(4);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Orders>());
            Assert.That(result?.ClientName, Is.EqualTo("Sathvik Reddy"));
        }
        [Test]
        public void TestAddOrder_ShouldNotAddDuplicateOrder()
        {
            var beforeCount = _context.OrderDetails.ToList().Count;
            var result  = _repo.AddOrder(new Orders
            {
                OrderId = 1,
                ClientName = "Sathvik Reddy",
                Year = 2020,
                Make = "Ford",
                Model = "F-150",
                DamageType = "Broken Mirror",
                ImageUrl = "https://example.com/images/ford-f150.jpg",
                Notes = "Right side mirror is broken.",
                Status = "Completed",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            });
            Assert.That(result, Is.Null);
        }
        [Test]
        public void TestAddOrder_ShouldNotAddNullOrder()
        {
            var result = _repo.AddOrder(null);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void GetOrders_ShouldReturnAllOrders()
        {
            var result = _repo.GetOrders();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IQueryable<Orders>>());
            Assert.That(result.Count, Is.EqualTo(_context.OrderDetails.ToList().Count));
        }
        [Test]
        public void UpdateOrder_ShouldUpdateOrder()
        {
            var result = _repo.UpdateOrder(new Orders
            {
                OrderId = 2,
                ClientName = "Jane Smith",
                Year = 2019,
                Make = "Honda",
                Model = "Civic",
                DamageType = "Dent",
                ImageUrl = "https://example.com/images/honda-civic.jpg",
                Notes = "Dent on rear door.",
                Status = "In Progress",
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Orders>());
            Assert.That(result?.ClientName, Is.EqualTo("Jane Smith"));
        }
        [Test]
        public void TestUpdateOrder_ShouldNotUpdateEmptyOrder()
        {
            var result = _repo.UpdateOrder(new Orders
            {
                OrderId = 100,
                ClientName = "Pankaj Kumar",
                Year = 2020,
                Make = "Ford",
                Model = "F-150",
                DamageType = "Broken Mirror",
                ImageUrl = "https://example.com/images/ford-f150.jpg",
                Notes = "Right side mirror is broken.",
                Status = "Completed",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            });
            Assert.That(result, Is.Null);
        }
        [Test]
        public void UpdateOrder_ShouldNotUpdateNullOrder()
        {
            var result = _repo.UpdateOrder(null);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void DeleteOrder_ShouldDeleteOrder()
        {
            _repo.DeleteOrder(1);
            var result = _context.OrderDetails.Find(1);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void TestDeleteOrder_ShouldNotDeleteEmptyOrder()
        {
            var result = _repo.DeleteOrder(100);
            Assert.That(result, Is.Null);
        }

    }
}
