using TopKnow.Common.Concretes;

namespace TopKnow.Modules.Management.RequestInputs;

public class PagedUserRequestInput : PagedQueryParameter
{
    public Guid? UserId { get; set; }
}
