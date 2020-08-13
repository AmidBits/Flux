namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<char> source)
      => new System.Text.StringBuilder(source.ToString());
    public static System.Text.StringBuilder ToStringBuilder(this System.ReadOnlySpan<char> source, int capacity)
      => new System.Text.StringBuilder(source.ToString(), capacity);
  }
}
