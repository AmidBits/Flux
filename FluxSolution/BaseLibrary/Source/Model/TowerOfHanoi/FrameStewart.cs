namespace Flux.Model.TowerOfHanoi
{
  public static class FrameStewart
  {
    private static T Min<T>(System.Collections.Generic.IEnumerable<T> items, int count)
      where T : System.IComparable<T>
    {
      if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      using var e = items.GetEnumerator();

      if (e.MoveNext())
      {
        var min = e.Current;

        while (--count > 0 && e.MoveNext())
          if (min.CompareTo(e.Current) < 0)
            min = e.Current;

        if (count > 0) throw new System.ArgumentOutOfRangeException(nameof(count));

        return min;
      }
      else throw new System.InvalidOperationException(@"The sequence is empty.");
    }
    private static T Min<T>(T[] items)
      where T : System.IComparable<T>
      => Min(items, items.Length);

    //private static int Min(int[] items, int n)
    //{
    //  if (items is null) throw new System.ArgumentNullException(nameof(items));

    //  var min = items[0];

    //  for (var i = 1; i < n; i++)
    //    if (items[i] < min)
    //      min = items[i];

    //  return min;
    //}

    public static int NumberOfMoves(int numberOfRods, int numberOfDisks)
    {
      if (numberOfRods < 0) throw new System.ArgumentOutOfRangeException(nameof(numberOfRods));
      if (numberOfDisks < 0) throw new System.ArgumentOutOfRangeException(nameof(numberOfDisks));

      var moves = int.MaxValue;

      if (numberOfDisks <= 1)
        return numberOfDisks;

      if (numberOfRods == 3)
        return Maths.Pow(2, numberOfDisks) - 1;
      else if (numberOfRods >= 3 && numberOfDisks > 0)
      {
        var potentialMoves = new int[numberOfDisks - 1];
        for (var i = 1; i < numberOfDisks; i++)
          potentialMoves[i - 1] = 2 * NumberOfMoves(numberOfRods, i) + NumberOfMoves(numberOfRods - 1, numberOfDisks - i);
        return Min(potentialMoves, numberOfDisks - 1);
      }

      return moves;
    }

    //private static int Pow(int value, int exponent)
    //{
    //  var p = 1;
    //  for (var i = 0; i < exponent; i++)
    //    p *= value;
    //  return p;
    //}
  }
}
