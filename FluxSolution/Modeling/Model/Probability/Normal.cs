﻿namespace Flux.Probabilities
{
  public sealed class Normal
    : IDistribution<double>
  {
    public double Mean { get; }
    public double Sigma { get; }
    public double μ => Mean;
    public double σ => Sigma;

    public static readonly Normal Standard = Distribution(0, 1);

    public static Normal Distribution(double mean, double sigma)
      => new(mean, sigma);
    private Normal(double mean, double sigma)
    {
      this.Mean = mean;
      this.Sigma = sigma;
    }

    // Box-Muller method
    private static double StandardSample() => System.Math.Sqrt(-2.0 * double.Log(StandardContinuousUniform.Distribution.Sample())) * double.Cos(2.0 * double.Pi * StandardContinuousUniform.Distribution.Sample());

    public double Sample() => μ + σ * StandardSample();
  }
}
