namespace Flux
{
  public static partial class IntEm
  {
    public static System.Collections.Generic.List<int> ToOrdinalRange(this System.Collections.Generic.IEnumerable<int> source, int maxNumber)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var distinct = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Distinct(source));

      var distinctCount = distinct.Count;

      for (var i = distinctCount; i < maxNumber; i++)
        distinct.Add(-1);

      for (var i = 0; i < distinctCount; i++)
        while (distinct[i] != -1 && distinct[distinct[i]] != distinct[i])
          distinct.AsSpan().Swap(i, distinct[i]);

      return distinct;
    }
  }
}
