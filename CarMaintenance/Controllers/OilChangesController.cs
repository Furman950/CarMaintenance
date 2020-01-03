using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarMaintenance.Models;
using CarMaintenance.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilChangesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OilChangesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<OilChange>>> GetOilChange()
        {
            return await _context.OilChanges.ToListAsync();
        }

        [HttpGet("{vin}")]
        public async Task<ActionResult<IEnumerable<OilChange>>> GetOilChange(string vin)
        {
            var carExists = await _context.Cars.FindAsync(vin) ??
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, $"Car with Vin '{vin}' does not exists!");

            var oilChange = await _context.OilChanges.Where(oilChange => oilChange.Vin.Equals(vin)).ToListAsync();

            return oilChange;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOilChange(int id, OilChange oilChange)
        {
            if (id != oilChange.Id)
            {
                return BadRequest();
            }

            _context.Entry(oilChange).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OilChangeExists(id))
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
        public async Task<ActionResult<OilChange>> PostOilChange(OilChange oilChange)
        {
            _ = await _context.Cars.FindAsync(oilChange.Vin) ??
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, $"Car with Vin '{oilChange.Vin}' does not exists!");

            _context.OilChanges.Add(oilChange);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOilChange", new { id = oilChange.Id }, oilChange);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOilChange(int id)
        {
            var oilChange = await _context.OilChanges.FindAsync(id);
            if (oilChange == null)
            {
                return NotFound();
            }

            _context.OilChanges.Remove(oilChange);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OilChangeExists(int id)
        {
            return _context.OilChanges.Any(e => e.Id == id);
        }
    }
}
