using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.VoteReplies
{
    public class VoteIndexReply
    {
        public string AuthorId { get; set; }
        public int ReplyId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool? VoteUpReply { get; set; }
        public bool? VoteDownReply { get; set; }
        public DateTime VoteCreation { get; set; }


        public int LikeCount { get; set; }
        public int UnLikeCount { get; set; }

    }
}
