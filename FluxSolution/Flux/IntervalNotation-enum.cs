namespace Flux
{
  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public enum IntervalNotation
  {
    /// <summary>A closed interval is an interval which includes all its limit points, and is denoted with square brackets: [left, right]</summary>
    /// <remarks>This is the default interval notation.</remarks>
    Closed,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-left-open interval, i.e exclude the left endpoint: (left, right]</remarks>
    HalfLeftOpen,
    /// <summary>A half-open interval includes only one of its endpoints, and is denoted by mixing the notations for open and closed intervals.</summary>
    /// <remarks>This is a half-right-open interval, i.e exclude the right endpoint: [left, right)</remarks>
    HalfRightOpen,
    /// <summary>An open interval excludes both endpoints, and is indicated with parentheses: (left, right)</summary>
    Open,
  }
}
