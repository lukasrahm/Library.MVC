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
    public class AuthorsController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;

        public AuthorsController(IBookService bookService, IAuthorService authorService)
        {

            this.bookService = bookService;
            this.authorService = authorService;
        }

        public async Task<IActionResult> Index(string searching)
        {
            var vm = new AuthorIndexVm();
            if (searching == null)
            {
                vm.Authors = authorService.GetAllAuthors();
                return View(vm);
            }
            else
            {
                vm.Authors = authorService.SearchAuthors(searching);
                return View(vm);
            }

        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            var vm = new AuthorCreateVm();
            return View(vm);
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                //Skapa ny bok
                var newAuthor = new Author();
                newAuthor.Name = vm.Name;

                authorService.AddAuthor(newAuthor);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        public async Task<IActionResult> Books(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }

            var vm = authorService.GetAuthorById(Id);
            return View(vm);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var authorDetails = authorService.GetAuthorById(id);
            if (authorDetails == null)
            {
                return NotFound();
            }

            var vm = new AuthorEditVm();
            vm.Id = authorDetails.Id;
            vm.Name = authorDetails.Name;
            return View(vm);

        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Author author)
        {

            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    authorService.UpdateAuthor(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (id != author.Id)
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

            return View(author);

        }
    }
}
