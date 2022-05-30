using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels
{
    public class PostReplyViewModel
    {
        public int ForumId { get; set; }
        public int ReplyId { get; set; }
        public string ForumName { get; set; }
        public string ForumImageUrl { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }


        public DateTime ReplyCreation { get; set; }
        public string ReplyContent { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }


        public int LikeCount { get; set; }
        public int UnLikeCount { get; set; }

    }
}
