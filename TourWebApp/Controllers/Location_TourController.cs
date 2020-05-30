using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourWebApp.Data;
using TourWebApp.Models;

namespace TourWebApp.Controllers
{
    [Route("TourLocation/[Action]")]
    public class Location_TourController : Controller
    {
        private readonly TourContext _context;

        public Location_TourController(TourContext context)
        {
            _context = context;
        }

        // GET: Location_Tour
        public async Task<IActionResult> Index(int? id)
        {
            var tourContext = _context.LocationSets.Where(e => e.TourID == id);
            return View(await tourContext.ToListAsync());
        }

        // GET: Location_Tour/Create
        public IActionResult Create()
        {
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name");
            ViewData["TourID"] = new SelectList(_context.Tours, "TourID", "Name");
            return View();
        }

        // POST: Location_Tour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Location_TourID,TourID,LocationID")] Location_Tour location_Tour)
        {
            if (ModelState.IsValid)
            {
                location_Tour.Location = _context.Locations.Find(location_Tour.LocationID);
                _context.Add(location_Tour);
                var tour = _context.Tours.Find(location_Tour.TourID);
                tour.Location_Tour.Add(location_Tour);
                tour.MinDuration += location_Tour.Location.MinTime;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), location_Tour.TourID);
            }
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name", location_Tour.LocationID);
            ViewData["TourID"] = new SelectList(_context.Tours, "TourID", "Name", location_Tour.TourID);
            return View(location_Tour);
        }

        // GET: Location_Tour/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location_Tour = await _context.LocationSets
                .Include(l => l.Location)
                .Include(l => l.Tour)
                .FirstOrDefaultAsync(m => m.Location_TourID == id);
            if (location_Tour == null)
            {
                return NotFound();
            }

            return View(location_Tour);
        }

        // POST: Location_Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Location_TourID)
        {
            var location_Tour = await _context.LocationSets.FindAsync(Location_TourID);
            var tour = _context.Tours.Find(location_Tour.TourID);
            tour.MinDuration -= location_Tour.Location.MinTime;
            _context.LocationSets.Remove(location_Tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Location_TourExists(int id)
        {
            return _context.LocationSets.Any(e => e.Location_TourID == id);
        }
    }
}
