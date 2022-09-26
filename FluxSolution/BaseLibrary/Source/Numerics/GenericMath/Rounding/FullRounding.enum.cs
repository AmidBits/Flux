#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>PREVIEW! Specifies the strategy to use when processing a number between two numbers.</summary>
  public enum FullRounding
  {
    /// <summary>Round to the number that is further from zero.</summary>
    /// <remarks>This is either the typical Math.Floor (if less than zero) or Math.Ceiling (if greater than zero) of the number.</remarks>
    AwayFromZero = 101,
    /// <summary>Round (up) to the number that is greater.</summary>
    /// <remarks>This is the same as Math.Ceiling of the number.</remarks>
    Ceiling = 102,
    /// <summary>Round (down) to the number that is less than.</summary>
    /// <remarks>This is the same as Math.Floor of the number.</remarks>
    Floor = 103,
    /// <summary>Round to the number that is closer to zero.</summary>
    /// <remarks>This is the same as Math.Truncate of the number.</remarks>
    TowardZero = 104,
  }
}
#endif
