namespace Flux
{
  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public enum IntervalNotation
  {
    /// <summary>A closed interval is an interval that includes all its endpoints and is denoted with square brackets: [left, right]</summary>
    /// <remarks>This is the default interval notation.</remarks>
    Closed,
    /// <summary>A half-open interval has two endpoints and includes only one of them. A left-open interval excludes the left endpoint: (left, right]</summary>
    HalfOpenLeft,
    /// <summary>A half-open interval has two endpoints and includes only one of them. A right-open interval excludes the right endpoint: [left, right)</summary>
    HalfOpenRight,
    /// <summary>An open interval does not include any endpoint, and is indicated with parentheses: (left, right)</summary>
    Open,
  }
}
