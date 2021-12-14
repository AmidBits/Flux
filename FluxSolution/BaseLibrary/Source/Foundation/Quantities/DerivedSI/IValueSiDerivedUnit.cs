namespace Flux
{
  public interface IValueSiDerivedUnit<T>
  {
    /// <summary>The SI derived unit value of the quantity.</summary>
    T Value { get; }
  }
}
