namespace Flux
{
  public interface IValueSiDerivedUnit<T>
    : IValueGeneralizedUnit<T>
  {
    /// <summary>The SI derived unit value of the quantity.</summary>
    new T Value { get; }
  }
}
