using Forum.ViewModels.Forum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Post
{
    public class ForumTopic
    {

        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime PostCreate { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }

        public int ReplyCount { get; set; }
        public int VoteCount { get; set; }
        public ForumListingViewModel Forum { get; set; }


        public string ForumTitle { get; set; }
        public DateTime ForumCreation { get; set; }
        public IFormFile ForumImageUrl { get; set; }
        public string ForumDescription { get; set; }

    }

}
