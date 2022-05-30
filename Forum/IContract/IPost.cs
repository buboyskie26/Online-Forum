using Forum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.IContract
{
    public interface IPost
    {
        Task<Post> GetById(int id);
        Task<PostReply> GetReplyById(int id);
        Task<IEnumerable<Post>> GetAll();
        Task<IEnumerable<PostReply>> GetAllReply();

        Task<IEnumerable<Post>> SearchFilter(string search);

        Task<IEnumerable<Post>> TopicFilteredPost(Forumx forum,string searchQuery);
        Task<IEnumerable<Post>> GetFilteredPosts(Forumx forum, string searchQuery);
        Task<IEnumerable<Post>> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        Task<IEnumerable<Post>> GetLatestPosts(int postsNumber);

        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);

        Task AddReply(PostReply reply);
        Task UpdateReply(PostReply reply);
    }
}
