namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    //public static string ToString<T>(this System.ReadOnlySpan<char> source, int startAt, int length)
    //{
    //  var sb = new System.Text.StringBuilder();
    //  foreach (var c in source.Slice(startAt, length).EnumerateChars())
    //    sb.Append(c);
    //  return sb.ToString();
    //}

    //public static string ToString<T>(this System.ReadOnlySpan<System.Text.Rune> source, int startAt, int length)
    //{
    //  var sb = new System.Text.StringBuilder();
    //  foreach (var r in source.Slice(startAt, length).EnumerateRunes())
    //    sb.Append(r);
    //  return sb.ToString();
    //}

    public static string ToString<T>(this System.ReadOnlySpan<T> source, int startAt, int length)
    {
      var sb = new System.Text.StringBuilder();
      for (; length > 0; length--, startAt++)
        sb.Append(source[startAt]?.ToString() ?? string.Empty);
      return sb.ToString();

      //var sb = new System.Text.StringBuilder();
      //foreach (var r in source.Slice(startAt, length).EnumerateElements())
      //  sb.Append(r);
      //return sb.ToString();



      //if (startAt < 0 || startAt >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(startAt));
      //if (length < 0 || startAt + length is var endIndex && endIndex > source.Length) throw new System.ArgumentOutOfRangeException(nameof(length));

      //var sb = new System.Text.StringBuilder();

      //for (int index = startAt, maxIndex = startAt + length; index < maxIndex; index++)
      //  sb.Append(source[index]?.ToString() ?? string.Empty);

      //return sb.ToString();
    }
  }
}
