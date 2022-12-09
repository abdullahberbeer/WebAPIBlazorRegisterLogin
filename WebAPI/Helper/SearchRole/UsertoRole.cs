using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Data;

namespace WebAPI.Helper.SearchRole
{
    public class UsertoRole : IUsertoRole
    {
        private readonly UserManager<Users> _userManager;

        public UsertoRole(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<string>> RoleName(Users user)
        {
            var role = await _userManager.GetRolesAsync(user);

            //var user = await _studentDBContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
            //var role = await _studentDBContext.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId).Result;

           
            return  role.ToList();
        }
    }
}
