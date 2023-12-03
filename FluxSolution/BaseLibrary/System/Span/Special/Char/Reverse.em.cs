namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reverse all characters in <paramref name="source"/>. Handles surrogates.</summary>
    public static System.Span<char> ReverseCharacters(ref this System.Span<char> source)
    {
      var startIndex = 0;
      var endIndex = source.Length - 1;

      if (endIndex >= 1) // At least 2 characters in span.
        for (; startIndex < endIndex; startIndex++, endIndex--)
        {
          var cs = source[startIndex];
          var ce = source[endIndex];

          if (char.IsLowSurrogate(cs))
            throw new System.InvalidOperationException(@"Unexpected low surrogate.");
          else if (char.IsHighSurrogate(ce))
            throw new System.InvalidOperationException(@"Unexpected high surrogate.");
          else if (char.IsHighSurrogate(cs) || char.IsLowSurrogate(ce))
          {
            source.Swap(startIndex, endIndex - 1);
            source.Swap(startIndex + 1, endIndex);

            startIndex++;
            endIndex--;
          }
          else
            source.Swap(startIndex, endIndex);
        }

      return source;
    }
  }
}
