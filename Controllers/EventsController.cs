using Microsoft.AspNetCore.Mvc;
using EventSignup.Models;

namespace EventSignup.Controllers
{
    public class EventsController : Controller
    {
        // Hardcoded list of events to simulate database persistence
        private static List<Event> _events = new List<Event>
        {
            new Event
            {
                Id = 1,
                Title = "Career Fair",
                Date = new DateTime(2026, 2, 1),
                Location = "Gym",
                Attendees = new List<Attendee>()
            },
            new Event
            {
                Id = 2,
                Title = "Tech Talk",
                Date = new DateTime(2026, 2, 8),
                Location = "Auditorium",
                Attendees = new List<Attendee>()
            },
            new Event
            {
                Id = 3,
                Title = "Hack Night",
                Date = new DateTime(2026, 2, 15),
                Location = "Library",
                Attendees = new List<Attendee>()
            }
        };

        // GET: Events/Index - Event Manager page displaying all events
        public IActionResult Index()
        {
            ViewData["Title"] = "Event Manager";
            return View(_events);
        }

        // GET: Events/ManageAttendees/1 - Manage Attendees page for a specific event
        public IActionResult ManageAttendees(int id)
        {
            var selectedEvent = _events.FirstOrDefault(e => e.Id == id);

            if (selectedEvent == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Manage Attendees - " + selectedEvent.Title;
            return View(selectedEvent);
        }

        // POST: Events/Signup - Register an attendee for a specific event
        [HttpPost]
        public IActionResult Signup(int eventId, string name, string email)
        {
            var selectedEvent = _events.FirstOrDefault(e => e.Id == eventId);

            if (selectedEvent != null && !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email))
            {
                selectedEvent.Attendees.Add(new Attendee
                {
                    Name = name,
                    Email = email
                });

                TempData["Success"] = "Attendee registered!";
            }

            return RedirectToAction("ManageAttendees", new { id = eventId });
        }
    }
}
