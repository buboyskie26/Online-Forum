using Forum.Data;
using Forum.IContract;
using Forum.ViewModels.Forum;
using Forum.ViewModels.Post;
using Forum.ViewModels.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPost _postService; //private readonly field to store our PostService

        private static UserManager<ApplicationUser> _userManager;

        public SearchController(IPost postService,   UserManager<ApplicationUser> userManager)
        {
            _postService = postService; //Assign the private field in our constructor.
            _userManager = userManager;
        }
        public async Task<IActionResult> Results(string searchQuery)
        {

            var post = await _postService.SearchFilter(searchQuery);

            var emptySearch = string.IsNullOrEmpty(searchQuery) || post.Any() == false;

            var postList = post.Select(p => new ForumTopic()
            {
                AuthorId=p.User.Id,
                AuthorRating=p.User.Rating,

                PostTitle=p.Title,
                PostCreate=p.Created,
                PostId=p.Id,
                
                Forum = BuildForum(p.Forum),
                ReplyCount=p.Replies.Count()
            });

            var search = new SearchResultModel()
            {
                Posts = postList,
                SearchQuery = searchQuery,
                EmptySearch = emptySearch
            };

            return View(search);
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }
        private ForumListingViewModel BuildForum(Forumx f)
        {
            return new ForumListingViewModel()
            {
                Id = f.Id,
                Description = f.Description,
                Title = f.Title,
                ImageUrl = f.ImageUrl
            };
        }

    }
}
