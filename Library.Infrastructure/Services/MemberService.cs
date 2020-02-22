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
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext context;

        public MemberService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddMember(Member member)
        {
            context.Add(member);
            context.SaveChanges();
        }

        public ICollection<Member> GetAllMembers()
        {
            // Here we are using .Include() to eager load the author, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.Members.ToList();
        }

        public void UpdateMemberDetails(Member member)
        {
            throw new NotImplementedException();
        }

        public void UpdateMemberDetails(int id, Member member)
        {
            throw new NotImplementedException();
        }

        public Member GetMemberById(int? id)
        {
            Member member = context.Members.Find(id);
            member.Loans = context.Loans.Where(x => x.MemberId == id).OrderBy(x => x.Returned).ToList();
            return member;
        }

        public void UpdateMember(Member member)
        {
            context.Update(member);
            context.SaveChanges();
        }


        public IList<Member> SearchMembers(string searching)
        {
            if (searching.All(c => char.IsDigit(c)))
            {
                    //searching for book SSN
                    return context.Members.Where(x => x.SSN.Contains(searching)).ToList();
            }

            return context.Members.Where(x => x.Name.Contains(searching)).ToList();
        }
    }
}
