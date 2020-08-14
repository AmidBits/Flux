namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Returns a Span of char from the string builder.</summary>
    public static System.Span<char> ToSpan(this System.Text.StringBuilder source, int startIndex, int length)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var buffer = new char[length];
      for (var index = 0; index < length; index++, startIndex++)
        buffer[index] = source[startIndex];
      return new System.Span<char>(buffer);
    }
  }
}
