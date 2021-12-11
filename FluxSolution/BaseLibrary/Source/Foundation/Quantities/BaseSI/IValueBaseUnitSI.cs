namespace Flux.Quantity
{
  public interface IValueBaseUnitSI<T>
  {
    /// <summary>The SI base unit value of the quantity.</summary>
    T BaseUnitValue { get; }
  }
}
