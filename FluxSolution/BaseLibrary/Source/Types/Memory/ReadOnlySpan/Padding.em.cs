namespace Flux
{
  public static partial class XtendReadOnlySpan
  {
    /// <summary>Returns a string padded evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
    public static System.ReadOnlySpan<T> PadEven<T>(this System.ReadOnlySpan<T> source, int desiredWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool invertDefaultOddPaddingBehavior = false)
    {
      var sourceLength = source.Length;

      if (desiredWidth <= sourceLength) return source;

      var unfavoredWidth = (desiredWidth - sourceLength) / 2 + sourceLength;

      return invertDefaultOddPaddingBehavior ? source.PadRight(unfavoredWidth, paddingRight).PadLeft(desiredWidth, paddingLeft) : source.PadLeft(unfavoredWidth, paddingLeft).PadRight(desiredWidth, paddingRight);
    }

    /// <summary>Returns a new string that right-aligns this string by padding them on the left with the specified padding string.</summary>
    public static System.ReadOnlySpan<T> PadLeft<T>(this System.ReadOnlySpan<T> source, int desiredWidth, System.ReadOnlySpan<T> padding)
    {
      if (desiredWidth <= source.Length) return source;

      var paddingArray = padding.ToArray();

      var buffer = new T[desiredWidth];

      var copyCount = source.Length;
      var bufferIndex = buffer.Length - copyCount;
      System.Array.Copy(source.ToArray(), 0, buffer, bufferIndex, copyCount);

      while (bufferIndex > 0)
      {
        copyCount = System.Math.Min(bufferIndex, paddingArray.Length);

        bufferIndex -= copyCount;

        System.Array.Copy(paddingArray, paddingArray.Length - copyCount, buffer, bufferIndex, copyCount);
      }

      return buffer;
    }

    /// <summary>Returns a new string that left-aligns this string by padding them on the right with the specified padding string.</summary>
    public static System.ReadOnlySpan<T> PadRight<T>(this System.ReadOnlySpan<T> source, int desiredWidth, System.ReadOnlySpan<T> padding)
    {
      if (desiredWidth <= source.Length) return source;

      var paddingArray = padding.ToArray();

      var buffer = new T[desiredWidth];

      source.CopyTo(buffer);

      for (int copyCount = source.Length, bufferIndex = copyCount; bufferIndex < desiredWidth; bufferIndex += copyCount)
      {
        copyCount = System.Math.Min(desiredWidth - bufferIndex, paddingArray.Length);

        System.Array.Copy(paddingArray, 0, buffer, bufferIndex, copyCount);
      }

      return buffer;
    }
  }
}
