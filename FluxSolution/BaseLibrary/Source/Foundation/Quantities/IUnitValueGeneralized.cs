namespace Flux
{
  public interface IUnitValueGeneralized<T>
  {
    /// <summary>The standardized unit value. Standardized in this context means, more or less so.</summary>
    T GeneralUnitValue { get; }
  }
}
