using System.Reflection;

namespace TopKnow.Modules.Management.Queries;

public static class ManagementQueriesAssemblyIndicator
{
    public static Assembly GetAssembly()
    {
        return typeof(ManagementQueriesAssemblyIndicator).Assembly;
    }
}
