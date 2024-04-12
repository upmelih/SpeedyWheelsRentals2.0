using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpeedyWheelsRentals.Models;
using SpeedyWheelsRentals2._0.Data;

namespace SpeedyWheelsRentals2._0.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
              return _context.Vehicle != null ? 
                          View(await _context.Vehicle.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Vehicle'  is null.");


        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewBag.Status = new SelectList(Enum.GetValues(typeof(VehicleStatus))
                                   .Cast<VehicleStatus>()
                                   .Select(e => new { Value = e.ToString(), Text = e.ToString() }),
                                   "Value", "Text");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("VehicleId,Make,Model,Year,RegistrationNumber,Status")] Vehicle vehicle)
         {
             if (ModelState.IsValid)
             {
                 vehicle.VehicleId = Guid.NewGuid();
                 _context.Add(vehicle);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(vehicle);
         }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,Make,Model,Year,RegistrationNumber,Status,DailyRentalPrice")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.VehicleId = Guid.NewGuid();
                _context.Add(vehicle);
                await _context.SaveChangesAsync();

               

                return RedirectToAction(nameof(Index));
            }

            // Repopulate the Status dropdown list if returning to the form
            ViewBag.Status = new SelectList(Enum.GetValues(typeof(VehicleStatus))
                                                .Cast<VehicleStatus>()
                                                .Select(e => new { Value = (int)e, Text = e.ToString() }),
                                                "Value", "Text", vehicle.Status); // Note the inclusion of vehicle.Status

            return View(vehicle);
        }


        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            ViewBag.Status = new SelectList(Enum.GetValues(typeof(VehicleStatus))
                                                .Cast<VehicleStatus>()
                                                .Select(e => new { Value = (int)e, Text = e.ToString() }),
                                                "Value", "Text", vehicle.Status); // Note the inclusion of vehicle.Status
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VehicleId,Make,Model,Year,RegistrationNumber,Status,DailyRentalPrice")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();

                   

                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(Guid id)
        {
          return (_context.Vehicle?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
