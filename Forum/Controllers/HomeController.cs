using Forum.Data;
using Forum.IContract;
using Forum.Models;
using Forum.ViewModels;
using Forum.ViewModels.Forum;
using Forum.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IPost _postService;

        public HomeController(IPost postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {

            var post = await _postService.GetLatestPosts(10);
            var homeIndex = post.Select(q => new PostListingViewModel()
            {
               AuthorId=q.User.Id,
               AuthorName=q.User.UserName,
               PostId = q.Id,
               PostTitle=q.Title,
               RepliesCount=q.Replies.Count(),
               Forum = BuildForum(q.Forum)
                
            });
            var model = new HomeIndexViewModel()
            {
                SearchQuery = "",
                Post = homeIndex
            };
            return View(model);
        }

        private ForumListingViewModel BuildForum(Forumx f)
        {
            return new ForumListingViewModel()
            {
                Id=f.Id,
                Title=f.Title,
                Description=f.Description,
                ImageUrl=f.ImageUrl
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
