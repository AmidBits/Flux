namespace Flux
{
  /// <summary>
  /// <para>Classifies a count by its divisibility by two.</para>
  /// </summary>
  public enum Parity
  {
    /// <summary>
    /// <para>Contains an even number of elements, units, or occurrences. A complete pairing is possible with nothing left over.</para>
    /// <para>Congruent to 0 modulo 2.</para>
    /// </summary>
    Even = 0,
    /// <summary>
    /// <para>Contains an odd number of elements, units, or occurrences. Pairing leaves exactly one unpaired remainder.</para>
    /// <para>Congruent to 1 modulo 2.</para>
    /// </summary>
    Odd = 1
  }
}
