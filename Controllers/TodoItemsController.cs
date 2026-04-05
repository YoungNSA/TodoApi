using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoApi.Models;
using TodoApi.DTOs;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // Требуем аутентификацию для всех методов
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(TodoContext context, ILogger<TodoItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim!);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            var userId = GetCurrentUserId();
            _logger.LogInformation("Пользователь {UserId} запросил свои задачи", userId);

            var items = await _context.TodoItems
                .Where(x => x.UserId == userId)
                .Select(x => new TodoItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsCompleted = x.IsCompleted,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(int id)
        {
            var userId = GetCurrentUserId();
            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (todoItem == null)
            {
                return NotFound();
            }

            var dto = new TodoItemDto
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                IsCompleted = todoItem.IsCompleted,
                CreatedAt = todoItem.CreatedAt
            };

            return Ok(dto);
        }

        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetIncompleteItems()
        {
            var userId = GetCurrentUserId();
            var items = await _context.TodoItems
                .Where(x => x.UserId == userId && !x.IsCompleted)
                .Select(x => new TodoItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsCompleted = x.IsCompleted,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return Ok(items);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, UpdateTodoItemDto updateDto)
        {
            var userId = GetCurrentUserId();
            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Title = updateDto.Title;
            todoItem.Description = updateDto.Description;
            todoItem.IsCompleted = updateDto.IsCompleted;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> PostTodoItem(CreateTodoItemDto createDto)
        {
            var userId = GetCurrentUserId();

            var todoItem = new TodoItem
            {
                Title = createDto.Title,
                Description = createDto.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            var resultDto = new TodoItemDto
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                IsCompleted = todoItem.IsCompleted,
                CreatedAt = todoItem.CreatedAt
            };

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var userId = GetCurrentUserId();
            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
