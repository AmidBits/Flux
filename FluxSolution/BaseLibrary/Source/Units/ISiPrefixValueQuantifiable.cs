﻿namespace Flux.Quantities
{
  #region Extension methods

  public static partial class Em
  {
    public static System.Collections.Generic.Dictionary<(MetricPrefix, TUnit), string> ToStringsOfMetricPrefixes<TValue, TUnit>(this ISiPrefixValueQuantifiable<TValue, TUnit> source, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      where TValue : struct, System.Numerics.INumber<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<(MetricPrefix, TUnit), string>();

      foreach (MetricPrefix mp in System.Enum.GetValues<MetricPrefix>().OrderDescending())
        d.Add((mp, source.UnprefixedUnit), source.ToSiPrefixValueString(mp, format, formatProvider, unitSpacing, preferUnicode, useFullName));

      return d;
    }
  }

  #endregion // Extension methods

  /// <summary>
  /// <para>This is for quantities and units that follow the SI (International System of Units). This interface enables the International System of Units (SI) to be represented.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/International_System_of_Units"/></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  /// <typeparam name="TUnit"></typeparam>
  /// <remarks>
  /// <para>If use of <see cref="System.IConvertible"/> is desirable, use the return value from <see cref="GetSiPrefixValue(MetricPrefix)"/> as a parameter for such functionality.</para>
  /// </remarks>
  public interface ISiPrefixValueQuantifiable<TValue, TUnit>
    : IUnitValueQuantifiable<TValue, TUnit>
    where TValue : System.Numerics.INumber<TValue>
    where TUnit : System.Enum
  {
    /// <summary>The metric base unit for <typeparamref name="TUnit"/>.</summary>
    TUnit BaseUnit { get; }

    /// <summary>The metric unprefixed unit for <typeparamref name="TUnit"/>.</summary>
    TUnit UnprefixedUnit { get; }

    /// <summary>
    /// <para>Returns the symbol of the specified metric <paramref name="prefix"/> and <paramref name="preferUnicode"/>/<paramref name="useFullName"/>.</para>
    /// </summary>
    /// <param name="prefix">The prefix to project.</param>
    /// <param name="preferUnicode"></param>
    /// <param name="useFullName"></param>
    /// <returns></returns>
    string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName);

    /// <summary>
    /// <para>Gets the value of the quantity in the <see cref="MetricPrefix"/> multiplicable specified by <paramref name="prefix"/>.</para>
    /// </summary>
    /// <param name="prefix">The prefix to project.</param>
    /// <returns></returns>
    TValue GetSiPrefixValue(MetricPrefix prefix);

    /// <summary>
    /// <para>Creates an SI quantity string specified by <paramref name="prefix"/>, <paramref name="format"/>, <paramref name="formatProvider"/>, <paramref name="unitSpacing"/>, <paramref name="preferUnicode"/> and <paramref name="useFullName"/>.</para>
    /// </summary>
    /// <param name="prefix">The prefix to project.</param>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <param name="unitSpacing"></param>
    /// <param name="preferUnicode"></param>
    /// <param name="useFullName"></param>
    /// <returns></returns>
    string ToSiPrefixValueString(MetricPrefix prefix, string? format, System.IFormatProvider? formatProvider, UnicodeSpacing unitSpacing, bool preferUnicode, bool useFullName);
  }
}