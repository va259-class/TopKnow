using System.Reflection;

namespace TopKnow.Modules.PlayGround.Commands;

public static class PlayGroundCommandsAssemblyIndicator
{
    public static Assembly GetAssembly()
    {
        return typeof(PlayGroundCommandsAssemblyIndicator).Assembly;
    }
}
