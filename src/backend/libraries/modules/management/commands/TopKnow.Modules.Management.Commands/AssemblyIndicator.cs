using System.Reflection;

namespace TopKnow.Modules.Management.Commands;

public static class ManagementCommandsAssemblyIndicator
{
    public static Assembly GetAssembly()
    {
        return typeof(ManagementCommandsAssemblyIndicator).Assembly;
    }
}
