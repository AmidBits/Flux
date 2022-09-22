namespace Flux
{
  /// <summary>An interface representing a quantifiable value.</summary>
  /// <typeparam name="TType">The value type.</typeparam>
  public interface IQuantifiable<TType>
    where TType : struct, System.IEquatable<TType>//, System.Numerics.INumberBase<TType>
  {
    /// <summary>The value of the quantity.</summary>
    /// <returns>The quantity based on the default unit.</returns>
    TType Value { get; }
  }
}
