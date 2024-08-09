using System;
using System.IO;
using System.IO.Compression;
using System.Text;

public static class StringCompression
{
    public static string Compress(this string str)
    {
        byte[] compressedBytes;
        using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(str)))
        {
            using (var compressedStream = new MemoryStream())
            {
                using (var compressorStream = new DeflateStream(compressedStream, CompressionLevel.Optimal, false))
                {
                    uncompressedStream.CopyTo(compressorStream);
                }

                compressedBytes = compressedStream.ToArray();
            }
        }
        return Convert.ToBase64String(compressedBytes);
    }

    public static string Decompress(this string str)
    {
        byte[] decompressedBytes;

        var compressedStream = new MemoryStream(Convert.FromBase64String(str));

        using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
        {
            using (var decompressedStream = new MemoryStream())
            {
                decompressorStream.CopyTo(decompressedStream);

                decompressedBytes = decompressedStream.ToArray();
            }
        }

        return Encoding.UTF8.GetString(decompressedBytes);
    }
}