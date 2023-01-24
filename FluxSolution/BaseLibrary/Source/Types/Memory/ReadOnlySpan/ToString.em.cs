namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static string ToString<T>(this System.ReadOnlySpan<T> source, int startAt, int length)
    {
      if (startAt < 0 || startAt >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(startAt));
      if (length < 0 || startAt + length is var endIndex && endIndex > source.Length) throw new System.ArgumentOutOfRangeException(nameof(length));

      var sb = new System.Text.StringBuilder();

      for (int index = startAt, maxIndex = startAt + length; index < maxIndex; index++)
        sb.Append(source[index]?.ToString() ?? string.Empty);

      return sb.ToString();
    }
    public static string ToString<T>(this System.ReadOnlySpan<T> source, int startAt)
      => ToString(source, startAt, source.Length - startAt);
  }
}
