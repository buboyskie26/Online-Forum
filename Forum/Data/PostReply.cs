using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class PostReply
    {
        public int Id { get; set; }
        public int Like { get; set; }
        public int UnLike { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }

        public virtual IEnumerable<VoteReply> VoteReply { get; set; }

    }
}
