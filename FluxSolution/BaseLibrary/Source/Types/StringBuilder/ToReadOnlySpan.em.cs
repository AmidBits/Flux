namespace Flux
{
  public static partial class StringBuilderEm
  {
    /// <summary>Returns a Span of char from the string builder.</summary>
    public static System.ReadOnlySpan<char> ToReadOnlySpan(this System.Text.StringBuilder source, int startIndex, int length)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var buffer = new char[length];
      source.CopyTo(startIndex, buffer, length);
      return buffer;
    }
  }
}
