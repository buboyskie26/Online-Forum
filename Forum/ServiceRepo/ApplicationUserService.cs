using Forum.Data;
using Forum.IContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.ServiceRepo
{
    public class ApplicationUserService : IApplicationUser
    {

        private readonly ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
           return await _context.ApplicationUsers.ToListAsync();
        }

        public IEnumerable<ApplicationUser> GetAllSamp()
        {
            return  _context.ApplicationUsers.ToList();

        }

        public async Task<ApplicationUser> GetById(string id)
        {
            var obj = await GetAll();

            return obj.Where(q => q.Id == id).FirstOrDefault();
        }

        public async Task IncrementRating(string id, Type type)
        {
            var user = await GetById(id);

            user.Rating = CalculateRating(user.Rating, type);

            await _context.SaveChangesAsync();
        }

        private int CalculateRating(int rating, Type type)
        {
            var points = 0;
            if (type == typeof(Post))
                points = 1;
            else if (type == typeof(PostReply))
                points = 3;
            // points will be added in the rating
            return points + rating;

        }

        public Task SetProfileImage(string id, Uri uri)
        {
            throw new NotImplementedException();
        }

      

        public async Task Update(ApplicationUser applicationUser)
        {

             _context.Update(applicationUser);
             await _context.SaveChangesAsync();
        }

        public async Task DecrementRating(string id, Type type)
        {
            var user = await GetById(id);
            user.Rating = CalculateDecRating(user.Rating, type);

            await _context.SaveChangesAsync();
        }

        private int CalculateDecRating(int rating, Type type)
        {
            var points = 0;

            if (type == typeof(Post) || type == typeof(PostReply))
                points = -1;

            return points + rating;

        }
    }
}
