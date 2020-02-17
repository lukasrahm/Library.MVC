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
            return context.Members.Find(id);
        }

        public void UpdateMember(Member member)
        {
            context.Update(member);
            context.SaveChanges();
        }
    }
}
