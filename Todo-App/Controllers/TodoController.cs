// file to handle CRUD operations

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController: ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context){
            _context = context;
        }

        //get : api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems(){
            return await _context.TodoItems.ToListAsync();
        }

        //get: api/todo/:5 (to get todo by specific id)
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id){
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem == null){
                return NotFound();
            }
            return todoItem;
        }

        // post: /api/todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem){
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new {id = todoItem.Id}, todoItem);
        }

        //put: /api/todo/:3
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> PutTodoItem(int id, TodoItem todoItem){
            if(id != todoItem.Id){
                return BadRequest();
            }
            _context.Entry(todoItem).State = EntityState.Modified;
            await  _context.SaveChangesAsync();
            return NoContent();
        }

        // delete: api/todo/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(int id){
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem == null){
                return NotFound();
            }
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}