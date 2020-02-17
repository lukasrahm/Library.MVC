using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Domain;
using Library.MVC.Models;
using Library.Application.Interfaces;

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
            vm.Members = memberService.GetAllMembers();
            return View(vm);
        }


        // GET: Loans/Make
        public async Task<IActionResult> Make(int? id)
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

            var vm = new LoanMakeVm();
            vm.Members = new SelectList(memberService.GetAllMembers(), "Id", "Name");
            return View(vm);
        }

        // POST: Loans/Make
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Make(LoanMakeVm vm)
        {
            if (ModelState.IsValid)
            {

                var newLoan = new Loan();
                newLoan.BookId = vm.BookId;
                newLoan.MemberId = vm.MemberId;
                newLoan.DateOfLoan = vm.DateOfLoan;
                newLoan.DateOfReturn = vm.DateOfReturn;

                loanService.AddLoan(newLoan);


                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }
    }
}
