using Library.Domain;
using System.Collections.Generic;

namespace Library.Application.Interfaces
{
    public interface ILoanService
    {
        /// <summary>
        /// Adds the loan to DB
        /// </summary>
        /// <param name="loan"></param>
        void AddNewLoan(Loan loan);

        IList<Loan> GetAllLoans();
        Loan GetLoan(int? id);

        /// <summary>
        /// Updates a loan.
        /// </summary>
        /// <param name="id">Id of loan to update</param>
        /// <param name="loan">New values of loan (Id is ignored)</param>
        void UpdateLoan(Loan loan);

        
        void DeleteLoan(Loan loan);

        IList<Loan> GetCopiesById(int? loanDetailId);
    }
}
