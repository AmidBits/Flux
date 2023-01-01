namespace Flux
{
  public interface INumberRoundable<TSelf, TBound>
    where TSelf : System.Numerics.INumber<TSelf>
    where TBound : System.Numerics.INumber<TBound>
  {
    TBound RoundNumber(TSelf value, RoundingMode mode);
  }
}
