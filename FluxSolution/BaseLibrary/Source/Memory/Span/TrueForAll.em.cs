namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Determines whether the end of this ReadOnlySpan instance matches a specified target ReadOnlySpan.</summary>
    public static bool TrueForAll<T>(this System.Span<T> source, System.Func<T, bool> predicate)
    {
      var sourceIndex = source.Length;

      while (sourceIndex-- > 0)
        if (!predicate(source[sourceIndex]))
          return false;

      return true;
    }
  }
}
