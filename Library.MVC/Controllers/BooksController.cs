using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Domain;
using Library.MVC.Models;
using Library.Application.Interfaces;

namespace Library.MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookservice;
        private readonly IAuthorService authorService;

        public BooksController(IBookService bookservice, IAuthorService authorService)
        {
            this.bookservice = bookservice;
            this.authorService = authorService;
        }

        //GET: Books
        public async Task<IActionResult> Index()
        {
            var vm = new BookIndexVm();
            vm.Books = bookservice.GetAllBooks();
            return View(vm);
        }

        //// GET: Books/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bookDetails = await _context.BookDetails
        //        .Include(b => b.Author)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (bookDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bookDetails);
        //}

        // GET: Books/Create
        public IActionResult Create()
        {
            var vm = new BookCreateVm();
            vm.AuthorList = new SelectList(authorService.GetAllAuthors(), "Id", "Name");
            return View(vm);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                //Skapa ny bok
                var newBook = new BookDetails();
                newBook.AuthorId = vm.AuthorId;
                newBook.Description = vm.Description;
                newBook.ISBN = vm.ISBN;
                newBook.Title = vm.Title;

                bookservice.AddBook(newBook);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        //// GET: Books/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bookDetails = await _context.BookDetails.FindAsync(id);
        //    if (bookDetails == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["AuthorID"] = new SelectList(_context.Authors, "Id", "Id", bookDetails.AuthorID);
        //    return View(bookDetails);
        //}

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,ISBN,Title,AuthorID,Description")] BookDetails bookDetails)
        //{
        //    if (id != bookDetails.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(bookDetails);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BookDetailsExists(bookDetails.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AuthorID"] = new SelectList(_context.Authors, "Id", "Id", bookDetails.AuthorID);
        //    return View(bookDetails);
        //}

        //// GET: Books/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bookDetails = await _context.BookDetails
        //        .Include(b => b.Author)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (bookDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bookDetails);
        //}

        //// POST: Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var bookDetails = await _context.BookDetails.FindAsync(id);
        //    _context.BookDetails.Remove(bookDetails);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BookDetailsExists(int id)
        //{
        //    return _context.BookDetails.Any(e => e.ID == id);
        //}
    }
}
