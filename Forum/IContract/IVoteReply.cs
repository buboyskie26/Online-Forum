using Forum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.IContract
{
    public interface IVoteReply
    {
        Task<VoteReply> GetById(int id);
        Task<IEnumerable<VoteReply>> GetAll();
        Task Add(VoteReply VoteReply);
        Task Update(VoteReply VoteReply);
        Task Delete(int id);
    }
}
