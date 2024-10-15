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
    internal class TasksRepositoryTest
    {
        private WheelContext _context;
        private TasksRepository _repo;
        [SetUp]
        public void SetUp()
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<WheelContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging();
            _context = new WheelContext(dbContextOptionBuilder.Options);
            _context.Tasks.Add(new Models.Task
            {
                Id = 1,
                OrderId = 101,
                Status = "neworder",
                SandBlastingLevel = "Medium",
                ImageUrl = "https://example.com/images/task1.jpg",
                Notes = "Initial inspection completed. Awaiting further processing.",
                IRating = 5,
                PColor = "Red",
                PType = "Glossy",
                CreatedAt = DateTime.UtcNow.AddHours(5).AddMinutes(30)
            });
            _context.Tasks.Add(new Models.Task
            {
                Id = 2,
                OrderId = 102,
                Status = "soldering",
                SandBlastingLevel = "High",
                ImageUrl = "https://example.com/images/task2.jpg",
                Notes = "Soldering in progress. Expected completion tomorrow.",
                IRating = 4,
                PColor = "Blue",
                PType = "Matte",
                CreatedAt = DateTime.UtcNow.AddHours(5).AddMinutes(30)
            });
            _context.Tasks.Add(new Models.Task
            {
                Id = 3,
                OrderId = 103,
                Status = "painting",
                SandBlastingLevel = "Low",
                ImageUrl = "https://example.com/images/task3.jpg",
                Notes = "Painting started. Color chosen: Silver.",
                IRating = null,  // Rating is not available yet
                PColor = "Silver",
                PType = "Metallic",
                CreatedAt = DateTime.UtcNow.AddHours(5).AddMinutes(30)
            });
            _context.SaveChanges();
            _repo = new TasksRepository(_context);
        }
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
        [Test]
        public void TestAddTask_ShouldAddTask()
        {
            var beforeCount = _context.Tasks.ToList().Count;
            _repo.AddTask(new Models.Task
            {
                Id = 4,
                OrderId = 103,
                Status = "painting",
                SandBlastingLevel = "Low",
                ImageUrl = "https://example.com/images/task3.jpg",
                Notes = "Painting started. Color chosen: Silver.",
                IRating = null,  // Rating is not available yet
                PColor = "Silver",
                PType = "Metallic",
                CreatedAt = DateTime.UtcNow.AddHours(5).AddMinutes(30)
            });
            var result = _context.Tasks.Find(4);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Models.Task>());
            Assert.That(result.OrderId, Is.EqualTo(103));
        }
        [Test]
        public void TestAddTask_ShouldNotAddDuplicateTask()
        {
            var beforeCount = _context.Tasks.ToList().Count;
            var result = _repo.AddTask(new Models.Task
            {
                Id = 1,
                OrderId = 103,
                Status = "painting",
                SandBlastingLevel = "Low",
                ImageUrl = "https://example.com/images/task3.jpg",
                Notes = "Painting started. Color chosen: Silver.",
                IRating = null,  // Rating is not available yet
                PColor = "Silver",
                PType = "Metallic",
                CreatedAt = DateTime.UtcNow.AddHours(5).AddMinutes(30)
            });
            Assert.That(result, Is.Null);
        }
        [Test]
        public void TestAddTask_ShouldNotAddNullTask()
        {
            var result = _repo.AddTask(null);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void GetTasks_ShouldReturnAllTasks()
        {
            var result = _repo.GetTasks();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IQueryable<Models.Task>>());
            Assert.That(result.Count, Is.EqualTo(_context.Tasks.ToList().Count));
        }
        [Test]
        public void UpdateTask_ShouldUpdateTask()
        {
            var result = _repo.UpdateTask(2, new Models.Task
            {
                Id = 2,
                OrderId = 103,
                Status = "painting",
                SandBlastingLevel = "Low",
                ImageUrl = "https://example.com/images/task3.jpg",
                Notes = "Painting started. Color chosen: Silver.",
                IRating = null,  // Rating is not available yet
                PColor = "Silver",
                PType = "Metallic",
                CreatedAt = DateTime.UtcNow.AddHours(5).AddMinutes(30)
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Models.Task>());
            Assert.That(result.OrderId, Is.EqualTo(103));
        }
        [Test]
        public void TestUpdateTask_ShouldNotUpdateEmptyTask()
        {
            var result = _repo.UpdateTask(100, new Models.Task
            {
                Id = 100,
                OrderId = 103,
                Status = "painting",
                SandBlastingLevel = "Low",
                ImageUrl = "https://example.com/images/task3.jpg",
                Notes = "Painting started. Color chosen: Silver.",
                IRating = null,  // Rating is not available yet
                PColor = "Silver",
                PType = "Metallic",
                CreatedAt = DateTime.UtcNow.AddHours(5).AddMinutes(30)
            });
            Assert.That(result, Is.Null);
        }
        [Test]
        public void UpdateTask_ShouldNotUpdateNullTask()
        {
            var result = _repo.UpdateTask(1, null);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void DeleteTask_ShouldDeleteTask()
        {
            _repo.DeleteTask(3);
            var result = _context.Tasks.Find(3);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void TestDeleteTask_ShouldNotDeleteEmptyTask()
        {
            var result = _repo.DeleteTask(100);
            Assert.That(result, Is.Null);
        }
    }
}
