using Forum.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels
{
    public class HomeIndexViewModel
    {
        public string SearchQuery { get; set; }

        public IEnumerable<PostListingViewModel> Post{ get; set; }


    }
}
