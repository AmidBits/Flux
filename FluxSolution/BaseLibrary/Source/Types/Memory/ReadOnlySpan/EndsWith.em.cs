namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer.</summary>
    public static bool EndsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var sourceIndex = source.Length;
      var valueIndex = value.Length;

      if (sourceIndex < valueIndex) return false;

      while (--sourceIndex >= 0 && --valueIndex >= 0)
        if (!comparer.Equals(source[sourceIndex], value[valueIndex]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the default comparer.</summary>
    public static bool EndsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value)
      => EndsWith(source, value, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
