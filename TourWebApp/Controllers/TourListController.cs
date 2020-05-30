using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourWebApp.Data;
using TourWebApp.Models;

namespace TourWebApp.Controllers
{
    public class TourListController : Controller
    {
        private readonly TourContext _context;
        public TourListController(TourContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult PrintAll() 
        {
            List<Tour> tours = _context.Tours.ToList();
            return PartialView("_Tours", tours);
        }

        [HttpGet]
        public IActionResult Filter(int? id) 
        {
            List<Tour> tours = _context.Tours.Where(e => e.TourTypeID == id).ToList();

            return PartialView("_Tours", tours);
        }
    }
}