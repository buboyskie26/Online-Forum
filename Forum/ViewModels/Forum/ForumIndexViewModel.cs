using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Forum
{
    public class ForumIndexViewModel
    {
        public IEnumerable<ForumListingViewModel> ForumList { get; set; }

        public int PostCount { get; set; } 
        public int UserCount { get; set; }
    }
}
