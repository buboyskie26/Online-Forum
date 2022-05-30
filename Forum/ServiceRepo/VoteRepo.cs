using Forum.Data;
using Forum.IContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ServiceRepo
{
    public class VoteRepo : IVote
    {
        private readonly ApplicationDbContext _context;

        public VoteRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Vote Vote)
        {
            await _context.Votes.AddAsync(Vote);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Vote>> GetAll()
        {
            return await _context.Votes
                .Include(q => q.Post).ThenInclude(q=> q.User)
                .Include(q => q.User)
                .ToListAsync();
        }

        public async Task<Vote> GetById(int id)
        {
            return await _context.Votes
                .Include(q=> q.User)
                .Include(q => q.Post).ThenInclude(q => q.User)
                .FirstOrDefaultAsync(q => q.Id == id);
             
        }

        public bool isMatch(Post post, Vote vote)
        {
            if(post.Id == vote.Post.Id)
            {
                return true;
            }
           
            return false;
           
        }

        public async Task Update(Vote Vote)
        {
            throw new NotImplementedException();
        }
    }
}
