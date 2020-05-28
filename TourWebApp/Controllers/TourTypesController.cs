using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourWebApp.Attributes;
using TourWebApp.Data;
using TourWebApp.Models;

namespace TourWebApp.Controllers
{
    [AuthorizeAdmin]
    public class TourTypesController : Controller
    {
        private readonly TourContext _context;

        public TourTypesController(TourContext context)
        {
            _context = context;
        }

        // GET: TourTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TourTypes.ToListAsync());
        }

        // GET: TourTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourType = await _context.TourTypes
                .FirstOrDefaultAsync(m => m.TourTypeID == id);
            if (tourType == null)
            {
                return NotFound();
            }

            return View(tourType);
        }

        // GET: TourTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TourTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourTypeID,Label")] TourType tourType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourType);
        }

        // GET: TourTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourType = await _context.TourTypes.FindAsync(id);
            if (tourType == null)
            {
                return NotFound();
            }
            return View(tourType);
        }

        // POST: TourTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TourTypeID,Label")] TourType tourType)
        {
            if (id != tourType.TourTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourTypeExists(tourType.TourTypeID))
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
            return View(tourType);
        }

        // GET: TourTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourType = await _context.TourTypes
                .FirstOrDefaultAsync(m => m.TourTypeID == id);
            if (tourType == null)
            {
                return NotFound();
            }

            return View(tourType);
        }

        // POST: TourTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourType = await _context.TourTypes.FindAsync(id);
            _context.TourTypes.Remove(tourType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourTypeExists(int id)
        {
            return _context.TourTypes.Any(e => e.TourTypeID == id);
        }
    }
}
