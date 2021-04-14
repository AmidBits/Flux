namespace Flux.Model
{
  /// <summary>
  /// 
  /// </summary>
  /// <see cref="https://en.wikipedia.org/wiki/Knapsack_problem"/>
  public static class Knapsack
	{
		public static int[,] ComputeRecursiveGrid(int weightCapacity, int[] weights, int[] values, int numberOfDistinctItems, out double maxValue)
		{
			if (weights is null) throw new System.ArgumentNullException(nameof(weights));
			if (weights.Length <= 0 || weights.Length < numberOfDistinctItems) throw new System.ArgumentOutOfRangeException(nameof(weights));

			if (values is null) throw new System.ArgumentNullException(nameof(values));
			if (values.Length <= 0 || values.Length < numberOfDistinctItems) throw new System.ArgumentOutOfRangeException(nameof(values));

			if (numberOfDistinctItems <= 0) throw new System.ArgumentOutOfRangeException(nameof(numberOfDistinctItems));

			var grid = new int[numberOfDistinctItems + 1, weightCapacity + 1];

			for (int i = 0; i <= numberOfDistinctItems; i++)
				for (int j = 0; j <= weightCapacity; j++)
					grid[i, j] = -1;

			maxValue = Recurse(numberOfDistinctItems, weightCapacity);

			return grid;

			int Recurse(int i, int j)
			{
				if (i == 0 || j <= 0)
					return 0;

				if (grid[i - 1, j] < 0) // m[i-1, j] has not been calculated, so we do so now.
					grid[i - 1, j] = Recurse(i - 1, j);

				if (weights[i - 1] > j) // Weight cannot fit in the bag.
					grid[i, j] = grid[i - 1, j];
				else
				{
					if (grid[i - 1, j - weights[i - 1]] < 0) // m[i-1,j-w[i]] has not been calculated, so we do that now.
						grid[i - 1, j - weights[i - 1]] = Recurse(i - 1, j - weights[i - 1]);

					grid[i, j] = System.Math.Max(grid[i - 1, j], grid[i - 1, j - weights[i - 1]] + values[i - 1]);
				}

				return grid[i, j];
			}
		}

		public static int[,] ComputeDynamicGrid(int weightCapacity, int[] weights, int[] values, int numberOfDistinctItems, out int maxValue)
		{
			if (weights is null) throw new System.ArgumentNullException(nameof(weights));
			if (weights.Length <= 0 || weights.Length < numberOfDistinctItems) throw new System.ArgumentOutOfRangeException(nameof(weights));

			if (values is null) throw new System.ArgumentNullException(nameof(values));
			if (values.Length <= 0 || values.Length < numberOfDistinctItems) throw new System.ArgumentOutOfRangeException(nameof(values));

			if (numberOfDistinctItems <= 0) throw new System.ArgumentOutOfRangeException(nameof(numberOfDistinctItems));

			var grid = new int[numberOfDistinctItems + 1, weightCapacity + 1];

			for (int i = 0; i <= numberOfDistinctItems; i++)
			{
				for (int j = 0; j <= weightCapacity; j++)
				{
					if (i == 0 || j == 0) grid[i, j] = 0;
					else if (weights[i - 1] <= j) grid[i, j] = System.Math.Max(values[i - 1] + grid[i - 1, j - weights[i - 1]], grid[i - 1, j]);
					else grid[i, j] = grid[i - 1, j];
				}
			}

			maxValue = grid[numberOfDistinctItems, weightCapacity];

			return grid;
		}
	}
}

/*
  var weights = new int[] { 23, 26, 20, 18, 32, 27, 29, 26, 30, 27 };
  var values = new int[] { 505, 352, 458, 220, 354, 414, 498, 545, 473, 543 };

  var dg = Flux.Model.Knapsack.ComputeDynamicGrid(67, weights, values, 10, out var dgMaxWorth);

  for (var i = 10; i >= 0; i--)
  {
    for (var j = 67; j >= 0; j--)
      System.Console.Write($"m({i},{j})={dg[i, j]},");

    System.Console.WriteLine();
  }

  var rg = Flux.Model.Knapsack.ComputeRecursiveGrid(67, weights, values, 10, out var rgMaxWorth);

  for (var i = 10; i >= 0; i--)
  {
    for (var j = 67; j >= 0; j--)
      if (rg[i, j] is var value && value > 0)
        System.Console.Write($"m({i},{j})={value},");

    System.Console.WriteLine();
  }
*/
