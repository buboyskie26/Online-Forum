using Forum.Data;
using Forum.IContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ServiceRepo
{
    public class ForumxRepo : IForumx
    {
        private readonly ApplicationDbContext _context;

        public ForumxRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Forumx> GetById(int id)
        {
            var forum = await _context.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts).ThenInclude(p => p.User)
                .Include(f => f.Posts).ThenInclude(p => p.Replies).ThenInclude(r => r.User)
                .FirstOrDefaultAsync();
            return forum;
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Forumx>> GetAll()
        {
            return await _context.Forums.OrderByDescending(q => q.Posts.Count())
                .Include(forum => forum.Posts)
                .GroupBy(q => q.Title).Select(q => q.First())
                .ToListAsync();
 
        }
        public IEnumerable<Forumx> GetAllSamp()
        {
            return  _context.Forums.OrderByDescending(q => q.Posts.Count())
                .Include(forum => forum.Posts)
                .ToList()
                .GroupBy(q => q.Title).Select(q => q.First());
            //throw new NotImplementedException();
            /**/
        }
        public IEnumerable<ApplicationUser> GetAllActiveUsers(int id)
        {

            var forum = _context.Forums
                .Include(q=> q.Posts).ThenInclude(q=> q.User)
                .Include(q=> q.Posts).ThenInclude(q=> q.Replies).ThenInclude(r => r.User)
                .FirstOrDefault(q => q.Id == id);

            var posts = forum.Posts;

            var postUser = posts.Select(q => q.User);

            var replyUser = posts.SelectMany(q => q.Replies).Select(q=> q.User);

            return postUser.Union(replyUser).Distinct();
        }

        public async Task Create(Forumx forum)
        {
            await _context.Forums.AddAsync(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }
    }
}
