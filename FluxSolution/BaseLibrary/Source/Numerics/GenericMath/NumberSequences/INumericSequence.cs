namespace Flux
{
  public interface INumericSequence<TNumber>
    : System.Collections.Generic.IEnumerable<TNumber>
    where TNumber : System.Numerics.INumber<TNumber>
  {
    System.Collections.Generic.IEnumerable<TNumber> GetSequence();

    System.Collections.Generic.IEnumerable<TNumber> GetGapsInSequence(bool includeLastFirstGap)
     => GetSequence().PartitionTuple2(includeLastFirstGap, (leading, trailing, index) => trailing - leading);
  }
}
