namespace TodoApi.DTOs
{
    // Для возврата данных клиенту
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // Для создания новой записи
    public class CreateTodoItemDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    // Для обновления записи
    public class UpdateTodoItemDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
