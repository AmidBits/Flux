namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a string padded evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
    public static System.ReadOnlySpan<T> PadEven<T>(this System.ReadOnlySpan<T> source, int desiredWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool invertDefaultOddPaddingBehavior = false)
    {
      if (desiredWidth <= source.Length)
        return source;

      var target = source.ToArray();
      PadEven(target, desiredWidth, paddingLeft, paddingRight, invertDefaultOddPaddingBehavior);
      return target;
    }

    /// <summary>Returns a new string that right-aligns this string by padding them on the left with the specified padding string.</summary>
    public static System.ReadOnlySpan<T> PadLeft<T>(this System.ReadOnlySpan<T> source, int desiredWidth, System.ReadOnlySpan<T> padding)
    {
      if (desiredWidth <= source.Length)
        return source;

      var target = source.ToArray();
      PadLeft(target, desiredWidth, padding);
      return target;
    }

    /// <summary>Returns a new string that left-aligns this string by padding them on the right with the specified padding string.</summary>
    public static System.ReadOnlySpan<T> PadRight<T>(this System.ReadOnlySpan<T> source, int desiredWidth, System.ReadOnlySpan<T> padding)
    {
      if (desiredWidth <= source.Length)
        return source;

      var target = source.ToArray();
      PadRight(target, desiredWidth, padding);
      return target;
    }
  }
}
