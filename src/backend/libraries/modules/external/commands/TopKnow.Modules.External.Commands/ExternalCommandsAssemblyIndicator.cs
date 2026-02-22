using System.Reflection;

namespace TopKnow.Modules.External.Commands;

public static class ExternalCommandsAssemblyIndicator
{
    public static Assembly GetAssembly()
    {
        return typeof(ExternalCommandsAssemblyIndicator).Assembly;
    }
}
