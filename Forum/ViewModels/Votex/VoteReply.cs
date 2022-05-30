using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Votex
{
    public class VoteReply
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }

        public bool? VoteUpReply { get; set; }
        public bool? VoteDownReply { get; set; }

        public DateTime VoteCreation { get; set; }
        public string VoteContent { get; set; }
        public int PostId { get; set; }
        public int ReplyId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
    }
}
