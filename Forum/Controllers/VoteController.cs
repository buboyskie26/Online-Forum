using Forum.Data;
using Forum.IContract;
using Forum.ViewModels.Vote;
using Forum.ViewModels.Votex;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class VoteController : Controller
    {
        private readonly IVote _vote;
        private readonly IPost _postService;
        private static UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public VoteController(IVote vote, IPost post, IApplicationUser applicationUser,
            UserManager<ApplicationUser> userManager)
        {
            _userService = applicationUser;
            _vote = vote;
            _userManager = userManager;
            _postService = post;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> UpVote(int id)
        {
            var p = await _postService.GetById(id);

            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var obj = new VoteUpAndDown()
            {
                PostId = p.Id,
                PostTitle = p.Title,
                PostContent = p.Content,

                AuthorId = p.User.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
            };

            var vote = new Vote()
            {
                Post = p,
                Created = DateTime.Now,
                IsVote = true,
                IsVoteDown=null,
                User = user
            };

            /* var isMatch = _vote.isMatch(p, vote);
             if (isMatch)
             {
                 ModelState.AddModelError("", "Error");
                 return View("Index");
             }*/

            /*if (obj.PostId == vote.Post.Id)
            {
                ModelState.AddModelError("", "Error");

                return View("Index");
            }*/
            await _userService.IncrementRating(p.User.Id, typeof(Post));

            await _vote.Add(vote);
            return RedirectToAction("Index","Post",new { id = obj.PostId});
        }


        public async Task<IActionResult> DownVote(int id)
        {
            var p = await _postService.GetById(id);
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var obj = new VoteUpAndDown()
            {
                PostId = p.Id,
                PostTitle = p.Title,
                PostContent = p.Content,

                AuthorId = p.User.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
            };

            var vote = new Vote()
            {
                Post = p,
                Created = DateTime.Now,
                IsVote = null,
                IsVoteDown = true,
                User = user
            };


            await _userService.DecrementRating(p.User.Id, typeof(Post));

            await _vote.Add(vote);
            return RedirectToAction("Index", "Post", new { id = obj.PostId });

        }


        public async Task<IActionResult> UpVoteReply(int id)
        {
            var p = await _postService.GetById(id);

            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var obj = new VoteUpAndDown()
            {
                PostId = p.Id,
                PostTitle = p.Title,
                PostContent = p.Content,

                AuthorId = p.User.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
            };

            var vote = new Vote()
            {
                Post = p,
                Created = DateTime.Now,
                IsVoteReply=true,
                User=user
            };
 
            await _userService.IncrementRating(p.User.Id, typeof(PostReply));

            await _vote.Add(vote);
            return RedirectToAction("Index", "Post", new { id = obj.PostId });
        }

        public async Task<IActionResult> DownVoteReply(int id)
        {
            var p = await _postService.GetById(id);

            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var obj = new VoteUpAndDown()
            {
                PostId = p.Id,
                PostTitle = p.Title,
                PostContent = p.Content,

                AuthorId = p.User.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
            };

            var vote = new Vote()
            {
                Post = p,
                Created = DateTime.Now,
                IsVoteDownReply = true,
                User=user
            };

            await _userService.DecrementRating(p.User.Id, typeof(PostReply));

            await _vote.Add(vote);
            return RedirectToAction("Index", "Post", new { id = obj.PostId });
        }


        public async Task<IActionResult> ViewPostVote(int id)
        {
            var p = await _postService.GetById(id);
            var votes = p.Votes;

            var obj = new ViewVotePost()
            {
                PostId = p.Id,
                AuthorId = p.User.Id,
                PostTitle = p.Title,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
                ViewListPost = BuildVotes(votes)

            };
            return View(obj);
        }

        private IEnumerable<VoteListViewPost> BuildVotes(IEnumerable<Vote> votes)
        {
            return votes.Select(p => new VoteListViewPost()
            {
                AuthorId = p.User.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
                VoteCreated = p.Created,
                VoteDownPost = p.IsVoteDown,
                VoteUpPost=p.IsVote
            });
        }
    }
}
