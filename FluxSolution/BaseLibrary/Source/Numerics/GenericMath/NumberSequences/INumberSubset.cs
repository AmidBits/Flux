#if NET7_0_OR_GREATER
namespace Flux
{
  public interface INumberSubset<TNumber>
    where TNumber : System.Numerics.INumber<TNumber>
  {
    System.Collections.Generic.IEnumerable<TNumber> GetSubset(TNumber number);

    System.Collections.Generic.IEnumerable<TNumber> GetGapsInSubset(TNumber number, bool includeLastFirstGap)
     => GetSubset(number).PartitionTuple2(includeLastFirstGap, (leading, trailing, index) => trailing - leading);
  }
}
#endif