using Forum.Data;
using Forum.IContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ServiceRepo
{
    public class PostRepo : IPost
    {
        private readonly ApplicationDbContext _context;

        public PostRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            await _context.PostReplies.AddAsync(reply);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts
                .Include(q=> q.User)
                .Include(q=> q.Forum)
                .Include(q => q.Votes).ThenInclude(q => q.User)
                .Include(q=> q.Replies).ThenInclude(q=> q.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<PostReply>> GetAllReply()
        {
            return await _context.PostReplies
                .Include(q=> q.User)
                .Include(q=> q.Post).ThenInclude(w=> w.User)
                .Include(q => q.VoteReply).ThenInclude(q => q.ReplyUser)
                .ToListAsync();

        }

        public async Task<Post> GetById(int id)
        {
            return await _context.Posts.Where(q => q.Id == id)
                .Include(q => q.User)
                .Include(q => q.Replies).ThenInclude(q=> q.VoteReply).ThenInclude(q=> q.ReplyUser)
                .Include(q=> q.Votes).ThenInclude(q=> q.User)
                .Include(q => q.Forum)
                .Include(q => q.Replies).ThenInclude(q => q.User)
                .FirstOrDefaultAsync(q=> q.Id==id);
        }

        public async Task<IEnumerable<Post>> GetFilteredPosts(Forumx forum, string searchQuery)
        {

            return string.IsNullOrEmpty(searchQuery) ? forum.Posts :
                forum.Posts.Where(i => i.Title.ToLower().Contains(searchQuery.ToLower())
                 || i.Content.ToLower().Contains(searchQuery.ToLower()));
        }

        public async Task<IEnumerable<Post>> GetFilteredPosts(string searchQuery)
        {
            var obj = await GetAll();
             return obj.Where(i => i.Title.ToLower().Contains(searchQuery.ToLower())
                 || i.Content.ToLower().Contains(searchQuery.ToLower()));
        }

        public async Task<IEnumerable<Post>> GetLatestPosts(int postsNumber)
        {
            var obj = await GetAll();
            return obj.OrderByDescending(q => q.Created).Take(postsNumber);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PostReply> GetReplyById(int id)
        {
            return await _context.PostReplies
                .Include(q => q.Post).ThenInclude(w => w.User)
                .Include(q => q.User)
                .Include(q=> q.VoteReply).ThenInclude(q=> q.ReplyUser)
                .FirstOrDefaultAsync(q => q.Id == id);

        }

        public async Task<IEnumerable<Post>> SearchFilter(string search)
        {
            var posts = await GetAll();

            return posts.Where(q => q.Title.ToLower().Contains(search.ToLower()) ||
                q.Content.ToLower().Contains(search.ToLower()));
        }

        public async Task<IEnumerable<Post>> TopicFilteredPost(Forumx forum,string searchQuery)
        {
            var obj = await GetAll();
            return string.IsNullOrEmpty(searchQuery) ? forum.Posts :
                obj.Where(q => q.Title.ToLower().Contains(searchQuery.ToLower())
                || q.Content.ToLower().Contains(searchQuery.ToLower()));
        }

        public async Task UpdateReply(PostReply reply)
        {
            _context.PostReplies.Update(reply);
            await _context.SaveChangesAsync();
        }
    }
}
