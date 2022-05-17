namespace Flux
{
  /// <summary>An interface representing a quantifiable value.</summary>
  /// <typeparam name="TType">The value type.</typeparam>
  public interface IQuantifiable<TType>
    where TType : struct
  {
    /// <summary>The value of the quantity.</summary>
    /// <returns></returns>
    TType Value { get; }
  }
}
