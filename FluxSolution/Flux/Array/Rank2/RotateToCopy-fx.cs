namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Create a new two-dimensional array from <paramref name="source"/> with the elements rotated counter-clockwise.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static T[,] RotateToCopy<T>(this T[,] source, RotationalDirection direction)
    {
      source.AssertRank(2);

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength1, sourceLength0]; // Swap the length of the dimensions.

      var sl0m1 = sourceLength0 - 1;
      var sl1m1 = sourceLength1 - 1;

      for (var s0 = 0; s0 < sourceLength0; s0++)
        for (var s1 = 0; s1 < sourceLength1; s1++)
          switch (direction)
          {
            case RotationalDirection.ClockWise:
              target[s1, sl0m1 - s0] = source[s0, s1];
              break;
            case RotationalDirection.CounterClockWise:
              target[sl1m1 - s1, s0] = source[s0, s1];
              break;
          }

      return target;
    }
  }
}
