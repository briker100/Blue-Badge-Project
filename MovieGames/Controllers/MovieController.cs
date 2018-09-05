using Microsoft.AspNet.Identity;
using MovieGames.Models;
using MovieGames.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieGames.Controllers
{
    public class MovieController : Controller
    {

        [Authorize]
        public ActionResult Index(string sortOrder)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);
            var model = service.GetMovies();
            //return View(model);

            if (User.IsInRole("Admin"))
                return View("Index", model);

            return View("ReadOnlyMovieIndex", model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(MovieCreate model)
        {
            if (!ModelState.IsValid) return View(model);
           
            var service = CreateMovieService();
            if (service.CreateMovie(model))                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            {
                TempData["SaveResult"] = "Your Movie was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Movie could not be created.");

            return View(model);
        }
        public ActionResult Details(int id)
        { 
            var svc = CreateMovieService();
            var model = svc.GetMovieById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateMovieService();
            var detail = service.GetMovieById(id);
            var model =
                new MovieEdit
                {
                    MovieId = detail.MovieId,
                    MovieName = detail.Title,
                    Producer = detail.Producer,
                    Rating = detail.Rating,
                    Description = detail.Description,
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.MovieId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateMovieService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateMovieService();
            var model = svc.GetMovieById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateMovieService();

            service.DeleteMovie(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }
        private MovieService CreateMovieService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);
            return service;
        }
    }
} 