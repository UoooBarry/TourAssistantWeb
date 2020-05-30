using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourWebApp.Attributes;
using TourWebApp.Data;
using TourWebApp.Models;

namespace TourWebApp.Controllers
{
    [AuthorizeAdmin]
    public class ToursController : Controller
    {
        private readonly TourContext _context;

        public ToursController(TourContext context)
        {
            _context = context;
        }

        // GET: Tours
        public async Task<IActionResult> Index()
        {
            var tourContext = _context.Tours.Include(t => t.Type);
            return View(await tourContext.ToListAsync());
        }

        // GET: Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(t => t.Type)
                .FirstOrDefaultAsync(m => m.TourID == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // GET: Tours/Create
        public IActionResult Create()
        {
            ViewData["TourTypeID"] = new SelectList(_context.TourTypes, "TourTypeID", "Label");
            return View();
        }

        public IActionResult Copy(int? id) 
        {
            ViewData["TourTypeID"] = new SelectList(_context.TourTypes, "TourTypeID", "Label");
            ViewBag.EToursID = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Copy([Bind("TourID,Name,TourTypeID,MinDuration")] Tour tour, int? ETour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tour);
                await _context.SaveChangesAsync(); //save tour here

                var e = _context.Tours.Find(ETour);
                var lcs = _context.LocationSets.Where(e => e.TourID == e.TourID).ToList(); //find the target location sets
                foreach (var lc in lcs) //for each location in list
                {
                    Console.WriteLine(lc.Location.Name);
                    var locationTour = new Location_Tour
                    {
                        LocationID = lc.Location.LocationID,
                        TourID = tour.TourID
                    };
                    _context.Add(locationTour);
                    tour.Location_Tour.Add(locationTour);
                }

                tour.MinDuration = e.MinDuration;
                _context.Update(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourTypeID"] = new SelectList(_context.TourTypes, "TourTypeID", "Label", tour.TourTypeID);
            return View(tour);
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourID,Name,TourTypeID,MinDuration")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourTypeID"] = new SelectList(_context.TourTypes, "TourTypeID", "Label", tour.TourTypeID);
            return View(tour);
        }

        // GET: Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            ViewData["TourTypeID"] = new SelectList(_context.TourTypes, "TourTypeID", "Label", tour.TourTypeID);
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TourID,Name,TourTypeID,MinDuration")] Tour tour)
        {
            if (id != tour.TourID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.TourID))
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
            ViewData["TourTypeID"] = new SelectList(_context.TourTypes, "TourTypeID", "Label", tour.TourTypeID);
            return View(tour);
        }

        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(t => t.Type)
                .FirstOrDefaultAsync(m => m.TourID == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return _context.Tours.Any(e => e.TourID == id);
        }
    }
}
