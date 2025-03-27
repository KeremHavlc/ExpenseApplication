using Business.Abstract;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business.Concrete
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

                Console.WriteLine($"User Claim: {userIdClaim}");

                if (Guid.TryParse(userIdClaim, out Guid userId))
                    return userId;

                throw new UnauthorizedAccessException("Kullanıcı kimliği doğrulanmadı!");
            }
        }
    }
}
