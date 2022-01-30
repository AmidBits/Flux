namespace Flux
{
  public interface IValueSiBaseUnit<T>
    : IValueGeneralizedUnit<T>
  {
    /// <summary>The SI base unit value of the quantity.</summary>
    new T Value { get; }
  }
}
