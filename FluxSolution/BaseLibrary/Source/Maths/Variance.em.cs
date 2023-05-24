//namespace Flux
//{
//  public static partial class ExtensionMethodsNumerics
//  {
//    /// <summary>Calculate the variance (how far a set of numbers is spread out) of a sequence, using the Welford's Online algorithm. Variance is the mean squared deviation. To compute the standard deviation, simply do the System.Math.Sqrt(variance).</summary>
//    /// <see cref="http://en.wikipedia.org/wiki/Variance"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Algorithms_for_calculating_variance"/>
//    public static (int count, TSelf mean, TSelf sampleVariance, TSelf populationVariance, TSelf standardDeviation) Variance<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
//      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
//    {
//      var count = 0;
//      TSelf mean = TSelf.Zero, M2 = TSelf.Zero;

//      foreach (var value in source ?? throw new System.ArgumentNullException(nameof(source)))
//      {
//        var delta1 = value - mean;
//        mean += delta1 / TSelf.CreateChecked(++count);

//        var delta2 = value - mean;
//        M2 += delta1 * delta2;
//      }

//      var c = TSelf.CreateChecked(count);

//      return (c > TSelf.One) ? (count, mean, M2 / (c - TSelf.One), M2 / c, TSelf.Sqrt(M2 / c)) : throw new System.ArgumentException(@"The sequence must contain at least two elements.");
//    }
//  }
//}
