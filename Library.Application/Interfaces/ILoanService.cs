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
        void AddLoan(Loan loan);

        void UpdateLoan(Loan loan);

        IList<Loan> GetAllLoans();
        Loan GetLoan(int? id);

        
        void DeleteLoan(Loan loan);
    }
}
