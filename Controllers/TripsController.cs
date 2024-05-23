namespace Zadanie5APBD.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Models;

    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly TripContext _context;

        public TripsController(TripContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Trip>> GetTrips()
        {
            var trips = _context.Trips
                .OrderByDescending(t => t.DateFrom)
                .ToList();

            return Ok(trips);
        }
    }