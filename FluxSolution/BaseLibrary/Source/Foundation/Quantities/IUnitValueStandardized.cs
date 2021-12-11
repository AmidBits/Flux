namespace Flux.Quantity
{
  public interface IUnitValueStandardized<T>
  {
    /// <summary>The standardized unit value. Standardized in this context means, more or less so.</summary>
    T StandardUnitValue { get; }
  }
}
