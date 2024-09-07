using IdGen;
using Microsoft.AspNetCore.WebUtilities;

namespace Utilities;

public static class SnowFlakeIdGenerator
{
    private static readonly IdGenerator _idGenerator = new(generatorId: 233);
    public static long NewId => _idGenerator.First();

    public static string FromLongToBase64(long id)
    {
        var bytes = BitConverter.GetBytes(id);
        var base64 = WebEncoders.Base64UrlEncode(bytes);
        return base64;
    }

    public static long FromBase64ToLongId(string base64)
    {
        ReadOnlySpan<byte> bytes = WebEncoders.Base64UrlDecode(base64);
        var id = BitConverter.ToInt64(bytes);
        return id;
    }
}
