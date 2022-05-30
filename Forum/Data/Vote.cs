using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class Vote
    {
        public int Id { get; set; }
        public bool? IsVote { get; set; }
        public bool? IsVoteDown { get; set; }
        public DateTime Created { get; set; }
        public bool? IsVoteReply { get; set; }
        public bool? IsVoteDownReply { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual Post Post { get; set; }
        /*public virtual IEnumerable<PostReply> Replies { get; set; }*/

 
    }
}
