namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reverse all ranged characters, in-place.</summary>
          // THIS METHOD HAS TO HANDLE SURROGATES!!!
    internal static System.Text.StringBuilder ReverseImpl(this System.Text.StringBuilder source, int startIndex, int lastIndex)
    {
      while (startIndex < lastIndex)
      {
        var sc = source[startIndex]; // if high then accomodate...
        var lc = source[lastIndex]; // if low then accomodate...


        if (char.IsHighSurrogate(sc))
        {
          source[startIndex++] = source[lastIndex]; // Overwrite
          source[lastIndex--] = source[startIndex]; // Move low (2nd) surrogate to reversed position as low (2nd) surrogate, i.e. keep relationship.
          source[startIndex++] = source[lastIndex]; // 
          source[lastIndex--] = sc; // Move character from initial position to reverse position, if high (1st) surrogate then as high (1st) surrogate, i.e. keep relationship.
        }
        else if (char.IsLowSurrogate(lc))
        {
        }
        else
        {
          source[startIndex++] = lc; 
          source[lastIndex--] = sc; 
        }
      }

      return source;
    }

    /// <summary>Reverse all characters in-place.</summary>
    public static System.Text.StringBuilder Reverse(this System.Text.StringBuilder source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return ReverseImpl(source, 0, source.Length - 1);
    }
    /// <summary>Reverse all ranged characters in-place.</summary>
    public static System.Text.StringBuilder Reverse(this System.Text.StringBuilder source, int startIndex, int endIndex)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (startIndex < 0 || startIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      return ReverseImpl(source, startIndex, endIndex);
    }
  }
}
