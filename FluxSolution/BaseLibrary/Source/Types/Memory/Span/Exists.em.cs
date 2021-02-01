namespace Flux
{
  public static partial class SystemSpanEm
  {
    /// <summary>Indicates whether any element in the sequence satisfies the predicate.</summary>
    public static bool Exists<T>(this System.Span<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (predicate(source[index], index))
          return true;

      return false;
    }
  }
}
