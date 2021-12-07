namespace Flux.Quantity
{
  public interface IUnitValueDefaultable<T>
  {
    /// <summary>The default unit value.</summary>
    T DefaultUnitValue { get; }
  }
}
