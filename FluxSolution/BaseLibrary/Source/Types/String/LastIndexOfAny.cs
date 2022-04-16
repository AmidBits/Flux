namespace Flux
{
  public static partial class StringEm
  {
    /// <summary>Reports the last index of any of the targets within the source. or -1 if none is found. Uses the specified comparer.</summary>
    public static int LastIndexOfAny(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valueIndex = values.Length - 1; valueIndex >= 0; valueIndex--)
          if (source.EqualsAt( sourceIndex, values[valueIndex], comparer))
            return sourceIndex;

      return -1;
    }
    /// <summary>Reports the last index of any of the targets within the source. or -1 if none is found. Uses the default comparer</summary>
    public static int LastIndexOfAny(this System.ReadOnlySpan<char> source, params string[] values)
      => LastIndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);
  }
}
