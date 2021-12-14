namespace Flux
{
  public interface IValueSiBaseUnit<T>
  {
    /// <summary>The SI base unit value of the quantity.</summary>
    T Value { get; }
  }
}
