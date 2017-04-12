using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZeiBook.Data;
using ZeiBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ZeiBook.Areas.Admin.Actions.Books;
using Microsoft.AspNetCore.Routing;
using ZeiBook.Models.PageViewModels;

namespace ZeiBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Books
        [HttpGet("Admin/Books",Order =1)]
        [HttpGet("Admin/Books/p{pageNum}", Order =0)]
        public async Task<IActionResult> Index(int? pageNum, string bookName, [FromServices]IndexAction action)
        {
            
            var model = await action.GetViewModelAsync(pageNum??1, bookName);
            return View(model);
        }

        // GET: Admin/Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Admin/Books/Create
        public IActionResult Create()
        {
            ViewData["cateList"] = _context.Categories.ToList();
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,CategoryId,Description,Gender,UploadTime")] BookItemViewModel book,
            IFormFile bookFile,
            [FromServices]CreateAction createAction)
        {
            if (ModelState.IsValid)
            {
                //authorId
                var authorItem = GetAuthor(book.Author);
                book.AuthorId = authorItem.Id;

                var path = createAction.SaveFile(bookFile);
                book.Description = createAction.GetHtmlDescription(book.Description);
                book.UploadTime = DateTime.Now;
                if (bookFile != null)
                {
                    book.FileLength = bookFile.Length;
                    book.Path = path;
                }
                var coverPath = createAction.GetCoverPath(book.Name);
                book.CoverPath = coverPath;
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Admin/Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["cateList"] = _context.Categories.ToList();

            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,CategoryId,Gender,Description")] BookItemViewModel book,
            IFormFile bookFile,
            [FromServices]CreateAction createAction)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = _context.Books.SingleOrDefault(b => b.Id == id);
                    item.Name = book.Name;
                    item.CategoryId = book.CategoryId;
                    item.Gender = book.Gender;
                    var authorItem = GetAuthor(book.Author);
                    item.AuthorId = authorItem.Id;
                    item.Description = book.Description;

                    var path = createAction.SaveFile(bookFile);
                    if (path != null)
                    {
                        item.Path = path;
                        item.FileLength = bookFile.Length;
                    }

                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = id });
        }

       
        // GET: Admin/Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private Author GetAuthor(string author)
        {
            var authorItem = _context.Authors.SingleOrDefault(t => t.Name == author);
            if (authorItem == null)
            {
                authorItem = new Author { Name = author };
                _context.Add(authorItem);
                _context.SaveChanges();
            }
            return authorItem;
        }

    }
}
