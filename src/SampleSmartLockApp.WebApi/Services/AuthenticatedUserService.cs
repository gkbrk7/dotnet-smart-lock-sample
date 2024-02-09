namespace SampleSmartLockApp.WebApi.Services;

public class AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string UserId => _httpContextAccessor.HttpContext!.User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value!;
    public string? Name => _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
    public IEnumerable<UserRoles> Roles => _httpContextAccessor?.HttpContext?.User?.Claims?
        .Where(c => c.Type == ClaimTypes.Role)?
        .Select(c => (UserRoles)Enum.Parse(typeof(UserRoles), c.Value))
        .ToList() ?? [];
}
