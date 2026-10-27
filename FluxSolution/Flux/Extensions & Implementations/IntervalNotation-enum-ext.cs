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

  public static partial class IntervalNotationExtensions
  {
    extension(IntervalNotation notation)
    {
      /// <summary>
      /// <para>Inverts an <see cref="IntervalNotation"/>.</para>
      /// </summary>
      /// <returns>
      /// <para>The 'opposite' <see cref="IntervalNotation"/>, i.e.:</para>
      /// <list type="bullet">
      /// <item><see cref="IntervalNotation.Closed"/> => <see cref="IntervalNotation.Open"/> </item>
      /// <item><see cref="IntervalNotation.HalfOpenLeft"/> => <see cref="IntervalNotation.HalfOpenRight"/> </item>
      /// <item><see cref="IntervalNotation.HalfOpenRight"/> => <see cref="IntervalNotation.HalfOpenLeft"/> </item>
      /// <item><see cref="IntervalNotation.Open"/> => <see cref="IntervalNotation.Closed"/> </item>
      /// </list>
      /// </returns>
      /// <exception cref="NotImplementedException"></exception>
      public IntervalNotation InvertNotation()
        => (notation == IntervalNotation.Closed) ? IntervalNotation.Open
        : (notation == IntervalNotation.Open) ? IntervalNotation.Closed
        : (notation == IntervalNotation.HalfOpenLeft) ? IntervalNotation.HalfOpenRight
        : (notation == IntervalNotation.HalfOpenRight) ? IntervalNotation.HalfOpenLeft
        : throw new NotImplementedException(notation.ToString());

      /// <summary>
      /// <para>Gets a char representing the <see cref="IntervalNotation"/> (specified by <paramref name="notation"/>) on the left hand side.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="NotImplementedException"></exception>
      public char ToIntervalNotationCharLeft()
        => (notation is IntervalNotation.Closed or IntervalNotation.HalfOpenRight) ? '['
        : (notation is IntervalNotation.Open or IntervalNotation.HalfOpenLeft) ? '('
        : throw new NotImplementedException();

      /// <summary>
      /// <para>Gets a char representing the <see cref="IntervalNotation"/> (specified by <paramref name="notation"/>) on the right hand side.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="NotImplementedException"></exception>
      public char ToIntervalNotationCharRight()
        => (notation is IntervalNotation.Closed or IntervalNotation.HalfOpenLeft) ? ']'
        : (notation is IntervalNotation.Open or IntervalNotation.HalfOpenRight) ? ')'
        : throw new NotImplementedException();

      /// <summary>
      /// <para>Creates a string representing the <see cref="IntervalNotation"/> (specified by <paramref name="notation"/>) of the interval <paramref name="minValue"/>..<paramref name="maxValue"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      /// <exception cref="NotImplementedException"></exception>
      public string ToIntervalNotationString<T>(T minValue, T maxValue, string? format = null, System.IFormatProvider? provider = null)
      {
        format = $"{{0{(format is null ? string.Empty : $":{format}")}}}";

        return $"{notation.ToIntervalNotationCharLeft()}{string.Format(provider, format, minValue)}, {string.Format(provider, format, maxValue)}{notation.ToIntervalNotationCharRight()}";
      }
    }
  }
}
