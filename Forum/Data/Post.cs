﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class Post
    { 
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Forumx Forum { get; set; }
        public virtual IEnumerable<PostReply> Replies { get; set; }
        public virtual IEnumerable<Vote> Votes { get; set; }
 



    }
}
