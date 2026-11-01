using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("User context is not present");
            }

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            var email = user.FindFirstValue(ClaimTypes.Email) ?? user.Identity.Name ?? string.Empty;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
            var dateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
            var dateOfBirth = dateOfBirthString == null
                ? (DateOnly?)null
                : DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");

            return new CurrentUser(userId, email, roles, nationality, dateOfBirth);
        }
    }
}
