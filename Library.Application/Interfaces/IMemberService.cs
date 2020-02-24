using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IMemberService
    {
        /// <summary>
        /// Adds the member to DB
        /// </summary>
        /// <param name="member"></param>
        void AddMember(Member member);

        /// <summary>
        /// Gets all members from the database
        /// </summary>
        /// <returns>list of members</returns>
        ICollection<Member> GetAllMembers();

        /// <summary>
        /// Returns a member by id
        /// </summary>
        /// <param name="id">Id of the member</param>
        /// <returns></returns>
        public Member GetMemberById(int? id);

        /// <summary>
        /// Updates a member
        /// </summary>
        /// <param name="member">Which member to update</param>
        void UpdateMember(Member member);

        /// <summary>
        /// Searches the database for specific members
        /// </summary>
        /// <param name="search">Search name or SSN</param>
        /// <returns></returns>
        IList<Member> SearchMembers(string search);

        void DeleteMember(Member member);
    }
}