namespace Flux
{
  public enum IntervalConstraint
  {
    /// <summary>A closed interval is an interval which includes all its limit points, and is denoted with square brackets: "[ low , high ]"</summary>
    Closed,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-open interval on the low, or left side value: "( low , high ]"</remarks>
    HalfOpenLeft,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-open interval on the high, or right side value: "[ low , high )"</remarks>
    HalfOpenRight,
    /// <summary>An open interval does not include its endpoints, and is indicated with parentheses: "( low , high )"</summary>
    Open,
  }
}