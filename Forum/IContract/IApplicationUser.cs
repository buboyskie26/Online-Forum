using Forum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.IContract
{
   public interface IApplicationUser
    {
        Task<ApplicationUser> GetById(string id);
        Task<IEnumerable<ApplicationUser>> GetAll();
        IEnumerable<ApplicationUser> GetAllSamp();
        Task SetProfileImage(string id, Uri uri);
        Task Update(ApplicationUser applicationUser);
        Task IncrementRating(string id, Type type);
        Task DecrementRating(string id, Type type);
    }
}
