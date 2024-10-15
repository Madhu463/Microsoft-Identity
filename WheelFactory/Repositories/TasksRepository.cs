
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
            if (task == null)
            {
                return null;
            }
            try
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }
            return task;
        }

        public Models.Task DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if(task == null)
            {
                return null;
            }
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
            if (currentTask == null || task == null)
            {
                return null;
            }
            try
            {
                _context.Entry(currentTask).CurrentValues.SetValues(task);
                _context.SaveChanges();
            }
            catch
            {
                return null;
            }
            return currentTask;
        }
    }
}
