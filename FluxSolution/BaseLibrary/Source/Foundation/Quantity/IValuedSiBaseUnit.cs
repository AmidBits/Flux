namespace Flux.Quantity
{
  public interface IValuedSiBaseUnit
    : IValuedUnit
  {
    /// <summary>The SI base unit value.</summary>
    new double Value { get; }
  }
}
