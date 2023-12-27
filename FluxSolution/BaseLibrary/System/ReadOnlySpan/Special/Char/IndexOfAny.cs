namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Reports the first index of any of the specified targets within the source, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char>? comparer = null, params string[] values)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        if (source.IndexOf(values[valueIndex], comparer) is var index && index > -1)
          return index;

      return -1;
    }
  }
}
