using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Votex
{
    public class VoteListViewPost
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool? VoteUpPost { get; set; }
        public bool? VoteDownPost { get; set; }
        public DateTime VoteCreated { get; set; }
    }
}
