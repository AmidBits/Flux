namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Text.StringBuilder ToStringBuilder(this string source)
      => new System.Text.StringBuilder(source);
    public static System.Text.StringBuilder ToStringBuilder(this string source, int startIndex, int length)
      => new System.Text.StringBuilder(source, startIndex, length, length);
    public static System.Text.StringBuilder ToStringBuilder(this string source, int startIndex, int length, int capacity)
      => new System.Text.StringBuilder(source, startIndex, length, capacity);
  }
}
