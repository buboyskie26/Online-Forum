using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Post
{
    public class MyPostList
    {
        public IEnumerable<MyPostIndex> PostIndex { get; set; }
        public string ForumTitle { get; set; }
        public int ForumId { get; set; }
        public string SearchQuery { get; set; }

    }
}
