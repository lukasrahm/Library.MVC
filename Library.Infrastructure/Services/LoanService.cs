using Library.Application.Interfaces;
using Library.Domain;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext context;

        public LoanService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddLoan(Loan loan)
        {
            context.Add(loan);
            context.SaveChanges();
        }

        public void UpdateLoan(Loan loan)
        {
            context.Update(loan);
            context.SaveChanges();
        }


        public IList<Loan> GetAllLoans()
        {
            return context.Loans.OrderBy(x => x.Returned).Include(x => x.BookCopy).ThenInclude(x => x.Book).ToList();
        }

        public Loan GetLoan(int? id)
        {
            return context.Loans.Find(id);
        }

        public void DeleteLoan(Loan loan)
        {
            context.Remove(loan);
            context.SaveChanges();

        }


    }
}
