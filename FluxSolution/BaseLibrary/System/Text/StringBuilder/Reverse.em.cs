namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reverse all ranged characters sort of in-place. Handles surrogates.</summary>
    public static System.Text.StringBuilder Reverse(this System.Text.StringBuilder source, int startIndex, int endIndex)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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

      return source;
    }

    /// <summary>Reverse all characters sort of in-place. Handles surrogates.</summary>
    public static System.Text.StringBuilder Reverse(this System.Text.StringBuilder source)
      => Reverse(source, 0, source.Length - 1);
  }
}
