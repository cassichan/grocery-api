using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroceryApi.Models;

namespace GroceryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryItemsController : ControllerBase
    {
        private readonly GroceryContext _context;

        public GroceryItemsController(GroceryContext context)
        {
            _context = context;
        }

        // GET: api/GroceryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroceryItem>>> GetGroceryItems()
        {
          if (_context.GroceryItems == null)
          {
              return NotFound();
          }
            return await _context.GroceryItems.ToListAsync();
        }

        // GET: api/GroceryItems/
        [HttpGet("{id}")]
        public async Task<ActionResult<GroceryItem>> GetGroceryItem(long id)
        {
          if (_context.GroceryItems == null)
          {
              return NotFound();
          }
            var groceryItem = await _context.GroceryItems.FindAsync(id);

            if (groceryItem == null)
            {
                return NotFound();
            }

            return groceryItem;
        }

        // PUT: api/GroceryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroceryItem(long id, GroceryItem groceryItem)
        {
            if (id != groceryItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(groceryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GroceryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroceryItem>> PostGroceryItem(GroceryItem groceryItem)
        {
          if (_context.GroceryItems == null)
          {
              return Problem("Entity set 'GroceryContext.GroceryItems'  is null.");
          }
            _context.GroceryItems.Add(groceryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof (GetGroceryItem), new { id = groceryItem.Id }, groceryItem);
        }

        // DELETE: api/GroceryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroceryItem(long id)
        {
            if (_context.GroceryItems == null)
            {
                return NotFound();
            }
            var groceryItem = await _context.GroceryItems.FindAsync(id);
            if (groceryItem == null)
            {
                return NotFound();
            }

            _context.GroceryItems.Remove(groceryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroceryItemExists(long id)
        {
            return (_context.GroceryItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
