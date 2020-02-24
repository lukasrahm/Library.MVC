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
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Library.MVC.Controllers
{
    public class BooksController : Controller
    {

        private readonly IAuthorService authorService;
        private readonly IBookService bookService;

        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            this.authorService = authorService;
            this.bookService = bookService;
        }


        //GET: Books
        public async Task<IActionResult> Index(string search)
        {
            var vm = new BookIndexVm();

            if (search == null)
            {
                vm.Books = bookService.GetAllBooks();
                vm.Copies = bookService.GetAllBookCopies();
                return View(vm);
            }
            else
            {
                vm.Books = bookService.SearchBooks(search);
                vm.Copies = bookService.GetAllBookCopies();
                return View(vm);
            }
        }

        //GET: Books
        public async Task<IActionResult> Admin(string search)
        {
            var vm = new BookIndexVm();

            if (search == null)
            {
                vm.Books = bookService.GetAllBooks();
                vm.Copies = bookService.GetAllBookCopies();
                return View(vm);
            }
            else
            {
                vm.Books = bookService.SearchBooks(search);
                vm.Copies = bookService.GetAllBookCopies();
                return View(vm);
            }
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var vm = new BookCreateVm();
            vm.Authors = new SelectList(authorService.GetAllAuthors(), "Id", "Name");
            //vm.AmountOfCopies = 0;
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

                for (int i = 0; i < vm.AmountOfCopies; i++)
                {
                    BookCopy copy = new BookCopy();
                    copy.BookId = newBook.Id;
                    bookService.AddBookCopy(copy);
                }

                return RedirectToAction(nameof(Admin));
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

                return RedirectToAction(nameof(Admin));
            }

            return RedirectToAction("Error", "Home", "");
        }


        // GET: Books/Details
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
            return View(book);
        }

        // GET: Books/AdminDetails
        public async Task<IActionResult> AdminDetails(int? id)
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

        // GET: Books/Edit
        public async Task<IActionResult> Edit(int? id)
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

            var vm = new BookEditVm();
            vm.Id = book.Id;
            vm.ISBN = book.ISBN;
            vm.Title = book.Title;
            vm.AuthorId = book.AuthorId;
            vm.Description = book.Description;
            vm.Authors = new SelectList(authorService.GetAllAuthors(), nameof(Author.Id), nameof(Author.Name), book.AuthorId);
            vm.AmountOfCopies = book.Copies.Count();
            return View(vm);

        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ISBN,Title,AuthorId,Description")] Book book, BookEditVm vm)
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
                book.Copies = bookService.GetAllBookCopies(book.Id);
                if (book.Copies.Count() > vm.AmountOfCopies)
                {
                    int difference = book.Copies.Count() - vm.AmountOfCopies;   //Calculate how many more copies user wants deleted
                    List<BookCopy> copiesToDelete = new List<BookCopy>();
                    foreach (var copy in book.Copies)
                    {
                        if(!copy.OnLoan)  //If the copy is not on loan the copy will be deleted
                        {
                            copiesToDelete.Add(copy);
                            difference--;
                        }
                        else if(copy.OnLoan && difference == book.Copies.Count())   //If the library is deleting all copies, delete loaned ones aswell
                        {
                            copiesToDelete.Add(copy);
                            difference--;
                        }
                        if (difference == 0)
                        {
                            break;
                        }

                    }
                    for (int i = 0; i < copiesToDelete.Count(); i++)
                    {
                        bookService.DeleteBookCopy(copiesToDelete[i]); //Delete copies
                    }
                }
                else if (book.Copies.Count() < vm.AmountOfCopies)    //If the user wants more copies of a book
                {
                    int difference = vm.AmountOfCopies - book.Copies.Count();   //Calculate how many more copies user wants added
                    for (int i = 0; i < difference; i++)
                    {
                        BookCopy copy = new BookCopy();
                        copy.BookId = book.Id;
                        bookService.AddBookCopy(copy);  //Add a new copy
                    }
                }

                return RedirectToAction(nameof(Admin));
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

            bookService.DeleteBook(book); //Delete book

            var copies = bookService.GetAllBookCopies(book.Id); //Get all copies of the book

            foreach(var copy in copies)
            {
                bookService.DeleteBookCopy(copy);   //Delete all copies of the book
            }

            return RedirectToAction(nameof(Admin));

        }
    }
}
