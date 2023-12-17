using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SemesterYear_Assignment3_caenders.Analysis;
using SemesterYear_Assignment3_caenders.Data;
using SemesterYear_Assignment3_caenders.Models; // Replace with the correct namespace for your models
using System;
using System.Threading.Tasks;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: MoviesController
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies.ToListAsync();
        return View(movies);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.ID == id);

        //  Send over a full list of actors
        movie.AvailableActors = await _context.Actors.ToListAsync();

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        MovieModel movie;
        if (id == null || id == Guid.Empty)
        {
            // No ID passed, treat as a new movie
            movie = new MovieModel();
        }
        else
        {
            // Existing movie
            movie = await _context.Movies.FindAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
        }
        return View(movie);
    }

    // POST: Updating movie data
    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [Bind("ID,Title,Release_year,Genre,IMDB_url,Media_url")] MovieModel movie)
    {
        ModelState.Remove("Actors");                //  Fix relationship validation error
        ModelState.Remove("AvailableActors");       //  Fix relationship validation error
        if (ModelState.IsValid)
        {
            if (movie.ID == Guid.Empty)
            {
                movie.ID = Guid.NewGuid();
                _context.Add(movie);
            }
            else
            {
                _context.Update(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }


    // GET: MoviesController/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.ID == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MoviesController/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var movie = await _context.Movies.FindAsync(id);
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(Guid id)
    {
        return _context.Movies.Any(e => e.ID == id);
    }

    [HttpPost]
    public async Task<IActionResult> AddActorToMovie(Guid movieId, Guid actorId)
    {
        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.ID == movieId);

        if (movie == null)
        {
            return NotFound();
        }

        var actor = await _context.Actors.FindAsync(actorId);

        if (actor != null && !movie.Actors.Contains(actor))
        {
            movie.Actors.Add(actor);
            await _context.SaveChangesAsync();
        }

        // Redirect back to the Details action for the same movie
        return RedirectToAction("Details", new { id = movieId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveActorFromMovie(Guid movieId, Guid actorId)
    {
        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.ID == movieId);

        if (movie == null)
        {
            return NotFound();
        }

        var actor = movie.Actors.FirstOrDefault(a => a.ID == actorId);

        if (actor != null)
        {
            movie.Actors.Remove(actor);
            await _context.SaveChangesAsync();
        }

        // Redirect back to the Details action for the same movie
        return RedirectToAction("Details", new { id = movieId });
    }

    // GET: MoviesController
    public async Task<IActionResult> Reviews(Guid id)
    {
        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    [HttpGet]
    public async Task<IActionResult> GenerateReview(Guid id)
    {
        //  Get movie
        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (movie == null)
        {
            return NotFound();
        }

        // Placeholder logic - to be replaced with actual review generation logic
        var review = await SemesterYear_Assignment3_caenders.Data.Reviews.GPT_GenerateMovieReview(movie);

        if(review == null) return NotFound();
        var sentiment = SemesterYear_Assignment3_caenders.Analysis.Sentiment.AnalyzeSentiment(review).Compound;

        return Json(new { review, sentiment });
    }
}
