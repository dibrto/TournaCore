using System.Security.Claims;

namespace TournaCore.API.Common {
    public static class ClaimsPrincipalExtensions {
        public static string GetEmail(this ClaimsPrincipal user) {
            return user.FindFirstValue(ClaimTypes.Email)
                ?? throw new UnauthorizedAccessException("Email claim is missing.");
        }
    }
}
