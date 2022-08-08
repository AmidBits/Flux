#if INumber
namespace Flux
{

  public interface IBinaryInteger<TSelf>
    where TSelf : IBinaryInteger<TSelf>
  {
    static abstract (TSelf Quotient, TSelf Remainder) DivRem(TSelf left, TSelf right);
  }

  public interface INumberBase<TSelf>
    where TSelf : INumberBase<TSelf>
  {
    static abstract TSelf One { get; }
    static abstract TSelf Zero { get; }

    static abstract TSelf Abs(TSelf value);
  }

  public interface IAdditionOperators<TSelf, TOther, TResult>
    where TSelf : IAdditionOperators<TSelf, TOther, TResult>
  {
    static abstract TResult operator +(TSelf left, TOther right);
  }

  public interface IComparisonOperators<TSelf>
  where TSelf : IComparisonOperators<TSelf>
  {
    static abstract bool operator >(TSelf left, TSelf right);
    static abstract bool operator >=(TSelf left, TSelf right);
    static abstract bool operator <(TSelf left, TSelf right);
    static abstract bool operator <=(TSelf left, TSelf right);
  }

  public interface IDivisionOperators<TSelf, TOther, TResult>
    where TSelf : IDivisionOperators<TSelf, TOther, TResult>
  {
    static abstract TResult operator /(TSelf left, TOther right);
  }

  public interface IEqualityOperators<TSelf, TOther> : IEquatable<TOther>
    where TSelf : IEqualityOperators<TSelf, TOther>
  {
    static abstract bool operator ==(TSelf left, TOther right);
    static abstract bool operator !=(TSelf left, TOther right);
  }

  public interface IModulusOperators<TSelf, TOther, TResult>
    where TSelf : IModulusOperators<TSelf, TOther, TResult>
  {
    static abstract TResult operator %(TSelf left, TOther right);
  }

  public interface IMultiplicationOperators<TSelf, TOther, TResult>
  where TSelf : IMultiplicationOperators<TSelf, TOther, TResult>
  {
    static abstract TResult operator *(TSelf left, TOther right);
  }

  public interface IPowerFunctions<TSelf, TOther, TResult>
    where TSelf : IPowerFunctions<TSelf, TOther, TResult>
  {
    static abstract TResult Pow(TSelf left, TOther right);
  }

  public interface ISubtractionOperators<TSelf, TOther, TResult>
    where TSelf : ISubtractionOperators<TSelf, TOther, TResult>
  {
    static abstract TResult operator -(TSelf left, TOther right);
  }

  public interface INumber<TSelf>
    : IBinaryInteger<TSelf>
    , INumberBase<TSelf>
    , IAdditionOperators<TSelf, TSelf, TSelf>
    , IComparisonOperators<TSelf>
    , IDivisionOperators<TSelf, TSelf, TSelf>
    , IEqualityOperators<TSelf, TSelf>
    , IModulusOperators<TSelf, TSelf, TSelf>
    , IMultiplicationOperators<TSelf, TSelf, TSelf>
    , IPowerFunctions<TSelf, TSelf, TSelf>
    , ISubtractionOperators<TSelf, TSelf, TSelf>
    where TSelf : INumber<TSelf>
  {
  }

}
#endif
