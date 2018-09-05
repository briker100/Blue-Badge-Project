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
    public class CommentController : Controller
    {
        [Authorize]
        public ActionResult Index(string sortOrder)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            var model = service.GetAllComments();
            //return View(model);

            if (User.IsInRole("Admin"))
                return View(model);

            return View("CreateOnlyCommentView", model);

        }

        [Authorize]
        public ActionResult Create()
        {
            //var model = new CommentCreate
            //{
            //    MovieId = id,
            //    UserId = Guid.Parse(User.Identity.GetUserId())
            //};
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(CommentCreate comment)
        {
            if (!ModelState.IsValid)
            {
                return View(comment);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            comment.UserName = User.Identity.GetUserName();
            if (service.CreateComment(comment))
            {
                TempData["SaveResult"] = "Your comment was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Comment could not be created");
            return View(comment);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCommentService();
            var model = svc.GetSingleCommentById(id);

            if (User.IsInRole("Admin"))
                return View(model);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCommentService();

            service.DeleteComment(id);

            TempData["SaveResult"] = "Your Comment was deleted";

            return RedirectToAction("Index");
        }
        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            return service;
        }

    }
}