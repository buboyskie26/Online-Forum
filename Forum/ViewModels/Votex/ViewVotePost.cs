using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Votex
{
    public class ViewVotePost
    {
        public string AuthorId { get; set; }
        public string PostTitle { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public int PostId { get; set; }

        public IEnumerable<VoteListViewPost> ViewListPost { get; set; }
    }
}
