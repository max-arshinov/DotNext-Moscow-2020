using HightechAngular.Identity.Entities;
using JetBrains.Annotations;

namespace HightechAngular.Identity.Services
{
    public interface IUserContext
    {
        [CanBeNull]
        public User User { get; }

        public bool IsAuthenticated => User != null;
    }
}