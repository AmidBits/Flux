namespace Flux.Quantity
{
  public interface IValuedUnit<T>
  {
    /// <summary>The unit Value.</summary>
    T Value { get; }
  }

  public interface IValuedUnit
    : IValuedUnit<double>
  { }
}
