using System.Reflection;

namespace TopKnow.Modules.PlayGround.Queries;

public static class PlayGroundQueriesAssemblyIndicator
{
    public static Assembly GetAssembly()
    {
        return typeof(PlayGroundQueriesAssemblyIndicator).Assembly;
    }
}
