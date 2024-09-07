namespace Exceptions.Exceptions;

public sealed class ConnectionStringNotFoundException(
    string? message = null
) : Exception
{
    public override string Message
        => $"No Connection String named {message ?? "(No name)"} is found in configurations. Please check appsettings.{message ?? "(No name)"}.json or local Secret.json";
}
