using System.Security.Claims;
using HightechAngular.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HightechAngular.Identity.Services
{
    public class UserContext : IUserContext
    {
        private User _user;

        public UserContext(IHttpContextAccessor httpContextAccessor, DbContext context)
        {
            HttpContextAccessor = httpContextAccessor;
            DbContext = context;
        }

        private IHttpContextAccessor HttpContextAccessor { get; }

        private DbContext DbContext { get; }

        public User User => _user ??= GetUser();

        private User GetUser()
        {
            var id = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return DbContext.Set<User>().Find(id);
        }
    }
}