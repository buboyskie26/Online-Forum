using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.VoteReplies
{
    public class ViewUserLikeReply
    {
        public string AuthorId { get; set; }
        public string AuthorImageUrl { get; set; }
        public string AuthorName { get; set; }
        public string ReplyContent { get; set; }
        public DateTime VoteCreation { get; set; }
        public bool? IsLike { get; set; }
        public bool? IsUnLike { get; set; }
    }
}
