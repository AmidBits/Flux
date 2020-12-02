namespace Flux
{
  public static partial class StringEm
  {
    /// <summary>Returns a (decoded) string from the Base64 (encoded) string.</summary>
    public static string Base64Decode(this string source)
      => System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(source));
    /// <summary>Returns a Base64 (encoded) string from the (decoded) string.</summary>
    public static string Base64Encode(this string source)
      => System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(source));
  }
}
