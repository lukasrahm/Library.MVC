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
            return context.Members.ToList();
        }

        public Member GetMemberById(int? id)
        {
            //Get member by id
            Member member = context.Members.Find(id);
            if (member != null)
            {
                //Add all the members loans, show loans that has not been returned first
                member.Loans = context.Loans.Where(x => x.MemberId == id).OrderBy(x => x.Returned).ToList();
            }

            return member;
        }

        public void UpdateMember(Member member)
        {
            context.Update(member);
            context.SaveChanges();
        }


        public IList<Member> SearchMembers(string search)
        {
            //If all characters are numbers
            if (search.All(c => char.IsDigit(c)))
            {
                //searching for member SSN
                return context.Members.Where(x => x.SSN.Contains(search)).ToList();
            }

            //If all characters was not numbers it searches member names instead
            return context.Members.Where(x => x.Name.Contains(search)).ToList();
        }

        public void DeleteMember(Member member)
        {
            context.Remove(member);
            context.SaveChanges();

        }
    }
}
