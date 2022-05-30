using Forum.ViewModels.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Post
{
    public class PostListingViewModel
    {

        public int PostId { get; set; }
        public string AuthorId { get; set; }
        public string PostTitle { get; set; }
        public string AuthorName { get; set; }

        public ForumListingViewModel Forum { get; set; }
        public int RepliesCount { get; set; }

    }
}
