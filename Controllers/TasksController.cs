using Microsoft.AspNetCore.Mvc;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static readonly List<TaskItem> Tasks = new()
{
new TaskItem { Id = 1, Title = "Learn ASP.NET Core", IsCompleted = false }
};


   [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(Tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask(TaskItem task)
        {
            task.Id = Tasks.Count == 0 ? 1 : Tasks.Max(t => t.Id) + 1;
            Tasks.Add(task);

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound();

            task.Title = updatedTask.Title;
            task.IsCompleted = updatedTask.IsCompleted;

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound();

            Tasks.Remove(task);
            return NoContent();
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }


}
