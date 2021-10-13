namespace Flux.Dsp.AudioProcessor
{
  public enum MonoQuadraticMode
  {
    Bypass,
    /// <summary>Apply the quadratic exponent asymmetrically (both peeks will mathematically react differently to the exponent) to the signal.</summary>
    Asymmetric,
    /// <summary>Apply the quadratic exponent asymmetrically (both peeks will mathematically react differently to the exponent) to the also inverted signal.</summary>
    InvertedAsymmetric,
    /// <summary>Apply the quadratic exponent symmetrically (peeks reacts differently) to both peeks.</summary>
    Symmetric,
    /// <summary>Apply the quadratic exponent symmetrically to both peeks.</summary>
    SymmetricInverse
  }
}
