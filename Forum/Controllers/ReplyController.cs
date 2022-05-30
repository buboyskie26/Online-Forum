using Forum.Data;
using Forum.IContract;
using Forum.ViewModels;
using Forum.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IPost _postService; //private readonly field to store our PostService
        private readonly IForumx _forumService;
        private readonly IApplicationUser _userService;

        private static UserManager<ApplicationUser> _userManager;

        public ReplyController(IPost postService, IApplicationUser applicationUse,
            IForumx forumService, UserManager<ApplicationUser> userManager)
        {
            _userService = applicationUse;
            _postService = postService; //Assign the private field in our constructor.
            _userManager = userManager;
        }

        public async Task<IActionResult> Create(int id)
        {
            var p = await _postService.GetById(id);
            
            var obj = new PostReplyViewModel()
            {
               PostId=p.Id,
               AuthorId=p.User.Id,
               AuthorImageUrl=p.User.ProfileImageUrl,
               AuthorName = p.User.UserName,
               ForumId = p.Forum.Id,
               ForumImageUrl=p.Forum.ImageUrl,
               PostTitle = p.Forum.Title,
               PostContent = p.Content,

            };

            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyViewModel f)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var post = await _postService.GetById(f.PostId);
            var obj = new PostReply()
            {
                User=user,
               Post = post,
                Created = DateTime.Now,
                Content = f.ReplyContent
            };
            await _userService.IncrementRating(user.Id, typeof(PostReply));

            await _postService.AddReply(obj);

            return RedirectToAction("Index", "Post", new { id = f.PostId });
        }

        public async Task<IActionResult> LikeReply(int id)
        {

            var reply = await _postService.GetReplyById(id);

            var post = reply.Post;

           

            return RedirectToAction("");
        }

    }
}
