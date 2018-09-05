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

    public class TheaterController : Controller
    {
        [Authorize]
        // GET: Theater
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TheaterService(userId);
            var model = service.GetTheater();
            //return View(model);

            if (User.IsInRole("Admin"))
                return View("Index",model);

            return View("ReadOnlyViewIndex", model);


        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(TheaterCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTheaterService();

            if (service.CreateTheater(model))
            {
                TempData["SaveResult"] = "Your Theater was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Theater could not be created.");

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateTheaterService();
            var detail = service.GetTheaterById(id);
            var model =
                new TheaterEdit
                {
                    TheaterId = detail.TheaterId,
                    TheaterName = detail.TheaterName,
                    TheaterLocation = detail.TheaterLocation,
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TheaterEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TheaterId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTheaterService();

            if (service.UpdateTheater(model))
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
            var svc = CreateTheaterService();
            var model = svc.GetTheaterById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTheaterService();

            service.DeleteTheater(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }


        private TheaterService CreateTheaterService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TheaterService(userId);
            return service;
        }

    }
}