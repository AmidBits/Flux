namespace Flux
{
  /// <summary>Specifies the strategy that mathematical rounding should process a number.</summary>
  public enum FullRoundingBehavior
  {
    /// <summary>Rounds a number to an integer that is further away from zero.</summary>
    AwayFromZero = 101,
    /// <summary>Rounds a number to the next greater integer.</summary>
    Ceiling = 102,
    /// <summary>Rounds a number to the next lower integer.</summary>
    Floor = 103,
    /// <summary>Rounds a number to an integer that is closer to zero.</summary>
    TowardZero = 104,
  }
}
