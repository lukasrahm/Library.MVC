﻿using System;
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
    public class MembersController : Controller
    {
        private readonly IMemberService memberservice;

        public MembersController(IMemberService memberservice)
        {
            this.memberservice = memberservice;
        }

        //GET: Members
        public async Task<IActionResult> Index()
        {
            var vm = new MemberIndexVm();
            vm.Members = memberservice.GetAllMembers();
            return View(vm);
        }


        // GET: Members/Create
        public IActionResult Create()
        {
            var vm = new MemberCreateVm();
            return View(vm);
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

                memberservice.AddMember(newMember);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error","Home","");
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = memberservice.GetMemberById(id);
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
                    memberservice.UpdateMember(member);
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
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var member = memberservice.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }

            var vm = new Member();
            vm.Id = member.Id;
            vm.SSN = member.SSN;
            vm.Name = member.Name;
            return View(vm);


        }
    }
}
