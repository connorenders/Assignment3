using Microsoft.AspNetCore.Mvc;
using SemesterYear_Assignment3_caenders.Data;
using SemesterYear_Assignment3_caenders.Models;
using System.Diagnostics;
using static SemesterYear_Assignment3_caenders.Data.Reviews;

namespace SemesterYear_Assignment3_caenders.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //  Generate statistics
            StatisticsModel stats = new StatisticsModel();
            stats.TotalMovies = (uint)_context.Movies.Count();
            stats.TotalActors = (uint)_context.Actors.Count();

            return View(stats);
        }

        public IActionResult Debug()
        {
            //  Generate debug statistics
            DebuggingModel debug = new DebuggingModel();
            debug.debug_variables.Add(("Total Movies", _context.Movies.Count().ToString()));
            debug.debug_variables.Add(("Total Actors", _context.Actors.Count().ToString()));
            debug.debug_variables.Add(("(GPT) Review", Configuration.GPT_Prompts.sys_review));
            debug.debug_variables.Add(("(GPT) Tweet", Configuration.GPT_Prompts.sys_tweet));
            debug.debug_variables.Add(("Review Intended Sentiments", string.Join(", ", Enum.GetNames(typeof(Intended_Sentiment)))));

            return View(debug);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
