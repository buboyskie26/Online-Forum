using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Reply
{
    public class ReplyLikeUnlike
    {
        public int PostId { get; set; }

        public string VoteAuthorId { get; set; }
        public string VoteAuthorName { get; set; }
        public string VoteAuthorImageUrl { get; set; }


    }
}
