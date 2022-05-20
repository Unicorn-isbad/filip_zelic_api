using Ispit.API.Data;
using Ispit.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ispit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TodoController(AppDbContext context)
        { 
            _context = context;
        }
        [HttpGet]
        public ActionResult GetLists()
        {
            return Ok(_context.todoLists.ToList());
        }
        [HttpGet("{id}")]
        public ActionResult GetList(int id)
        {
            try
            {
                var list = _context.todoLists.Find(id);
                if (list == null)
                {
                    throw new ArgumentNullException();
                }
                return Ok(list);
            }
            catch (ArgumentNullException)
            {
                return NotFound("List not found");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
            }
        }
        [HttpPost]
        public ActionResult CreateList(TodoList new_list)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Check list properties");
                }
                _context.todoLists.Add(new_list);
                _context.SaveChanges();
                return Ok("List Created");
            }
            catch (BadHttpRequestException err)
            {
                return BadRequest(err.Message);
            }

            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult UpdateList(int id, TodoList update_list)
        {
            try
            {
                if (!ModelState.IsValid && id != update_list.Id)
                {
                    throw new BadHttpRequestException("Check list properties");
                }
                if (_context.todoLists.Find(id) == null)
                {
                    throw new ArgumentNullException();
                }
                _context.ChangeTracker.Clear();
                _context.todoLists.Update(update_list);
                _context.SaveChanges();
                return Ok($"List {id} updated");
            }
            catch (ArgumentNullException)
            {
                return NotFound("List not found");
            }
            catch (BadHttpRequestException err)
            {
                return BadRequest(err.Message);
            }

            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteList(int id)
        {
            try
            {
                var list = _context.todoLists.Find(id);
                if (list == null)
                {
                    throw new ArgumentNullException();
                }
                _context.todoLists.Remove(list);
                _context.SaveChanges();
                return Ok($"List {id} deleted");
            }
            catch (ArgumentNullException)
            {
                return NotFound("List not found");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
            }

        }
    }
}
