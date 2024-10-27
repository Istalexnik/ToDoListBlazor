namespace ToDoListBlazor
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedDate { get; set; }

        public TaskItem(int id, string description)
        {
            Id = id;
            Description = description;
            IsComplete = false;
            CreatedDate = DateTime.Now;
        }
    }
}
