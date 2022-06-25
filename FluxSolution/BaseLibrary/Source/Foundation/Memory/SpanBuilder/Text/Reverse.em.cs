namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reverse all ranged characters sort of in-place. Handles surrogates.</summary>
    public static void ReverseChars(ref this SpanBuilder<char> source, int startIndex, int endIndex)
    {
      if (startIndex < 0 || startIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      var offset = endIndex + 1;

      for (var index = endIndex; index >= startIndex; index--)
      {
        var c = source[index];

        if (char.IsLowSurrogate(c))
          source.Insert(offset++, source[--index]);
        else if (char.IsHighSurrogate(c))
          throw new System.InvalidOperationException(@"Orphan high surrogate (missing low surrogate).");

        source.Insert(offset++, c);
      }

      source.Remove(startIndex, endIndex - startIndex + 1);
    }
    /// <summary>Reverse all characters sort of in-place. Handles surrogates.</summary>
    public static void ReverseChars(ref this SpanBuilder<char> source)
      => ReverseChars(ref source, 0, source.Length - 1);
  }
}
