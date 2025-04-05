namespace Flux
{
  public static partial class FxType
  {
    /// <summary>
    /// <para>Get the C# keyword alias of basic .NET built-in types.</para>
    /// <para>This was originally constructed for numeric types but expanded somewhat over time (for various reasons).</para>
    /// <para><see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/"/></para>
    /// </summary>
    public static bool TryGetCsharpKeyword(this System.Type source, out string keyword)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      keyword = (source == typeof(System.Boolean)) ? "bool"
              : (source == typeof(System.Byte)) ? typeof(System.Byte).Name.ToLowerInvariant()
              : (source == typeof(System.Char)) ? typeof(System.Char).Name.ToLowerInvariant()
              : (source == typeof(System.Decimal)) ? typeof(System.Decimal).Name.ToLowerInvariant()
              : (source == typeof(System.Double)) ? typeof(System.Double).Name.ToLowerInvariant()
              : (source == typeof(System.Enum)) ? typeof(System.Enum).Name.ToLowerInvariant()
              : (source == typeof(System.Int16)) ? "short"
              : (source == typeof(System.Int32)) ? "int"
              : (source == typeof(System.Int64)) ? "long"
              : (source == typeof(System.IntPtr)) ? "nint"
              : (source == typeof(System.Object)) ? typeof(System.Object).Name.ToLowerInvariant() // Reference type.
              : (source == typeof(System.SByte)) ? "sbyte"
              : (source == typeof(System.Single)) ? "float"
              : (source == typeof(System.String)) ? typeof(System.String).Name.ToLowerInvariant() // Reference type.
              : (source == typeof(System.UInt16)) ? "ushort"
              : (source == typeof(System.UInt32)) ? "uint"
              : (source == typeof(System.UInt64)) ? "ulong"
              : (source == typeof(System.UIntPtr)) ? "nuint"
              : string.Empty;

      return !string.IsNullOrEmpty(keyword);
    }
  }
}
