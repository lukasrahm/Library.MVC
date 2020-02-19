using System.Collections.Generic;

namespace Library.Domain
{
    public class Member
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public IList<Loan> Loans { get; set; }

        public int Fees { get; set; }
    }
}