using Forum.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Search
{
    public class SearchResultModel
    {
        public IEnumerable<ForumTopic> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearch { get; set; }
    }
}
