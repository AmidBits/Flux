namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.List<int> ToOrdinalRange(this System.Collections.Generic.IEnumerable<int> source, int maxNumber)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var distinct = source.Distinct().ToList();

      var distinctCount = distinct.Count;

      for (var i = distinctCount; i < maxNumber; i++)
        distinct.Add(-1);

      for (var i = 0; i < distinctCount; i++)
        while (distinct[i] != -1 && distinct[distinct[i]] != distinct[i])
          distinct.Swap(i, distinct[i]);

      return distinct;
    }
  }
}
