namespace Flux.Numerics
{
  public abstract record class ANumberSequenceable<T>
    : INumberSequenceable<T>
    where T : System.Numerics.INumber<T>
  {
    #region Implemented interfaces
    [System.Diagnostics.Contracts.Pure]
    public abstract System.Collections.Generic.IEnumerable<T> GetNumberSequence();

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
      => GetNumberSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
