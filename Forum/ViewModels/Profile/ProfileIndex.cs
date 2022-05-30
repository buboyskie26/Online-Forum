using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ViewModels.Profile
{
    public class ProfileIndex
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorImageUrl { get; set; }
        public IFormFile ProfileImage { get; set; }
        public int AuthorRating { get; set; }
        public DateTime MemberSince { get; set; }

    }
}
