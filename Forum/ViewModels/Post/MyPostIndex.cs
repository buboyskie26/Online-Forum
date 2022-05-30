using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Post
{
    public class MyPostIndex
    {
        public int PostId { get; set; }
        public int ForumId { get; set; }
        public string PostTitle{ get; set; }
        public string AuthorName{ get; set; }
        public string AuthorId{ get; set; }
        public string PostContent{ get; set; }
        public string ForumTitle{ get; set; }
        public string SearchQuery{ get; set; }
        public DateTime PostCreation { get; set; }
        public int ReplyCount { get; set; }
        public int VoteCount { get; set; }
    }
}
