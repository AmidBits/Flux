namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports all first indices of the specified targets within the source (-1 if not found). Uses the specified comparer.</summary>
    public static int[] IndicesOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var indices = new int[values.Length];

      System.Array.Fill(indices, -1);

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
      {
        var sourceChar = source[sourceIndex];

        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        {
          if (indices[valueIndex] == -1 && comparer.Equals(sourceChar, values[valueIndex]))
          {
            indices[valueIndex] = sourceIndex;

            if (!System.Array.Exists(indices, i => i == -1))
              return indices;
          }
        }
      }

      return indices;
    }
    /// <summary>Reports all first indices of the specified targets within the source (-1 if not found). Uses the specified comparer.</summary>
    public static int[] IndicesOfAny<T>(this System.ReadOnlySpan<T> source, params T[] values)
      => IndicesOfAny(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
