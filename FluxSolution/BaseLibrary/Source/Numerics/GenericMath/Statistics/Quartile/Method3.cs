namespace Flux.Quartiles
{
  /// <summary>
  /// <para>This method is not implemented at this time.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_3"/></para>
  /// </summary>
  public record class Method3
    : IQuartileComputable
  {
    public (TSelf q1, TSelf q2, TSelf q3) ComputeQuartiles<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      Compute(sample, out var q1, out var q2, out var q3);

      return (q1, q2, q3);
    }

    public static void Compute<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, out TSelf q1, out TSelf q2, out TSelf q3)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => throw new System.NotImplementedException();
  }
}
