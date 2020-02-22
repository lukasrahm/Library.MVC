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
        /// Updates a member
        /// </summary>
        /// <param name="member"></param>
        void UpdateMemberDetails(Member member);

        /// <summary>
        /// Updates a member.
        /// </summary>
        /// <param name="id">Id of member to update</param>
        /// <param name="member">New values of member (Id is ignored)</param>
        void UpdateMemberDetails(int id, Member member);
        /// <summary>
        /// Gets all members from the database
        /// </summary>
        /// <returns>list of members</returns>
        ICollection<Member> GetAllMembers();

        public Member GetMemberById(int? id);

        void UpdateMember(Member member);

        IList<Member> SearchMembers(string searching);
    }
}