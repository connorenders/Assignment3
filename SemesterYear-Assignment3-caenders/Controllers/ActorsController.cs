using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SemesterYear_Assignment3_caenders.Data;
using SemesterYear_Assignment3_caenders.Models;

namespace SemesterYear_Assignment3_caenders.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            var actors = await _context.Actors.ToListAsync();
            return View(actors);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var actor = await _context.Actors
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            ActorModel actor;
            if (id == null || id == Guid.Empty)
            {
                // No ID passed, treat as a new actor
                actor = new ActorModel();
            }
            else
            {
                // Existing actor
                actor = await _context.Actors.FindAsync(id.Value);
                if (actor == null)
                {
                    return NotFound();
                }
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Birthdate,Gender,IMDB_URL,Media_URL")] ActorModel actor)
        {
            ModelState.Remove("Movies");

            if (ModelState.IsValid)
            {
                if (actor.ID == Guid.Empty)
                {
                    actor.ID = Guid.NewGuid();
                    _context.Add(actor);
                }
                else
                {
                    _context.Update(actor);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var actor = await _context.Actors
                .FirstOrDefaultAsync(a => a.ID == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var actor = await _context.Actors.FindAsync(id);
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(Guid id)
        {
            return _context.Actors.Any(e => e.ID == id);
        }

        // GET: MoviesController
        public async Task<IActionResult> Tweets(Guid id)
        {
            //  Get actor
            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.ID == id);

            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        [HttpGet]
        public async Task<IActionResult> GenerateTweet(Guid id)
        {
            //  Get actor
            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.ID == id);

            if (actor == null)
            {
                return NotFound();
            }

            // Placeholder logic - to be replaced with actual review generation logic
            var tweet = await SemesterYear_Assignment3_caenders.Data.Tweets.GPT_GenerateTweet(actor);

            if (tweet == null) return NotFound();
            var sentiment = SemesterYear_Assignment3_caenders.Analysis.Sentiment.AnalyzeSentiment(tweet).Compound;

            return Json(new { tweet, sentiment });
        }
    }
}