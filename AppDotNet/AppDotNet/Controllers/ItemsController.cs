using AppDotNet.Data;
using AppDotNet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly DataContext _context;

        public ItemsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Items>>> GetAllItems()
        {
            var items = await _context.Item.ToListAsync();

            return Ok(items);
        }

        [HttpGet("{Name}")]
        public async Task<ActionResult<Items>> GetItem(string Name)
        {
            // Assuming you want to find the item by its name, not by the primary key
            var item = await _context.Item.FirstOrDefaultAsync(i => i.Name == Name);

            if (item is null)
            {
                return NotFound("Item not found.");
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<List<Items>>> AddItem(Items item)
        {
            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            return Ok(await _context.Item.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Items>>> UpdateItem(Items item)
        {

            var dbItem = await _context.Item.FindAsync(item.Id);

            if (dbItem is null)
            {
                return NotFound("Item not found.");
            }

            dbItem.Name = item.Name;
            dbItem.Price = item.Price;
            dbItem.Count = item.Count;

            await _context.SaveChangesAsync();

            return Ok(await _context.Item.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Items>>> DeleteItem(string name)
        {

            var dbUpdated = await _context.Item.FirstOrDefaultAsync(i => i.Name == name);

            if (dbUpdated is null)
            {
                return NotFound("Item not found.");
            }

            _context.Item.Remove(dbUpdated);
            await _context.SaveChangesAsync();

            return Ok(await _context.Item.ToListAsync());
        }

    }
}
