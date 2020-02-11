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
using Library.Infrastructure.Persistence;


namespace Library.MVC.Controllers
{
    public class NewBooksController : Controller
    {
        private readonly IBookDetailsService bookdetailsservice;
        private readonly IAuthorService authorService;

        public NewBooksController(IBookDetailsService bookdetailsservice, IAuthorService authorService)
        {
            this.bookdetailsservice = bookdetailsservice;
            this.authorService = authorService;
        }

        //GET: Books
        public async Task<IActionResult> Index()
        {
            var vm = new NewBookIndexVm();
            vm.BookDetails = bookdetailsservice.GetAllBookDetails();
            vm.Authors = authorService.GetAllAuthors();
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
            var vm = new NewBookCreateVm();
            vm.AuthorList = new SelectList(authorService.GetAllAuthors(), "Id", "Name");
            return View(vm);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewBookCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                //Create
                var newBook = new BookDetails();
                newBook.AuthorId = vm.AuthorId;
                newBook.Description = vm.Description;
                newBook.ISBN = vm.ISBN;
                newBook.Title = vm.Title;

                bookdetailsservice.AddNewBook(newBook);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var bookDetails = await _context.BookDetails
                    .Include(b => b.Author)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (bookDetails == null)
                {
                    return NotFound();
                }

                return View(bookDetails);
            }

        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bookDetails = bookdetailsservice.GetBookDetails(id);
            if (bookDetails == null)
            {
                return NotFound();
            }

            var vm = new BookEditVm();
            vm.Id = bookDetails.Id;
            vm.ISBN = bookDetails.ISBN;
            vm.Title = bookDetails.Title;
            vm.AuthorId = bookDetails.AuthorId;
            vm.Description = bookDetails.Description;
            vm.SelectAuthorList = new SelectList(authorService.GetAllAuthors(), nameof(Author.Id), nameof(Author.Name), bookDetails.AuthorId);
            return View(vm);

        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ISBN,Title,AuthorID,Description")] BookDetails bookDetails)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                if (id != bookDetails.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(bookDetails);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (id != bookDetails.Id)
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", bookDetails.AuthorId);
                return View(bookDetails);
            }
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var bookDetails = await _context.BookDetails
                    .Include(b => b.Author)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (bookDetails == null)
                {
                    return NotFound();
                }

                return View(bookDetails);
            }
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var bookDetails = await _context.BookDetails.FindAsync(id);
                _context.BookDetails.Remove(bookDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool BookDetailsExists(int id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {

                return _context.BookDetails.Any(e => e.Id == id);
            }

        }

    }
}
