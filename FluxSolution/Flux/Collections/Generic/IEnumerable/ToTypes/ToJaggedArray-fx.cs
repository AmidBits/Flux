namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>Creates a jagged array <paramref name="source"/> using the specified <paramref name="countSelector"/> which specifies the number of elements (i.e. field/column count) for the next row.</summary>
    /// <remarks>For each row, a readonly list of all remaining elements is presented for decision on how many elements should appear on the next row.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static TResult[][] ToJaggedArray<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<System.Collections.Generic.IReadOnlyList<TSource>, int> countSelector, System.Func<TSource, TResult> resultSelector)
    {
      var sourceList = source.ToList();
      var resultLists = new System.Collections.Generic.List<TResult[]>();

      while (sourceList.Count != 0)
      {
        var count = int.Min(countSelector(sourceList), sourceList.Count); // Cannot select more elements than is in the list.

        var resultList = new System.Collections.Generic.List<TResult>();

        for (var index = 0; index < count; index++)
          resultList.Add(resultSelector(sourceList[index]));

        resultLists.Add([.. resultList]);

        sourceList.RemoveRange(0, count);
      }

      return [.. resultLists];
    }
  }
}
