using Forum.Data;
using Forum.IContract;
using Forum.ViewModels;
using Forum.ViewModels.Post;
using Forum.ViewModels.Vote;
using Forum.ViewModels.VoteReplies;
using Forum.ViewModels.Votex;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Authorize]

    public class PostController : Controller
    {
        private readonly IPost _postService; //private readonly field to store our PostService
        private readonly IForumx _forumService;
        private readonly IApplicationUser _userService;
        private readonly IVote _vote;

        private static UserManager<ApplicationUser> _userManager;

        public PostController(IPost postService, IForumx forumService, IVote vote,
            IApplicationUser applicationUse,
            UserManager<ApplicationUser> userManager)
        {
            _vote = vote;
            _userService = applicationUse;
            _postService = postService; //Assign the private field in our constructor.
            _forumService = forumService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var p = await _postService.GetById(id);
            var vote = p.Votes;
            var voteReply = p.Replies;
            
            var isMatch = BuildVote(vote, p);

            bool upVote = vote.Any(q => q.IsVote == true);
            bool downVote = vote.Any(q => q.IsVoteDown == true);

            var likedCount = voteReply.Select(q => q.Like);
            var unlikedCount = voteReply.Select(q => q.UnLike);

            var upVoteCount = vote.Count(q => q.IsVote == true);
            var downVoteCount = vote.Count(q => q.IsVoteDown == true);

            var upVoteReplyCount = vote.Count(q => q.IsVoteReply == true);
            var downVoteReplyCount = vote.Count(q => q.IsVoteDownReply == true);

            var reply = p.Replies;

            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            /* var user = await _userManager.FindByNameAsync(User.Identity.Name);*/
            var postList = new PostIndexViewModel()
            {
                LoginId = user.Id,
                AuthorId = p.User.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
                PostContent = p.Content,
                PostCreation = p.Created,
                PostId = p.Id,
                PostTitle = p.Title,
                ForumId = p.Forum.Id,
                ForumName = p.Forum.Title,
                Replies = BuildReply(reply),
                Voting = BuildVotes(vote),
                AlreadyVoted = isMatch,

                IsUpVote = upVote,
                IsDownVote = downVote,

                DownVoteReplyCount = downVoteReplyCount,
                UpVoteReplyCount = upVoteReplyCount,

                DownVoteCount = downVoteCount,
                UpVoteCount = upVoteCount,

                // Vote Post
                VoteObj = BuildVoteObj(vote),
                // Vote Reply
                VoteReply = BuildVoteReply(voteReply),
                /*VReplyObj = BuildReplyObject(voteReply),*/

                LikeCount = likedCount.ToList(),
                UnLikeCount = unlikedCount.ToList(),

                VReplyObj = BuildLike(p.Replies)
            };
            var obj = postList.Voting.Select(q => q.AuthorId == user.Id).FirstOrDefault();
            var ni = postList.Voting.Select(q => q.AuthorId == postList.LoginId).FirstOrDefault();

            return View(postList);
        }

        private VoteIndexReply BuildLike(IEnumerable<PostReply> replies)
        {
            var obj = new VoteIndexReply();
            foreach (var item in replies)
            {
                obj = new VoteIndexReply()
                {
                    LikeCount = item.Like,
                    UnLikeCount=item.UnLike
                };

                return obj;
            }
            return obj;
            /*return  replies.Select(q => new VoteIndexReply()
            {
                LikeCount = q.Like,
                UnLikeCount=q.UnLike
            }).FirstOrDefault();*/
             
        }

        private VoteIndexReply BuildReplyObject(IEnumerable<PostReply> postReply)
        {
            var obj = new VoteIndexReply();
            foreach (var item in postReply)
            {
                
                foreach (var p in item.VoteReply)
                {
                    obj = new VoteIndexReply()
                    {
                        AuthorId = p.ReplyUser.Id,
                        AuthorImageUrl = p.ReplyUser.ProfileImageUrl,
                        AuthorName = p.ReplyUser.UserName,
                        VoteCreation = p.Created,
                        ReplyId = p.Replies.Id,
                        VoteUpReply = p.UpVoteReply,
                        VoteDownReply = p.DownVoteReply
                    };
                }
            }
            Type type = typeof(VoteIndexReply);
            int NumberOfRecords = type.GetProperties().Length;


            return obj;
        }
        private IEnumerable<ViewModels.Votex.VoteReply> BuildVoteReply(IEnumerable<PostReply> postReply)
        {
            var list = new List<ViewModels.Votex.VoteReply>();
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            foreach (var item in postReply)
            {

                list = item.VoteReply.Select(p => new ViewModels.Votex.VoteReply()
                {
                    AuthorId = p.ReplyUser.Id,
                    AuthorImageUrl = p.ReplyUser.ProfileImageUrl,
                    AuthorName = p.ReplyUser.UserName,
                    VoteCreation = p.Created,
                    ReplyId = p.Replies.Id,
                    VoteUpReply = p.UpVoteReply,
                    VoteDownReply = p.DownVoteReply
                }).ToList();

                var obj = list.Count();
                return list;
            }
            return list;
        }

        private IEnumerable<VotePost> BuildVoteObj(IEnumerable<Vote> vote)
        {
 
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var obj = vote.Where(q => q.User.Id == user.Id)
                .Select(p=> new VotePost()
                {
                    AuthorId = p.User.Id,
                    AuthorImageUrl = p.User.ProfileImageUrl,
                    AuthorName = p.User.UserName,
                    VoteCreation = p.Created,
                    PostId = p.Post.Id,
                    VoteDownPost = p.IsVoteDown,
                    VoteUpPost = p.IsVote,
                    VoteDownReply = p.IsVoteDownReply,
                    VoteUpReply=p.IsVoteReply


                }).ToList();


            return obj;

        }

        private IEnumerable<VotePost> BuildVotes(IEnumerable<Vote> vote)
        {
            var obj = vote.Select(p => new VotePost()
            {
                AuthorId = p.User.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
                VoteCreation = p.Created,
                PostId = p.Post.Id,
                VoteDownPost = p.IsVoteDown,
                VoteUpPost = p.IsVote
            });
            return obj;
        }

        private bool BuildVote(IEnumerable<Vote> vote, Post post)
        {
            return vote.Any(q => q.Post.Id == post.Id);

        }

        private IEnumerable<PostReplyViewModel> BuildReply(IEnumerable<PostReply> reply)
        {
            return reply.Select(p => new PostReplyViewModel()
            {
                AuthorId = p.User.Id,
                ReplyId = p.Id,
                AuthorImageUrl = p.User.ProfileImageUrl,
                AuthorName = p.User.UserName,
                ReplyContent = p.Content,
                ReplyCreation = p.Created,
                PostId = p.Post.Id,
                UnLikeCount = p.UnLike,
                LikeCount = p.Like
            });
        }

        public async Task<IActionResult> Create(int id)
        {
            var forum = await _forumService.GetById(id);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = new PostInForumTopicId()
            {
                AuthorId = user.Id,
                AuthorName = user.UserName,
                ForumId = forum.Id,
                ForumTitle = forum.Title
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(PostInForumTopicId f)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var forum = await _forumService.GetById(f.ForumId);
            var post = new Post()
            {
                User = user,
                Content=f.PostContent,
                Created = DateTime.Now,
                Title = f.PostTitle,
                Forum = forum
            };

            await _userService.IncrementRating(user.Id, typeof(Post));

            await _postService.Add(post);

            return RedirectToAction("Topic", "Forum", new { id = f.ForumId});

            /*return RedirectToAction("Index","Post", new { id= f.PostId });*/
        }
 

        public async Task<IActionResult> MyPost()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var post = await _postService.GetAll();


            var obj = post.Where(q=> q.User.Id==user.Id)
                .Select(p => new MyPostIndex()
                {

                    ForumTitle = p.Forum.Title,
                    ForumId = p.Forum.Id,

                    PostCreation = p.Created,
                    PostTitle = p.Title,
                    PostId = p.Id,
                    PostContent = p.Content,

                    AuthorName = p.User.UserName,
                    AuthorId = p.User.Id,
                    SearchQuery = "",

                     ReplyCount = p.Replies.Count()
                }).ToList();

            var objModel = new MyPostList();
            foreach (var item in obj)
            {

                objModel = new MyPostList()
                {
                    PostIndex = obj,
                    SearchQuery = "",
                    ForumId = item.ForumId,
                    ForumTitle = item.ForumTitle
                };
            }

 
         
            return View(objModel);
        }

    }
}
