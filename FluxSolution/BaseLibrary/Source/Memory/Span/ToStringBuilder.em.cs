namespace Flux
{
  public static partial class XtensionsSpan
  {
    public static System.Text.StringBuilder ToStringBuilder(this System.Span<char> source)
      => new System.Text.StringBuilder(source.ToString());
    public static System.Text.StringBuilder ToStringBuilder(this System.Span<char> source, int capacity)
      => new System.Text.StringBuilder(source.ToString(), capacity);
  }
}
