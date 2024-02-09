namespace SampleSmartLockApp.WebApi.Attributes;
public class TestAuthorizeAttribute(int age) : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
{
    public int Age { get; private set; } = age;

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}


public class TestAuthorizeAttributeHandler : AuthorizationHandler<TestAuthorizeAttribute>
{
    private readonly UserManager<ApplicationUser> userManager;
    public TestAuthorizeAttributeHandler(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TestAuthorizeAttribute requirement)
    {
        var userId = context.User?.Claims?.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
        var roles = userManager.GetRolesAsync(new ApplicationUser { Id = userId! });
        // var roles = await UserManager.GetRolesAsync(new ApplicationUser { Id = userId! });
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}