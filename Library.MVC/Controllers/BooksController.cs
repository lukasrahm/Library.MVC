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
        private readonly IAuthorService authorService;
        private readonly ILoanService loanService;
        private readonly IMemberService memberService;

        public BooksController(IBookService bookService, IAuthorService authorService, ILoanService loanService, IMemberService memberService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.loanService = loanService;
            this.memberService = memberService;
        }

        //GET: Books
        public async Task<IActionResult> Index()
        {
            var vm = new BookIndexVm();
            vm.Copies = bookService.GetAllBookCopies();
            vm.Books = bookService.GetAllBooks();
            return View(vm);
        }

        //GET: Books
        public async Task<IActionResult> Admin()
        {
            var vm = new BookIndexVm();
            vm.Copies = bookService.GetAllBookCopies();
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
                var newBook = new Book();
                newBook.AuthorId = vm.AuthorId;
                newBook.Description = vm.Description;
                newBook.ISBN = vm.ISBN;
                newBook.Title = vm.Title;

                bookService.AddNewBook(newBook);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        public IActionResult Add()
        {
            var vm = new BookAddCopiesVm();
            vm.BookList = new SelectList(bookService.GetAllBooks(), "Id", "Title");
            return View(vm);
        }

        // POST: Books/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BookAddCopiesVm vm)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < vm.AmountOfCopies; i++)
                {
                    //Skapa ny bok
                    var copy = new BookCopy();

                    copy.BookId = vm.BookId;
                    bookService.AddBookCopy(copy);

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

            var book = bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            var vm = bookService.GetBook(id);
            return View(vm);


        }

        // GET: Books/Edit/Id
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bookDetails = bookService.GetBook(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ISBN,Title,AuthorId,Description")] Book book)
        {

            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookService.UpdateBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (id != book.Id)
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

            return View(book);

        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);

        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Book book)
        {

            bookService.DeleteBook(book);


            return RedirectToAction(nameof(Index));

        }
    }
}
