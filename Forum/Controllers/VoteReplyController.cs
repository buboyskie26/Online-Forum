using Forum.Data;
using Forum.IContract;
using Forum.ViewModels.Reply;
using Forum.ViewModels.Vote;
using Forum.ViewModels.VoteReplies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class VoteReplyController : Controller
    {

        private readonly IVote _vote;
        private readonly IVoteReply _voteReply;
        private readonly IPost _postService;
        private static UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public VoteReplyController(IVote vote, IPost post, IVoteReply voteReply,
            IApplicationUser applicationUser,
            UserManager<ApplicationUser> userManager)
        {
            _voteReply = voteReply;
            _userService = applicationUser;
            _vote = vote;
            _userManager = userManager;
            _postService = post;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> UpVoteReply(int id)
        {
            var p = await _postService.GetReplyById(id);
            var post = p.Post;
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;

            var obj = new VoteUpAndDown()
            {
                PostId = post.Id,
                PostContent = p.Content,
                ReplyId = p.Id,
                AuthorId = p.User.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
            };
            var vote = new VoteReply()
            {
                Replies = p,
                Created = DateTime.Now,
                UpVoteReply = true,
                ReplyUser = user
            };

            await _userService.DecrementRating(p.User.Id, typeof(PostReply));

            await _voteReply.Add(vote);
            return RedirectToAction("Index", "Post", new { id = obj.PostId });
        }

        public async Task<IActionResult> DownVoteReply(int id)
        {
            var p = await _postService.GetReplyById(id);
            var post = p.Post;
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
           
            var obj = new VoteUpAndDown()
            {
                PostId = post.Id,
                PostContent = p.Content,
                ReplyId = p.Id,
                AuthorId = p.User.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
            };
            var vote = new VoteReply()
            {
                Replies = p,
                Created = DateTime.Now,
                DownVoteReply = true,
                ReplyUser = user
            };

            await _userService.DecrementRating(p.User.Id, typeof(PostReply));

            await _voteReply.Add(vote);
            return RedirectToAction("Index", "Post", new { id = obj.PostId });
        }

        public async Task<IActionResult> LikeReply(int id)
        {

            var reply = await _postService.GetReplyById(id);

            var post = reply.Post;
            // redirect to the post
            var postId = post.Id;

            var userId = _userManager.GetUserId(User);
            var loginUser = _userManager.FindByIdAsync(userId).Result;
 
            reply.Like += 1;


            var replyObj = new VoteReply()
            {
                LikeCount =1,
                Created = DateTime.Now,
                ReplyUser = loginUser,
                Replies = reply,
                UpVoteReply = true,

            };
            // update reply

            // Create VoteReply
            await _postService.UpdateReply(reply);
            await _voteReply.Add(replyObj);
            return RedirectToAction("Index", "Post", new { id = postId });

        }
        public async Task<IActionResult> UnLikeReply(int id)
        {

            var reply = await _postService.GetReplyById(id);

            var post = reply.Post;
            // redirect to the post
            var postId = post.Id;

            var userId = _userManager.GetUserId(User);
            var loginUser = _userManager.FindByIdAsync(userId).Result;

            reply.UnLike += 1;


            var replyObj = new VoteReply()
            {
                UnLikeCount = 1,
                Created = DateTime.Now,
                ReplyUser = loginUser,
                Replies = reply,
                DownVoteReply = true,
            };
            // update reply

            // Create VoteReply
            await _postService.UpdateReply(reply);
            await _voteReply.Add(replyObj);
            return RedirectToAction("Index", "Post", new { id = postId });

        }


        public async Task<IActionResult> ViewReplyPostVote(int id)
        {
            var reply = await _postService.GetReplyById(id);

            var voteReply = reply.VoteReply;

            var obj = new ViewLikeReply()
            {
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorName = reply.User.UserName,
                ReplyContent = reply.Content,
                
                ViewUserLike = BuildViewUserLike(voteReply)

            };

            return View(obj);
        }

        private IEnumerable<ViewUserLikeReply> BuildViewUserLike(IEnumerable<VoteReply> voteReply)
        {
            return voteReply.Select(p => new ViewUserLikeReply()
            {
                AuthorId = p.ReplyUser.Id,
                AuthorImageUrl = p.ReplyUser.ProfileImageUrl,
                AuthorName = p.ReplyUser.UserName,
                VoteCreation = p.Created,
                IsLike = p.UpVoteReply,
                IsUnLike = p.DownVoteReply
            });
        }
    }
}
