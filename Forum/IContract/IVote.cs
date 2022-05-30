using Forum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.IContract
{
    public interface IVote
    {
        Task<Vote> GetById(int id);
        Task<IEnumerable<Vote>> GetAll();
        Task Add(Vote Vote);
        Task Update(Vote Vote);
        Task Delete(int id);

        bool isMatch(Post post, Vote vote);
    }
}
