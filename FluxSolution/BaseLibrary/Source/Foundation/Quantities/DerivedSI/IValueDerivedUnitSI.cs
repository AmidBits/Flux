namespace Flux
{
  public interface IValueDerivedUnitSI<T>
  {
    /// <summary>The SI derived unit value of the quantity.</summary>
    T DerivedUnitValue { get; }
  }
}
