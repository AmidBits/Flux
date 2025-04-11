namespace Flux.Model.Knapsack
{
  /// <summary>
  /// <para>Given a set of items, each with a weight and a value, determine the number of each item to include in a collection so that the total weight is less than or equal to a given limit and the total value is as large as possible.</para>
  /// </summary>
  /// <see cref="https://en.wikipedia.org/wiki/Knapsack_problem"/>
  public sealed class Knapsack
  {
    public int NumberOfDistinctItems { get; init; }
    public int WeightCapacity { get; init; }

    public int[] Weights { get; init; }
    public int[] Values { get; init; }

    public Knapsack(int weightCapacity, int numberOfDistinctItems, int[] weights, int[] values)
    {
      WeightCapacity = weightCapacity;

      if (numberOfDistinctItems <= 0) throw new System.ArgumentOutOfRangeException(nameof(numberOfDistinctItems));

      NumberOfDistinctItems = numberOfDistinctItems;

      System.ArgumentNullException.ThrowIfNull(weights);
      if (weights.Length <= 0 || weights.Length < NumberOfDistinctItems) throw new System.ArgumentOutOfRangeException(nameof(weights));

      Weights = weights;

      System.ArgumentNullException.ThrowIfNull(values);
      if (values.Length <= 0 || values.Length < NumberOfDistinctItems) throw new System.ArgumentOutOfRangeException(nameof(values));

      Values = values;
    }

    public int[,] ComputeRecursiveGrid(out int maxValue, bool zeroInitialValues = false)
    {
      var grid = new int[NumberOfDistinctItems + 1, WeightCapacity + 1];

      for (var i = 0; i <= NumberOfDistinctItems; i++)
        for (var j = 0; j <= WeightCapacity; j++)
          grid[i, j] = -1;

      maxValue = Recurse(NumberOfDistinctItems, WeightCapacity);

      if (zeroInitialValues) // Optionally zero out values that are still -1 after the recursive process.
        for (var i = 0; i <= NumberOfDistinctItems; i++)
          for (var j = 0; j <= WeightCapacity; j++)
            if (grid[i, j] == -1)
              grid[i, j] = 0;

      return grid;

      int Recurse(int i, int j)
      {
        if (i == 0 || j <= 0)
          return 0;

        if (grid[i - 1, j] < 0) // m[i-1,j] has not been calculated, so we do so now.
          grid[i - 1, j] = Recurse(i - 1, j);

        if (Weights[i - 1] > j) // Weight cannot fit in the bag.
          grid[i, j] = grid[i - 1, j];
        else
        {
          if (grid[i - 1, j - Weights[i - 1]] < 0) // m[i-1,j-w[i]] has not been calculated, so we do that now.
            grid[i - 1, j - Weights[i - 1]] = Recurse(i - 1, j - Weights[i - 1]);

          grid[i, j] = System.Math.Max(grid[i - 1, j], grid[i - 1, j - Weights[i - 1]] + Values[i - 1]);
        }

        return grid[i, j];
      }
    }

    public int[,] ComputeDynamicGrid(out int maxValue)
    {
      var grid = new int[NumberOfDistinctItems + 1, WeightCapacity + 1];

      for (int i = 0; i <= NumberOfDistinctItems; i++)
      {
        for (int j = 0; j <= WeightCapacity; j++)
        {
          if (i == 0 || j == 0)
            grid[i, j] = 0;
          else if (Weights[i - 1] <= j)
            grid[i, j] = System.Math.Max(Values[i - 1] + grid[i - 1, j - Weights[i - 1]], grid[i - 1, j]);
          else
            grid[i, j] = grid[i - 1, j];
        }
      }

      maxValue = grid[NumberOfDistinctItems, WeightCapacity];

      return grid;
    }
  }
}

/*
  var weights = new int[] { 23, 26, 20, 18, 32, 27, 29, 26, 30, 27 };
  var values = new int[] { 505, 352, 458, 220, 354, 414, 498, 545, 473, 543 };

  var ks = new Flux.Model.Knapsack(67, 10, weights, values);

  var dg = ks.ComputeDynamicGrid(out var dgMaxWorth);
  System.Console.WriteLine($"MaxWorth: {dgMaxWorth}");
  //System.Console.WriteLine(dg.ToConsoleBlock());
  for (var i = ks.NumberOfDistinctItems; i >= 0; i--)
  {
    for (var j = ks.WeightCapacity; j >= 0; j--)
      if (dg[i, j] is var value && value > 0)
        System.Console.Write($"({i},{j})={value},");

    System.Console.WriteLine();
  }

  System.Console.WriteLine();

  var rg = ks.ComputeRecursiveGrid(out var rgMaxWorth);
  System.Console.WriteLine($"MaxWorth: {rgMaxWorth}");
  //System.Console.WriteLine(rg.ToConsoleBlock());
  for (var i = ks.NumberOfDistinctItems; i >= 0; i--)
  {
    for (var j = ks.WeightCapacity; j >= 0; j--)
      if (rg[i, j] is var value && value > 0)
        System.Console.Write($"({i},{j})={value},");

    System.Console.WriteLine();
  }
*/
