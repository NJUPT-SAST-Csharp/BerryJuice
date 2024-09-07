using System.Reflection;

namespace Accounts.IntegrationEvent;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
