namespace Flux
{
  public interface IValueGeneralizedUnit<T>
  {
    /// <summary>The standardized unit value. Standardized in this context means, more or less so.</summary>
    T Value { get; }
  }
}
