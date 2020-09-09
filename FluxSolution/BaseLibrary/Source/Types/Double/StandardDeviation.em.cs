namespace Flux
{
  public static partial class XtendDouble
  {
    /// <summary>Calculate the standard deviation (a measure that is used to quantify the amount of variation of a set of values) of a sequence.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Standard_deviation"/>
    public static double StandardDeviation(this System.Collections.Generic.IEnumerable<double> source)
      => System.Math.Sqrt(source.Variance().sampleVariance);
  }
}
