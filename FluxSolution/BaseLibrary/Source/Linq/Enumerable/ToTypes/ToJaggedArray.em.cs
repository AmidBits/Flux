using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a jagged array <paramref name="source"/> using the specified <paramref name="countSelector"/> which specifies the number of elements (i.e. field/column count) for the next row.</summary>
    /// <remarks>For each row, a readonly list of all remaining elements is presented for decision on how many elements should appear on the next row.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static TSource[][] ToJaggedArray<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<System.Collections.Generic.IReadOnlyList<TSource>, int> countSelector)
    {
      var sourceList = source.ToList();
      var targetList = new System.Collections.Generic.List<TSource[]>();

      while (sourceList.Any())
      {
        var count = System.Math.Min(countSelector(sourceList), sourceList.Count); // Cannot select more elements than is in the list.

        var target = new System.Collections.Generic.List<TSource>();

        for (var index = 0; index < count; index++)
          target.Add(sourceList[index]);

        targetList.Add(target.ToArray());

        sourceList.RemoveRange(0, count);
      }

      return targetList.ToArray();
    }
  }
}
