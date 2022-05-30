using Forum.Data;
using Forum.IContract;
using Forum.ViewModels.Forum;
using Forum.ViewModels.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly IForumx _forumService;
        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ForumController(IForumx forumService, IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager,
            IPost postService)
        {
            _hostingEnvironment = webHostEnvironment;
            _userManager = userManager;
            _forumService = forumService;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            /* var forum = await _forumService.GetAll();*/
            var forum = _forumService.GetAllSamp();
            var adForum = forum.Select(p => new ForumListingViewModel()
            {

                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Title = p.Title,
                PostCount = p.Posts.Count(),
                UserCount= _forumService.GetAllActiveUsers(p.Id).Count()
            });
           
            var model = new ForumIndexViewModel()
            {
                ForumList = adForum,
                
            };

            return View(model);
        }
        public async Task<IActionResult> Topic(int id, string searchQuery)
        {
            var forumId = await _forumService.GetById(id);

            /*var post = forumId.Posts;*/

            var post = await _postService.TopicFilteredPost(forumId, searchQuery);
            post  = post.ToList();

            var forumPost = post.Select(q => new ForumTopic()
            {
                PostContent=q.Content,
                PostCreate=q.Created,
                PostId=q.Id,
                PostTitle=q.Title,
                AuthorId = q.User.Id,
                AuthorName=q.User.UserName,
                AuthorRating=q.User.Rating,
                ReplyCount = q.Replies.Count(),
                VoteCount = q.Votes.Count(),
                Forum = BuildForum(q)
                
            });

            var model = new ForumTopicModel()
            {
                Post = forumPost,
                Forum = BuildForum(forumId)
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var obj = new ForumTopic();
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ForumTopic f)
        {
            if (ModelState.IsValid)
            {
                var image = "/images/users/default.png";

                var obj = new Forumx()
                {
                    Title = f.ForumTitle,
                    Description = f.ForumDescription,
                    Created = DateTime.Now,
                };

                if (f.ForumImageUrl != null && f.ForumImageUrl.Length > 0)
                {
                    var uploadDir = @"images/aProfile";

                    var fileName = Path.GetFileNameWithoutExtension(f.ForumImageUrl.FileName);
                    var extension = Path.GetExtension(f.ForumImageUrl.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    await f.ForumImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

                    obj.ImageUrl = "/" + uploadDir + "/" + fileName;
                }
                else
                {
                    obj.ImageUrl = image;
                };
                await _forumService.Create(obj);
                return RedirectToAction("Index");
            }
            return View(f);
        }
        [HttpPost]
        public async Task<IActionResult> Search(int id, string searchQuery)
        {


            return RedirectToAction("Topic", new { id, searchQuery });

        }
        private ForumListingViewModel BuildForum(Forumx q)
        {
            return new ForumListingViewModel()
            {
                Description = q.Description,
                Id = q.Id,
                Title = q.Title,
                ImageUrl = q.ImageUrl
            };
        }

        private ForumListingViewModel BuildForum(Post q)
        {
            var forum = q.Forum;
            return BuildForum(forum);
        }
    }
}
