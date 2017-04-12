using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeiBook.Data;
using ZeiBook.Models;
using Microsoft.AspNetCore.Authorization;

namespace ZeiBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RanksController : Controller
    {
        private ApplicationDbContext _context;

        public RanksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ranks
        public ActionResult Index()
        {
            var list = _context.BookRanks.ToList();
            return View(list);
        }

        // GET: Ranks/Details/5
        public ActionResult Details(int id)
        {
            var rankItem = _context.BookRanks.SingleOrDefault(t => t.Id == id);
            return View(rankItem);
        }

        // GET: Ranks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ranks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookRank rank)
        {
            try { 
            if (ModelState.IsValid)
            {
                _context.BookRanks.Add(rank);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            catch
            {

            }

            return View(rank);
        }

        // GET: Ranks/Edit/5
        public ActionResult Edit(int id)
        {
            var rankItem = _context.BookRanks.SingleOrDefault(t => t.Id == id);
            return View(rankItem);
        }

        // POST: Ranks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ranks/Delete/5
        public ActionResult Delete(int id)
        {
            var rankItem = _context.BookRanks.SingleOrDefault(t => t.Id == id);
            return View(rankItem);
        }

        // POST: Ranks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}