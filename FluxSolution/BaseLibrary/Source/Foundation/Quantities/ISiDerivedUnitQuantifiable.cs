namespace Flux
{
  public interface ISiDerivedUnitQuantifiable<TType, TEnum>
    : IUnitQuantifiable<TType, TEnum>
    where TEnum : System.Enum
  {
    /// <summary>The SI derived unit value of the quantity.</summary>
    new TType Value { get; }
  }
}
