namespace Flux
{
  public interface IQuantifiable<TType>
  {
    /// <summary>The value of the quantity.</summary>
    TType Value { get; }
  }
}
