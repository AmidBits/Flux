namespace Flux.Probability
{
  public sealed class StandardContinuousUniform 
    : IDistribution<double>
  {
    public static readonly StandardContinuousUniform Distribution = new();

    private StandardContinuousUniform() { }

    public double Sample() => Pseudorandom.NextDouble();
  }
}
