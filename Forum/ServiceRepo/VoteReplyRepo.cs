using Forum.Data;
using Forum.IContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ServiceRepo
{
    public class VoteReplyRepo : IVoteReply
    {

        private readonly ApplicationDbContext _context;

        public VoteReplyRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(VoteReply VoteReply)
        {
            await _context.VoteReply.AddAsync(VoteReply);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VoteReply>> GetAll()
        {
            return await _context.VoteReply
                .Include(q => q.Replies).ThenInclude(q => q.User)
                .Include(q => q.ReplyUser)
                .ToListAsync();
        }

        public async Task<VoteReply> GetById(int id)
        {
            return await _context.VoteReply
                .Include(q => q.ReplyUser)
                .Include(q => q.Replies).ThenInclude(q => q.User)
                .FirstOrDefaultAsync(q => q.Id == id);

        }

        public bool isMatch(Post post, VoteReply VoteReply)
        {
            throw new NotImplementedException();

        }

        public async Task Update(VoteReply VoteReply)
        {
            throw new NotImplementedException();
        }
    }
}
