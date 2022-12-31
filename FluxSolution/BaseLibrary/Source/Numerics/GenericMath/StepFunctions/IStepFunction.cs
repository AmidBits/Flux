namespace Flux.Numerics
{
  public interface IStepFunction<TSelf, TResult>
    where TSelf : System.Numerics.INumber<TSelf>
    where TResult : System.Numerics.INumber<TResult>
  {
    TSelf ReferenceValue { get; }

    TResult LessThan { get; }
    TResult EqualTo { get; }
    TResult GreaterThan { get; }

    TResult Evaluate(TSelf x);
  }
}
