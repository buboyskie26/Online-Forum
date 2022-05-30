using Forum.ViewModels.Vote;
using Forum.ViewModels.VoteReplies;
using Forum.ViewModels.Votex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Post
{
    public class PostIndexViewModel
    {
        public List<int> LikeCount { get; set; }
        public List<int> UnLikeCount { get; set; }

        public int ForumId { get; set; }
        public string ForumName { get; set; }

        public string LoginId { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }


        public string PostTitle{ get; set; }
        public DateTime PostCreation { get; set; }
        public string PostContent { get; set; }
        public int PostId {get; set; }
        public bool AlreadyVoted {get; set; }
        public bool IsUpVote {get; set; }
        public bool IsDownVote {get; set; }

        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        public int UpVoteReplyCount { get; set; }
        public int DownVoteReplyCount { get; set; }
        public IEnumerable<PostReplyViewModel> Replies { get; set; }
        public IEnumerable<VotePost> Voting { get; set; }


        public IEnumerable<VotePost> VoteObj { get; set; }
        public IEnumerable<VoteReply> VoteReply { get; set; }


        public VoteIndexReply VReplyObj { get; set; }



    }
}
