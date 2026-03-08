using TopKnow.Common.Enums;

namespace TopKnow.Management.Client.ViewModels;

public class AdminUserRequestOutput
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
}

public class LoginRequestOutput
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public UserType UserType { get; set; }
    public string Token { get; set; }
}

public class LoginRequestInput
{
    public string Mail { get; set; }
    public string Password { get; set; }
}