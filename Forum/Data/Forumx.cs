﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class Forumx
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string ImageUrl { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }

    }
}
