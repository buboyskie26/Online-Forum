using Forum.Data;
using Forum.IContract;
using Forum.ViewModels.Profile;
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
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProfileController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment,   
            IApplicationUser applicationUser
            )
        {
            _hostingEnvironment = webHostEnvironment;
            _userService = applicationUser;
            _userManager = userManager;
        }
        public IActionResult Index()
        {

            /*   var user = await _userService.GetAll();*/
            var user = _userService.GetAllSamp().OrderByDescending(q => q.MemberSince).Select(q => new ProfileIndex()
            {
                AuthorEmail= q.Email,
                AuthorId=q.Id,
                AuthorImageUrl=q.ProfileImageUrl,
                AuthorName=q.UserName,
                AuthorRating=q.Rating,
                MemberSince=q.MemberSince
            });

            var model = new ProfieList()
            {
                Users = user
            };

            return View(model);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userService.GetById(id);

            var objUser = new ProfileIndex()
            {
                AuthorEmail=user.Email,
                AuthorId=user.Id,
                AuthorImageUrl=user.ProfileImageUrl,
                AuthorName=user.UserName,
                AuthorRating=user.Rating,
                MemberSince=user.MemberSince
            };

            return View(objUser);
        }
        [HttpPost]
        public async Task<IActionResult> Detail(ProfileIndex model)
        {
            var obj = await _userService.GetById(model.AuthorId);
            if(obj != null)
            {
                if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                {
                    var uploadDir = @"images/aProfile";

                    var fileName = Path.GetFileNameWithoutExtension(model.ProfileImage.FileName);
                    var extension = Path.GetExtension(model.ProfileImage.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    await model.ProfileImage.CopyToAsync(new FileStream(path, FileMode.Create));

                    obj.ProfileImageUrl = "/" + uploadDir + "/" + fileName;
                }
                await _userService.Update(obj);
                return RedirectToAction("Detail", new{ id= model.AuthorId });

            }
            return View(model);
        }
    }
}
