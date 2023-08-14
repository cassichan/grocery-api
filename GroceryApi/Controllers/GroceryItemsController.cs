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
        public async Task<ActionResult<IEnumerable<GroceryItemDTO>>> GetGroceryItems()
        {
            return await _context.GroceryItems.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/GroceryItems/
        [HttpGet("{id}")]
        public async Task<ActionResult<GroceryItemDTO>> GetGroceryItem(long id)
        {
            var groceryItem = await _context.GroceryItems.FindAsync(id);

            if (groceryItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(groceryItem);
        }

        // PUT: api/GroceryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroceryItem(long id, GroceryItemDTO groceryItemDTO)
        {
            if (id != groceryItemDTO.Id)
            {
                return BadRequest();
            }

            //_context.Entry(groceryItem).State = EntityState.Modified;

            var groceryItem = await _context.GroceryItems.FindAsync(id);
            if (groceryItem == null)
            {
                return NotFound();
            }

            groceryItem.Name = groceryItemDTO.Name;
            groceryItem.Quantity = groceryItemDTO.Quantity;
            groceryItem.InCart = groceryItemDTO.InCart;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException) when (!GroceryItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/GroceryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroceryItemDTO>> PostGroceryItem(GroceryItemDTO groceryItemDTO)
        {

            var groceryItem = new GroceryItem
            {
                InCart = groceryItemDTO.InCart,
                Name = groceryItemDTO.Name,
                Quantity = groceryItemDTO.Quantity
            };

            _context.GroceryItems.Add(groceryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroceryItem), new { id = groceryItem.Id }, ItemToDTO(groceryItem));
        }

        // DELETE: api/GroceryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroceryItem(long id)
        {
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
            return _context.GroceryItems.Any(e => e.Id == id);
        }

        private static GroceryItemDTO ItemToDTO(GroceryItem groceryItem) =>
           new GroceryItemDTO
           {
               Id = groceryItem.Id,
               Name = groceryItem.Name,
               Quantity = groceryItem.Quantity,
               InCart = groceryItem.InCart
           };
    }
}
