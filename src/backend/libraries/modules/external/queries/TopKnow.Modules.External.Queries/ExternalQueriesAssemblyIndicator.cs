using System.Reflection;

namespace TopKnow.Modules.External.Queries;

public static class ExternalQueriesAssemblyIndicator
{
    public static Assembly GetAssembly()
    {
        return typeof(ExternalQueriesAssemblyIndicator).Assembly;
    }
}
