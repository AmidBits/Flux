namespace Flux.Quantity
{
  public interface IValuedSiDerivedNamedUnit
    : IValuedUnit
  {
    /// <summary>The SI derived unit value.</summary>
    new double Value { get; }
  }
}
