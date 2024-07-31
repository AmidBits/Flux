namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Asserts that the <paramref name="value"/> is a member of the System.Numerics.IMinMaxValue in <paramref name="source"/> constrained by <paramref name="notation"/>. If not, it throws an exception.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertMember<TSelf>(this TSelf source, TSelf value, IntervalNotation notation, string? paramName = null)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => notation.AssertValidMember(value, TSelf.CreateChecked(source.MinValue), TSelf.CreateChecked(source.MaxValue), paramName ?? nameof(value));

    /// <summary>
    /// <para>Asserts that the <paramref name="value"/> is a member of the System.Numerics.IMinMaxValue in <paramref name="source"/> constrained by IntervalNotation.Closed enum value. If not, it throws an exception.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertMember<TSelf>(this TSelf source, TSelf value, string? paramName = null)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => source.AssertMember(value, IntervalNotation.Closed, paramName);

    /// <summary>
    /// <para>Returns whether the <paramref name="value"/> is a member of the System.Numerics.IMinMaxValue in <paramref name="source"/> constrained by <paramref name="notation"/>.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static bool VerifyMember<TSelf>(this TSelf source, TSelf value, IntervalNotation notation)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => notation.IsValidMember(value, TSelf.CreateChecked(source.MinValue), TSelf.CreateChecked(source.MaxValue));

    /// <summary>
    /// <para>Returns whether the <paramref name="value"/> is a member of the System.Numerics.IMinMaxValue in <paramref name="source"/> constrained by IntervalNotation.Closed enum value.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static bool VerifyMember<TSelf>(this TSelf source, TSelf value)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => source.VerifyMember(value, IntervalNotation.Closed);
  }
}
