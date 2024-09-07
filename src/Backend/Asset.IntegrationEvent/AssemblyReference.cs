using System.Reflection;

namespace Asset.IntegrationEvent;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
