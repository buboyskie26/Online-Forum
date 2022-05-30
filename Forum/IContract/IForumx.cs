using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;

namespace Forum.IContract
{
    public interface IForumx
    {
        Task<Forumx> GetById(int id);
        Task<IEnumerable<Forumx>> GetAll();
        IEnumerable<Forumx> GetAllSamp();
        IEnumerable<ApplicationUser> GetAllActiveUsers(int id);

        Task Create(Forumx Forumx);
        Task Delete(int ForumxId);
        Task UpdateForumTitle(int ForumxId, string newTitle);
        Task UpdateForumDescription(int ForumxId, string newDescription);
    }
}
