using GigHub.Models;
using GigHub.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
            public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Gigs
        public ActionResult Create()
        {
            var GenresList = _context.Genres.ToList();
            GigFormViewModel model = new GigFormViewModel();
            model.Genres = GenresList;
            return View(model);
        }
    }
}