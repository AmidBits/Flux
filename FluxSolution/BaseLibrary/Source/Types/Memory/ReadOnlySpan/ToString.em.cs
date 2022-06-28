namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string ToString<T>(this System.ReadOnlySpan<T> source, int startAt, int length)
      where T : notnull
    {
      if (startAt < 0 || startAt >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(startAt));
      if (length < 0 || startAt + length is var endIndex && endIndex > source.Length) throw new System.ArgumentOutOfRangeException(nameof(length));

      var sb = new SpanBuilder<char>();

      for (var index = 0; index < source.Length; index++)
        sb.Append(source[index].ToString());

      return sb.AsReadOnlySpan().ToString();
    }
    public static string ToString<T>(this System.ReadOnlySpan<T> source, int startAt)
      where T : notnull
      => ToString(source, startAt, source.Length - startAt);
  }
}
