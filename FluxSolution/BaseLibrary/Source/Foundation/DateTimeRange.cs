namespace Flux
{
  public struct DateTimeRange
    : System.IEquatable<DateTimeRange>
  {
    public readonly static DateTimeRange Empty;

    private readonly System.DateTime m_start;
    private readonly System.DateTime m_end;

    public DateTimeRange(System.DateTime start, System.DateTime end)
    {
      if (end < start) throw new System.ArgumentException(@"Start must be before end.");

      m_start = start;
      m_end = end;
    }
    public DateTimeRange(System.DateTime start, System.TimeSpan duration)
      : this(start, start + duration)
    { }

    public System.DateTime Start
      => m_start;

    public System.DateTime End
      => m_end;

    public System.TimeSpan Duration
      => m_end - m_start;

    /// <summary>Split the range into sub-ranges.</summary>
    /// <param name="duration">The duration of each sub-range (except for the last sub-range, which may be shorter).</param>
    public System.Collections.Generic.IEnumerable<DateTimeRange> Split(System.TimeSpan duration)
    {
      for (System.DateTime subRangeStart = m_start; subRangeStart < m_end; subRangeStart += duration)
        yield return m_end - subRangeStart is var actualDuration && actualDuration <= duration ? new DateTimeRange(subRangeStart, duration) : new DateTimeRange(subRangeStart, actualDuration);
    }

    #region Static methods
    public static DateTimeRange Intersect(System.DateTime s1, System.DateTime e1, System.DateTime s2, System.DateTime e2)
      => s1 < e2 && s2 < e1 ? new DateTimeRange(s1 > s2 ? s1 : s2, e1 < e2 ? e1 : e2) : Empty;
    public static DateTimeRange Intersect(DateTimeRange a, DateTimeRange b)
      => Intersect(a.m_start, a.m_end, b.m_start, b.m_end);

    public static bool IsOverlapped(System.DateTime s1, System.DateTime e1, System.DateTime s2, System.DateTime e2)
      => s1 < e2 && s2 < e1;
    #endregion Static methods

    #region Implemented interfaces
    public bool Equals(DateTimeRange other)
      => m_start == other.m_start && m_end == other.m_end;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is DateTimeRange o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_start, m_end);
    public override string ToString()
      => $"<{GetType().Name}: {m_start}, {m_end}>";

    #endregion Object overrides
  }
}
