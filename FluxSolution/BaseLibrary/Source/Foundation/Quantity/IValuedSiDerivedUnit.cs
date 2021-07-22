namespace Flux.Quantity
{
  public interface IValuedSiDerivedUnit
    : IValuedUnit
  {
    /// <summary>The SI derived unit value.</summary>
    new double Value { get; }
  }
}
