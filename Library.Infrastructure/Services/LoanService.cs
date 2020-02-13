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

        public void AddNewLoan(Loan book)
        {
            context.Add(book);
            context.SaveChanges();
        }

        public void UpdateLoan(Loan book)
        {
            context.Update(book);
            context.SaveChanges();
        }


        public IList<Loan> GetAllLoans()
        {
            // Here we are NOT using .Include() so the authors books will NOT be loaded, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.Loans.OrderBy(x => x.DateOfLoan).ToList();
        }

        public Loan GetLoan(int? id)
        {
            return context.Loans.Find(id);
        }

        public void DeleteLoan(Loan book)
        {
            context.Remove(book);
            context.SaveChanges();

        }

        public IList<Loan> GetCopiesById(int? detailId)
        {
            return context.Loans.Where(book => book.Id == detailId).ToList();
        }
    }
}
