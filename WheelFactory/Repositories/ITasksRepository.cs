using WheelFactory.Models;

namespace WheelFactory.Repositories
{
    public interface ITasksRepository
    {
        IQueryable<Models.Task> GetTasks();
        Models.Task UpdateTask(int id, Models.Task task);
        Models.Task AddTask(Models.Task task);
        Models.Task DeleteTask(int id);
    }
}
