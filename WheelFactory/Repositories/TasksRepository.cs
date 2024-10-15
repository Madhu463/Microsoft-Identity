
using WheelFactory.Models;

namespace WheelFactory.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly WheelContext _context;
        public TasksRepository(WheelContext context)
        {
            _context = context;
        }
        public Models.Task AddTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public Models.Task DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return task;
        }

        public IQueryable<Models.Task> GetTasks()
        {
            return _context.Tasks.AsQueryable<Models.Task>();
        }

        public Models.Task UpdateTask(int id, Models.Task task)
        {
            var currentTask = _context.Tasks.Find(id);
            if (currentTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found");
            }
            currentTask = task;
            _context.SaveChanges();
            return currentTask;
        }
    }
}
