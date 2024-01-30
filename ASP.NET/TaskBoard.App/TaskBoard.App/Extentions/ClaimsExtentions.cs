using System.Security.Claims;

namespace TaskBoard.App.Extentions
{
    public static class ClaimsExtentions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
