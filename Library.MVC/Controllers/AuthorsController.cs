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
        private readonly IAuthorService authorservice;

        public AuthorsController(IAuthorService authorservice)
        {
            this.authorservice = authorservice;
        }

        //GET: Authors
        public async Task<IActionResult> Index()
        {
            var vm = new AuthorIndexVm();
            vm.Authors = authorservice.GetAllAuthors();
            return View(vm);
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

                authorservice.AddAuthor(newAuthor);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error","Home","");
        }
    }
}
