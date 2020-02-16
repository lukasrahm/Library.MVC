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
using Library.Infrastructure.Services;

namespace Library.MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        private readonly IBookDetailsService bookDetailsService;
        private readonly IAuthorService authorService;

        public BooksController(IBookService bookService, IBookDetailsService bookDetailsService, IAuthorService authorService)
        {
            this.bookService = bookService;
            this.bookDetailsService = bookDetailsService;
            this.authorService = authorService;
        }

        //GET: Books
        public async Task<IActionResult> Index()
        {
            var vm = new BookIndexVm();
            vm.Details = bookDetailsService.GetAllBookDetails();
            vm.Authors = authorService.GetAllAuthors();
            vm.Books = bookService.GetAllBooks();
            return View(vm);
        }

        //GET: Books
        public async Task<IActionResult> Admin()
        {
            var vm = new BookIndexVm();
            vm.Details = bookDetailsService.GetAllBookDetails();
            vm.Authors = authorService.GetAllAuthors();
            vm.Books = bookService.GetAllBooks();
            return View(vm);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var vm = new BookCreateVm();
            vm.Authors = new SelectList(authorService.GetAllAuthors(), "Id", "Name");
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
                //Create
                var newBook = new BookDetails();
                newBook.AuthorId = vm.AuthorId;
                newBook.Description = vm.Description;
                newBook.ISBN = vm.ISBN;
                newBook.Title = vm.Title;

                bookDetailsService.AddNewBook(newBook);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        public IActionResult Add()
        {
            var vm = new BookAddVm();
            vm.DetailsList = new SelectList(bookDetailsService.GetAllBookDetails(), "Id", "Title");
            return View(vm);
        }

        // POST: Books/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BookAddVm vm)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < vm.AmountOfCopies; i++)
                {
                    //Skapa ny bok
                    var addBook = new Book();

                    addBook.DetailsId = vm.DetailId;
                    bookService.AddBook(addBook);

                }

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bookDetails = bookDetailsService.GetBookDetails(id);
            if (bookDetails == null)
            {
                return NotFound();
            }

            var vm = new BookDetails();
            vm.Id = bookDetails.Id;
            vm.ISBN = bookDetails.ISBN;
            vm.Title = bookDetails.Title;
            vm.AuthorId = bookDetails.AuthorId;
            vm.Description = bookDetails.Description;
            vm.Author = authorService.GetAuthorById(bookDetails.AuthorId);
            vm.Copies = bookDetailsService.GetCopiesById(bookDetails.Id);
            return View(vm);


        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bookDetails = bookDetailsService.GetBookDetails(id);
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
            vm.Authors = new SelectList(authorService.GetAllAuthors(), nameof(Author.Id), nameof(Author.Name), bookDetails.AuthorId);
            return View(vm);

        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ISBN,Title,AuthorId,Description")] BookDetails bookDetails)
        {

            if (id != bookDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookDetailsService.UpdateBookDetails(bookDetails);
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

            return View(bookDetails);

        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bookDetails = bookDetailsService.GetBookDetails(id);
            if (bookDetails == null)
            {
                return NotFound();
            }

            return View(bookDetails);

        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(BookDetails bookDetails)
        {

            bookDetailsService.DeleteBook(bookDetails);


            return RedirectToAction(nameof(Index));

        }
    }
}
