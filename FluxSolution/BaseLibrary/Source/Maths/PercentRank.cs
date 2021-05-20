namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Compute the precent rank for each of the specified count of items.</summary>
    /// <param name="count">The number of items in the set.</param>
    public static System.Collections.Generic.IEnumerable<double> PercentRank(int count)
    {
      if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var multiplier = 100.0 / --count;

      yield return 0;

      for (var index = 1; index < count; index++)
        yield return multiplier * index;

      yield return 100;
    }

    /// <summary>Compute the precent rank for the specified index in a set of specified count of items.</summary>
    /// <param name="count">The number of items in the set.</param>
    /// <param name="index">The index of the item to compute.</param>
    public static double PercentRank(int count, int index)
    {
      if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count));
      if (index < 0 && index > count) throw new System.ArgumentOutOfRangeException(nameof(index));

      if (index == 0)
        return 0;
      if (index == count)
        return 100;

      return 100.0 / (count - 1) * index;
    }
  }
}
