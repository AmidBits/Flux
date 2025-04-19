namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>Reverse all ranged characters sort of in-place. Handles surrogates.</summary>
    public static System.Text.StringBuilder Reverse(this System.Text.StringBuilder source, int startIndex, int endIndex)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (startIndex < 0 || startIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      while (startIndex < endIndex)
        source.Swap(startIndex++, endIndex--);

      return source;
    }

    /// <summary>Reverse all characters sort of in-place. Handles surrogates.</summary>
    public static System.Text.StringBuilder Reverse(this System.Text.StringBuilder source)
      => Reverse(source, 0, source.Length - 1);
  }
}
