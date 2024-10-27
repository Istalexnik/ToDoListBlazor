using System.Text.Json;

namespace ToDoListBlazor;

public class ToDoListService
{
    private readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ToDoListBlazor", "tasks.json");
    private List<TaskItem> tasks;

    public ToDoListService()
    {
        EnsureDataDirectoryExists(); // Ensure the directory exists
        tasks = LoadTasks();
    }

    public IEnumerable<TaskItem> GetTasks() => tasks;

    public void AddTask(string description)
    {
        int newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
        tasks.Add(new TaskItem(newId, description));
        SaveTasks();
    }

    public void MarkTaskAsComplete(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.IsComplete = true;
            SaveTasks();
        }
    }

    public void MarkTaskAsIncomplete(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.IsComplete = false;
            SaveTasks();
        }
    }

    public void DeleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            tasks.Remove(task);
            SaveTasks();
        }
    }

    private void SaveTasks()
    {
        try
        {
            var json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving tasks: {ex.Message}");
        }
    }

    private List<TaskItem> LoadTasks()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return new List<TaskItem>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading tasks: {ex.Message}");
            return new List<TaskItem>();
        }
    }

    private void EnsureDataDirectoryExists()
    {
        var directory = Path.GetDirectoryName(filePath);

        if (directory is not null && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

}
