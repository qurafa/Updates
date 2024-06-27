using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Updates.Data;
using Updates.Entities;

namespace Updates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UpdatesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Update>>> GetAllUpdates()
        {
            var updates = await _dataContext.Updates.ToListAsync();
            return Ok(updates);
        }

        //also have to specify the id in the route, could also use [Route("{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Update>> GetUpdate(int id)
        {
            var update = await _dataContext.Updates.FindAsync(id);

            if (update is null)
                return NotFound("Update not found, the update you're looking for might not exist");
            else
                return Ok(update);
        }

        [HttpPost]
        public async Task<ActionResult<List<Update>>> AddUpdate(Update update)
        {
            _dataContext.Updates.Add(update);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Updates.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Update>>> UpdateUpdate(Update update)
        {
            var dbUpdate = await _dataContext.Updates.FindAsync(update.Id);
            if (dbUpdate is null)
                return NotFound("Update not found");

            dbUpdate.Title = update.Title;
            dbUpdate.Description = update.Description;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Updates.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Update>>> DeleteUpdate(int id)
        {
            var update = await _dataContext.Updates.FindAsync(id);
            if (update is null)
                return NotFound("Update not found");

            _dataContext.Updates.Remove(update);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Updates.ToListAsync());
        }
    }
}
