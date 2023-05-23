namespace Flux
{
  public static partial class ExtensionMethodsString
  {
    /// <summary>Reports the last index of any of the targets within the source. or -1 if none is found. Uses the specified comparer.</summary>
    public static int LastIndexOfAny(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valueIndex = values.Length - 1; valueIndex >= 0; valueIndex--)
          if (source.EqualsAt(sourceIndex, values[valueIndex], equalityComparer))
            return sourceIndex;

      return -1;
    }
  }
}
