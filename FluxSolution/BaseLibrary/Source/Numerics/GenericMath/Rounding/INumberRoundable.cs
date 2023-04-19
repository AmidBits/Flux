namespace Flux
{
#if NET7_0_OR_GREATER

  public interface INumberRoundable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf RoundNumber(TSelf value, RoundingMode mode);
  }

#else

  public interface INumberRoundable
  {
    double RoundNumber(double value, RoundingMode mode);
  }

#endif
}
