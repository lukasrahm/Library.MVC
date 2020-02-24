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
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {

            this.authorService = authorService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var vm = new AuthorIndexVm();
            
            if (search == null) //If search is empty the list will show all authors
            {
                vm.Authors = authorService.GetAllAuthors();
                return View(vm);
            }
            else //Show search result
            {
                vm.Authors = authorService.SearchAuthors(search);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                //Create new author
                var newAuthor = new Author();
                newAuthor.Name = vm.Name;

                authorService.AddAuthor(newAuthor);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        /// <summary>
        /// Shows all books from specific author
        /// </summary>
        /// <param name="authorId">Authors id</param>
        /// <returns>Books written by author</returns>
        public async Task<IActionResult> Books(int? authorId)
        {

            if (authorId == null)  //If the authorId field is empty
            {
                return NotFound();
            }

            var vm = authorService.GetAuthorById(authorId);

            if (vm == null) //If the author does not exist in db
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var author = authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            var vm = new AuthorEditVm();
            vm.Id = author.Id;
            vm.Name = author.Name;
            return View(vm);

        }

        // POST: Books/Edit/5
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
