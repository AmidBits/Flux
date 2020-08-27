namespace Flux.Model
{
  /*
         var items = new Flux.Model.Knapsack.Item[] {
        new Flux.Model.Knapsack.Item(505, 23),
        new Flux.Model.Knapsack.Item(352, 26),
        new Flux.Model.Knapsack.Item(458, 20),
        new Flux.Model.Knapsack.Item(220, 18),
        new Flux.Model.Knapsack.Item(354, 32),
        new Flux.Model.Knapsack.Item(414, 27),
        new Flux.Model.Knapsack.Item(498, 29),
        new Flux.Model.Knapsack.Item(545, 26),
        new Flux.Model.Knapsack.Item(473, 30),
        new Flux.Model.Knapsack.Item(543, 27)
      };

      int[] value = { 10, 50, 70 };
      int[] weight = { 10, 20, 30 };
      int capacity = 40;
      int itemsCount = 7;

      var ks = new Flux.Model.Knapsack(67, items);

      System.Console.WriteLine(ks.Recurser(10, 67));
*/

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
  /// <summary>
  /// 
  /// </summary>
  /// <see cref="https://en.wikipedia.org/wiki/Knapsack_problem"/>
  public class Knapsack
  {
    public int WeightCapacity { get; set; }
    public Item[] Items;

    public int[,] Value;

    public Knapsack(int weightCapacity, Item[] items)
    {
      WeightCapacity = weightCapacity;
      Items = items;

      Value = new int[Items.Length + 1, WeightCapacity + 1];

      for (int i = 0; i < Value.GetLength(0); i++)
      {
        for (int j = 0; j < Value.GetLength(1); j++)
        {
          Value[i, j] = -1;
        }
      }
    }

    public int Recurser(int i, int j)
    {
      if (i == 0 || j <= 0)
        return 0;

      if (Value[i - 1, j] == -1)      //m[i-1, j] has not been calculated, we have to call function m
        Value[i - 1, j] = Recurser(i - 1, j);

      if (Items[i - 1].Weight > j)                      //item cannot fit in the bag (THIS WAS MISSING FROM THE PREVIOUS ALGORITHM)
        Value[i, j] = Value[i - 1, j];
      else
      {
        if (Value[i - 1, j - Items[i - 1].Weight] == -1)      //m[i-1,j-w[i]] has not been calculated, we have to call function m
          Value[i - 1, j - Items[i - 1].Weight] = Recurser(i - 1, j - Items[i - 1].Weight);

        Value[i, j] = System.Math.Max(Value[i - 1, j], Value[i - 1, j - Items[i - 1].Weight] + Items[i - 1].Value);
      }

      return Value[i, j];
    }

    public static void MaxValue2(int W, int[] w, int[] v, int n)
    {
      var value = new int[n + 1, W + 1];

      for (int i = 0; i <= value.GetLength(0); i++)
      {
        for (int j = 0; j <= value.GetLength(1); j++)
        {
          value[i, j] = -1;
        }
      }


    }

    public static int MaxValue(int weightCapacity, int[] weight, int[] value, int numberOfDistinctItems)
    {
      var K = new int[numberOfDistinctItems + 1, weightCapacity + 1];

      for (int i = 0; i <= numberOfDistinctItems; i++)
      {
        for (int w = 0; w <= weightCapacity; w++)
        {
          if (i == 0 || w == 0) K[i, w] = 0;
          else if (weight[i - 1] <= w) K[i, w] = System.Math.Max(value[i - 1] + K[i - 1, w - weight[i - 1]], K[i - 1, w]);
          else K[i, w] = K[i - 1, w];
        }
      }

      return K[numberOfDistinctItems, weightCapacity];
    }


    public class Item
    {
      public int Value;
      public int Weight;

      public Item(int value, int weight)
      {
        Value = value;
        Weight = weight;
      }
    }

    public static int MaxValue2(int weightCapacity, Item[] items)
    {
      var K = new int[items.Length + 1, weightCapacity + 1];

      for (int i = 0; i <= items.Length; i++)
      {
        for (int w = 0; w <= weightCapacity; w++)
        {
          if (i == 0 || w == 0) K[i, w] = 0;
          else if (items[i - 1].Weight <= w) K[i, w] = System.Math.Max(items[i - 1].Value + K[i - 1, w - items[i - 1].Weight], K[i - 1, w]);
          else K[i, w] = K[i - 1, w];
        }
      }

      return K[items.Length, weightCapacity];
    }
  }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
}
