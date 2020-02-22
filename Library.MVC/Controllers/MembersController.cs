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
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace Library.MVC.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService memberService;
        private readonly IBookService bookService;
        private readonly ILoanService loanService;

        public MembersController(IMemberService memberService, IBookService bookService, ILoanService loanService)
        {
            this.memberService = memberService;
            this.bookService = bookService;
            this.loanService = loanService;
        }

        //GET: Members and get searched member
        public async Task<IActionResult> Index(string searching)
        {
            var vm = new MemberIndexVm();
            if (searching == null)
            {
                vm.Members = memberService.GetAllMembers();
                return View(vm);
            }
            else
            {
                vm.Members = memberService.SearchMembers(searching);
                return View(vm);
            }
        }
        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                //Skapa ny bok
                var newMember = new Member();
                newMember.SSN = vm.SSN;
                newMember.Name = vm.Name;

                memberService.AddMember(newMember);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = memberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }

            var vm = new MemberEditVm();
            vm.SSN = member.SSN;
            vm.Name = member.Name;
            return View(vm);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SSN")] Member member)
        {

            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    memberService.UpdateMember(member);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (id != member.Id)
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

            return View(member);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var vm = new MemberDetailsVm();
            var member = memberService.GetMemberById(id);


            if (member == null)
            {
                return NotFound();
            }

            vm.Books = bookService.GetAllBooks();
            vm.Fees = member.Fees;
            vm.Loans = member.Loans;
            vm.Name = member.Name;
            vm.SSN = member.SSN;
            vm.Id = member.Id;


            return View(vm);


        }

        // GET: Members/ReturnBook  
        public async Task<IActionResult> ReturnBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = loanService.GetLoan(id);
            var copy = bookService.GetBookCopy(loan.BookCopyId);
            var member = memberService.GetMemberById(loan.MemberId);

            if (loan == null || copy == null || member == null)
            {
                return NotFound();
            }

            loan.Returned = true;
            copy.OnLoan = false;


            //If the book is returned late we add a fee
            DateTime date = DateTime.Today.ToLocalTime();
            if (date > loan.DateOfReturn)
            {
                int lateFee = Convert.ToInt32((date - loan.DateOfReturn).TotalDays) * 12;
                member.Fees += lateFee;
                memberService.UpdateMember(member);
            }

            loanService.UpdateLoan(loan);
            bookService.UpdateBookCopy(copy);


            return RedirectToAction("Index", "Members");
        }

        [AllowAnonymous]
        //Make payment
        [HttpPost]
        public async Task<IActionResult> Details(int id, int payment)
        {
            if (ModelState.IsValid)
            {
                //Avbetala
                var member = memberService.GetMemberById(id);

                member.Fees -= payment;

                memberService.UpdateMember(member);

               return RedirectToAction("Details", "Members", id);
            }

            return RedirectToAction("Error", "Home", "");
        }
    }
}
