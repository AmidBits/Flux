namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Determines whether the end of this ReadOnlySpan instance matches a specified target ReadOnlySpan.</summary>
    public static bool TrueForAll<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        if (!predicate(source[index]))
          return false;

      return true;
    }
  }
}
