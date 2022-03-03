using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WaterPlant.Interfaces;
using WaterPlant.Model;
using WaterPlant.Model.DBContext;

namespace WaterPlant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly IHubContext<BroadcastHub> _hubContext;
        private readonly PlantContext _context;
        private readonly IRepository _repo;
        BroadcastHub hb;
        public PlantsController(PlantContext context, IRepository repo, IHubContext<BroadcastHub> hubContext)
        {
            hb = new BroadcastHub();
            _context = context;
            _hubContext = hubContext;
            _repo = repo;
        }
        [HttpGet]
        [Route("getdbdata")]
        public   ActionResult<List<data>> fetchDBdata()
        {
            return   _repo.GetAllMessages();
        }
        // GET: api/Plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants()
        {
            var list = await _context.Plants.ToListAsync();
            foreach (var item in list)
            {
                var diffInSeconds = (DateTime.Now - item.lastWateredAt).TotalSeconds;
                if (diffInSeconds < 30)
                    item.isWaterAllowed = true;
            }
            return list;
        }

        // GET: api/Plants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(int id)
        {
            var plant = await _context.Plants.FindAsync(id);

            if (plant == null)
            {
                return NotFound();
            }

            return plant;
        }

        // PUT: api/Plants/5
         [HttpPut("{id}")]
        public async Task<IActionResult> PutPlant(int id, Plant plant)
        {
            if (id != plant.PlantId)
            {
                return BadRequest();
            }
             Request.Headers.TryGetValue("connectionid", out var traceValue);
            string connectionId = traceValue.ToString();
            //if (plant.status == "s")//it will be used if we are updating stop status in the DB and update others.
            //    plant.status = "Y";
            // else { 
            plant.lastWateredAt = DateTime.Now;
            plant.status = "N";
            _context.Entry(plant).State = EntityState.Modified;

            try
            {
                if (!string.IsNullOrEmpty(traceValue))
                {
                    await _hubContext.Clients.AllExcept(connectionId).SendAsync("BroadcastMessage");
                }
               
                await _context.SaveChangesAsync();
              // await _hubContext.Clients.All.SendAsync("BroadcastMessage");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
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

        // POST: api/Plants
        [HttpPost]
        public async Task<ActionResult<Plant>> PostPlant(Plant plant)
        {
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlant", new { id = plant.PlantId }, plant);
        }

        // DELETE: api/Plants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PlantExists(int id)
        {
            return _context.Plants.Any(e => e.PlantId == id);
        }
    }
}
