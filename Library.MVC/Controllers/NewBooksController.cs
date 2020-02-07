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
            var vm = new BookIndexVm();
            vm.Books = bookdetailsservice.GetAllBookDetails();
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

            return RedirectToAction("Error","Home","");
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

    }
}
