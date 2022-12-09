using WebAPI.Data;

namespace WebAPI.Helper.SearchRole
{
    public interface IUsertoRole
    {
        Task<List<string>> RoleName(Users user);
    }
}
