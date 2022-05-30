using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class VoteReply
    {
        public int Id { get; set; }
        public int LikeCount { get; set; }
        public int UnLikeCount { get; set; }
        public DateTime Created { get; set; }
        public bool? UpVoteReply { get; set; }
        public bool? DownVoteReply { get; set; }

        [ForeignKey("ReplyUserId")]
        public virtual ApplicationUser ReplyUser { get; set; }
        public string ReplyUserId { get; set; }

        public virtual PostReply Replies { get; set; }
    }
}
