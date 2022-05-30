using Forum.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Forum
{
    public class ForumPostTopic
    {
        public IEnumerable<PostListingViewModel> Posts { get; set; }
        public ForumListingViewModel Forum { get; set; }
        public string SearchQuery { get; set; }
    }
}
