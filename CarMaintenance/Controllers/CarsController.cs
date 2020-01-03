using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarMaintenance.Models;
using Microsoft.AspNetCore.Authorization;
using CarMaintenance.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CarMaintenance.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [Route("all")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            return await _context.Cars.ToListAsync();
        }

        [HttpGet("{vin}")]
        public async Task<ActionResult<Car>> GetCar(string vin)
        {
            var car = await _context.Cars.FindAsync(vin);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPut("{vin}")]
        public async Task<IActionResult> PutCar(string vin, Car car)
        {
            if (vin != car.Vin)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(vin))
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

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarExists(car.Vin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCar", new { id = car.Vin }, car);
        }

        [HttpDelete("{vin}")]
        public async Task<ActionResult> DeleteCar(string vin)
        {
            var car = await _context.Cars
                .Include(c => c.OilChanges)
                .FirstOrDefaultAsync(c => c.Vin.Equals(vin)) ??
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, $"Car with Vin '{vin}' does not exists!");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(string id)
        {
            return _context.Cars.Any(e => e.Vin == id);
        }
    }
}
