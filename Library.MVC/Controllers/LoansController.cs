using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Domain;
using Library.MVC.Models;
using Library.Application.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;

namespace Library.MVC.Controllers
{
    public class LoansController : Controller
    {
        private readonly ILoanService loanService;
        private readonly IBookService bookService;
        private readonly IMemberService memberService;
        private readonly IBookDetailsService detailsService;

        public LoansController(ILoanService loanService, IBookService bookService, IMemberService memberService, IBookDetailsService detailsService)
        {
            this.loanService = loanService;
            this.bookService = bookService;
            this.memberService = memberService;
            this.detailsService = detailsService;
        }

        //GET: Loans
        public async Task<IActionResult> Index()
        {
            var vm = new LoansVm();
            vm.Loans = loanService.GetAllLoans();
            vm.Books = bookService.GetAllBooks();
            vm.Details = detailsService.GetAllBookDetails();
            vm.Members = memberService.GetAllMembers();
            return View(vm);
        }


        // GET: Loans/Make
        public async Task<IActionResult> Make(int? id, LoanMakeVm vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = detailsService.GetBookDetails(id);
            if (book == null)
            {
                return NotFound();
            }

            vm.Members = new SelectList(memberService.GetAllMembers(), "Id", "Name");
            vm.Details = book;
            vm.DateOfLoan = DateTime.Today.ToLocalTime();
            vm.DateOfReturn = DateTime.Today.ToLocalTime().AddDays(14);

            return View(vm);
        }

        // POST: Loans/Make
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Make(int id, LoanMakeVm vm)
        {
            if (ModelState.IsValid)
            {
                var book = detailsService.GetBookDetails(id);
                Loan loan = new Loan();

                foreach (var copy in book.Copies)
                {
                    if (!copy.OnLoan)
                    {
                        loan.BookId = copy.Id;
                    }
                }
                DateTime dateOfLoan = DateTime.Today.ToLocalTime();
                DateTime dateOfReturn = DateTime.Today.ToLocalTime().AddDays(14);
                loan.DateOfLoan = dateOfLoan;
                loan.DateOfReturn = dateOfReturn;
                loan.MemberId = vm.MemberId;
                loanService.AddLoan(loan);

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }
    }
}
