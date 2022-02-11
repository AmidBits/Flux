namespace Flux
{
  public interface ISiBaseUnitQuantifiable<TType, TEnum>
    : IUnitQuantifiable<TType, TEnum>
    where TEnum : System.Enum
  {
    /// <summary>The SI base unit value of the quantity.</summary>
    new TType Value { get; }
  }
}
