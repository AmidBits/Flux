namespace Flux
{
  public static partial class ExtensionMethodsString
  {
    public static System.Text.StringBuilder ToStringBuilder(this string source)
      => new(source);
    public static System.Text.StringBuilder ToStringBuilder(this string source, int startIndex, int length)
      => new(source, startIndex, length, length);
    public static System.Text.StringBuilder ToStringBuilder(this string source, int startIndex, int length, int capacity)
      => new(source, startIndex, length, capacity);
  }
}
