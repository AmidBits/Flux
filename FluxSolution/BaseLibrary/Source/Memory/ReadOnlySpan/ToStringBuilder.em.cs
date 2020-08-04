namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<char> source)
      => new System.Text.StringBuilder(source.ToString());
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<char> source, int startIndex, int length)
      => new System.Text.StringBuilder(source.ToString(), startIndex, length, length);
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<char> source, int startIndex, int length, int capacity)
      => new System.Text.StringBuilder(source.ToString(), startIndex, length, capacity);
  }
}
