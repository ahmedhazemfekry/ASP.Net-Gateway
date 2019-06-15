using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWay.Models;
using System.Net.Http;
using System;
using System.Text;
using Newtonsoft.Json;

namespace GateWay.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    public class GateWayController : ControllerBase
    {
        private readonly TodoContext _context;
        private static readonly HttpClient client = new HttpClient();

        public GateWayController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        // GET: api/gateway
        [HttpGet]
        public async Task<string> GetTodoItems()
        {
            var responseString = await client.GetStringAsync("http://localhost:5000/Predict?product=pepsi");
            return  responseString;

        }

        /// <summary>
        /// Gets Specific TodoItem.
        /// </summary>
        // GET: api/gateway/pepsi
        [HttpGet("{string}")]
        public async Task<string> GetTodoItem(string product)
        {
            var responseString = await client.GetStringAsync("http://localhost:5000/Predict?product="+product);

            return responseString;
        }

        /// <summary>
        /// Post TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /gateway
        ///     {
        ///        "products": ["Pepsi 1 Litre","Pringles","Nutella Jar"],
        ///        "classes": ["Soft Drinks", "Snacks", "Chocolates"]
        ///     }
        ///
        /// </remarks>
        // POST: api/gateway
        [HttpPost]
        public async Task<string> PostTodoItem(TrainingSet trainingSet)
        {
           
            var content = JsonConvert.SerializeObject(trainingSet);

            var response = await client.PostAsync("http://localhost:5000/Train", new StringContent(content, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        // PUT: api/gateway/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/gateway/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

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