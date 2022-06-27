namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports all last indices of the specified targets within the source (-1 if not found). Uses the specified comparer.</summary>
    public static int[] LastIndicesOfAll<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> equalityComparer, params T[] values)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var lastIndices = new int[values.Length];

      System.Array.Fill(lastIndices, -1);

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
      {
        var sourceChar = source[sourceIndex];

        for (var valueIndex = values.Length - 1; valueIndex >= 0; valueIndex--)
        {
          if (lastIndices[valueIndex] == -1 && equalityComparer.Equals(sourceChar, values[valueIndex]))
          {
            lastIndices[valueIndex] = sourceIndex;

            if (!System.Array.Exists(lastIndices, i => i == -1))
              return lastIndices;
          }
        }
      }

      return lastIndices;
    }
    /// <summary>Reports all last indices of the specified targets within the source (-1 if not found). Uses the default comparer.</summary>
    public static int[] LastIndicesOfAll<T>(this System.ReadOnlySpan<T> source, params T[] values)
      => LastIndicesOfAll(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
