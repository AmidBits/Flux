namespace Flux
{
  ///// <summary>An interface representing a quantifiable value.</summary>
  ///// <typeparam name="TType">The value type.</typeparam>
  //public interface IQuantifiable<TType>
  //  where TType : struct, System.IEquatable<TType>//, System.Numerics.INumberBase<TType>
  //{
  //  /// <summary>The value of the quantity.</summary>
  //  /// <returns>The quantity based on the default unit.</returns>
  //  TType Value { get; }
  //}

  /// <summary>An interface representing a quantifiable value.</summary>
  /// <typeparam name="TType">The value type.</typeparam>
  public interface IQuantifiable<TType>
#if NET7_0_OR_GREATER
    where TType : System.Numerics.INumber<TType>
#else
    where TType : struct
#endif
  {
    /// <summary>The value of the quantity.</summary>
    /// <returns>The quantity based on the default unit.</returns>
    TType Value { get; }
  }
}
