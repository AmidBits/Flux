#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Specifies how mathematical rounding methods should process a number.</summary>
  public enum IntegerRounding
  {
    /// <summary>Rounds a number to an integer that is further away from zero.</summary>
    /// <remarks>This is either the typical Math.Floor (if less than zero) or Math.Ceiling (if greater than zero) of the number.</remarks>
    AwayFromZero = 101,
    /// <summary>Rounds a number to the next greater integer.</summary>
    /// <remarks>This is the same as Math.Ceiling of the number.</remarks>
    Ceiling = 102,
    /// <summary>Rounds a number to the next lower integer.</summary>
    /// <remarks>This is the same as Math.Floor of the number.</remarks>
    Floor = 103,
    /// <summary>Rounds a number to an integer that is closer to zero.</summary>
    /// <remarks>This is the same as Math.Truncate of the number.</remarks>
    TowardZero = 104,
  }
}
#endif
