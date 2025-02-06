namespace Flux.Dsp.Samples
{
  /// <summary>Mono sample.</summary>
  public interface ISampleMono<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf Sample { get; }
  }
}
