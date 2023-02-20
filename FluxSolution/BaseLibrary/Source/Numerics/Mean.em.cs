namespace Flux
{
  public static partial class ExtensionMethodsNumerics
  {
    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="mean"></param>
    /// <param name="median"></param>
    /// <param name="mode"></param>
    /// <param name="sum"></param>
    /// <param name="count"></param>
    //public static void ComputeBasicStatistics<TSelf, TResult>(this System.Collections.Generic.IEnumerable<TSelf> source, out TResult mean, out TResult median, out TResult mode, out TResult sum, out int count)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  where TResult : System.Numerics.IFloatingPoint<TResult>
    //{
    //  mean = TResult.Zero;
    //  median = TResult.Zero;
    //  mode = TResult.Zero;

    //  sum = TResult.Zero;

    //  var list = source.ToList();
    //  list.Sort();

    //  count = list.Count;

    //  var dictionary = new Dictionary<TSelf, int>();

    //  for (var index = 0; index < list.Count; index++)
    //  {
    //    var value = list[index];

    //    if (!dictionary.TryGetValue(value, out var valueCount))
    //      valueCount = 0;

    //    dictionary[value] = valueCount + 1;

    //    sum += TResult.CreateChecked(value);
    //  }

    //  mean = sum / TResult.CreateChecked(list.Count);

    //  var halfIndex = list.Count / 2;

    //  median = int.IsEvenInteger(list.Count) ? (TResult.CreateChecked(list.ElementAt(halfIndex)) + TResult.CreateChecked(list.ElementAt(halfIndex - 1))).Divide(2) : TResult.CreateChecked(list.ElementAt(halfIndex));

    //  mode = TResult.CreateChecked(dictionary.FirstOrDefault(x => x.Value.Equals(dictionary.Values.Max())).Key);
    //}

    /// <summary>
    /// <para>Calculate the mean of a sequence, also return the count and the sum of values in the sequence as output parameters.</para>
    /// <see href="http://en.wikipedia.org/wiki/Mean"/>
    /// </summary>
    public static void Mean<TSelf, TResult>(this System.Collections.Generic.IEnumerable<TSelf> source, out int count, out TSelf sum, out TResult mean)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      count = 0;
      sum = TSelf.Zero;

      foreach (var item in source)
      {
        count++;
        sum += TSelf.CreateChecked(item);
      }

      mean = count > 0 ? TResult.CreateChecked(sum) / TResult.CreateChecked(count) : TResult.Zero;
    }

    /// <summary>
    /// <para>Calculate the mean of a sequence, also return the sum and a list of the values in the sequence as output parameters.</para>
    /// <see href="http://en.wikipedia.org/wiki/Mean"/>
    /// </summary>
    public static void Mean<TSelf, TResult>(this System.Collections.Generic.IEnumerable<TSelf> source, out System.Collections.Generic.List<TSelf> values, out TSelf sum, out TResult mean)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      values = source.ToList();
      sum = values.Sum();

      mean = values.Any() ? TResult.CreateChecked(sum) / TResult.CreateChecked(values.Count) : TResult.Zero;
    }
  }
}
