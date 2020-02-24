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

        /// <summary>
        /// Updates a loan
        /// </summary>
        /// <param name="loan">Which loan to update</param>
        void UpdateLoan(Loan loan);

        /// <summary>
        /// Returns all loans in a list from db
        /// </summary>
        /// <returns>Loans</returns>
        IList<Loan> GetAllLoans();

        /// <summary>
        /// Returns a specific loan
        /// </summary>
        /// <param name="id">Id of the loan</param>
        /// <returns></returns>
        Loan GetLoan(int? id);
    }
}
