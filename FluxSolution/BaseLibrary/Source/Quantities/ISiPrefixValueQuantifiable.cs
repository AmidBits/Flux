namespace Flux
{
  #region Extension methods

  public static partial class Em
  {
    public static string ToSiFormattedString<TValue>(this TValue source, System.Globalization.CultureInfo? cultureInfo = null)
      where TValue : System.Numerics.INumber<TValue>
    {
      cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

      var nfi = (System.Globalization.NumberFormatInfo)cultureInfo.NumberFormat.Clone();
      nfi.NumberGroupSeparator = UnicodeSpacing.ThinSpace.ToSpacingString();

      return source.ToString("#,0.#", nfi);
    }

    public static System.Collections.Generic.Dictionary<(Quantities.MetricPrefix, TUnit), string> ToStringsOfSiPrefixes<TValue, TUnit>(this Quantities.ISiPrefixValueQuantifiable<TValue, TUnit> source, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      where TValue : struct, System.Numerics.INumber<TValue>
      where TUnit : System.Enum
    {
      var d = new System.Collections.Generic.Dictionary<(Quantities.MetricPrefix, TUnit), string>();

      foreach (Quantities.MetricPrefix mp in System.Enum.GetValues<Quantities.MetricPrefix>().OrderDescending())
        d.Add((mp, default(TUnit)!), source.ToSiPrefixValueSymbolString(mp, unitSpacing, preferUnicode));

      return d;
    }
  }

  #endregion // Extension methods

  namespace Quantities
  {
    /// <summary>
    /// <para>This is for quantities and units that follow the SI (International System of Units). This interface enables the International System of Units (SI) to be represented.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/International_System_of_Units"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TUnit"></typeparam>
    /// <remarks>
    /// <para>If use of <see cref="System.IConvertible"/> is desirable, use the return value from <see cref="GetSiPrefixValue(MetricPrefix)"/> as a parameter for such functionality.</para>
    /// <para>No <see cref="ISiPrefixValueQuantifiable{TValue, TUnit}"/>.GetSiPrefixName() exists. There are only enum labels, no modifiers, e.g. plural, etc. Try <see cref="MetricPrefix"/>.GetUnitName() instead.</para>
    /// </remarks>
    public interface ISiPrefixValueQuantifiable<TValue, TUnit>
      : IUnitValueQuantifiable<TValue, TUnit>
      where TValue : System.Numerics.INumber<TValue>
      where TUnit : System.Enum
    {
      /// <summary>
      /// <para>Gets the name of the <paramref name="prefix"/> with the <typeparamref name="TUnit"/> and whether to <paramref name="preferPlural"/>.</para>
      /// </summary>
      /// <param name="prefix"></param>
      /// <param name="preferPlural"></param>
      /// <returns></returns>
      string GetSiPrefixName(MetricPrefix prefix, bool preferPlural);

      /// <summary>
      /// <para>Gets the symbol of the <paramref name="prefix"/> with the <typeparamref name="TUnit"/> and whether to <paramref name="preferUnicode"/>.</para>
      /// </summary>
      /// <param name="prefix">The prefix to project.</param>
      /// <param name="preferUnicode"></param>
      /// <returns></returns>
      string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode);

      /// <summary>
      /// <para>Gets the value of the quantity for the specified <paramref name="prefix"/>.</para>
      /// </summary>
      /// <param name="prefix">The prefix to project.</param>
      /// <returns></returns>
      TValue GetSiPrefixValue(MetricPrefix prefix);

      /// <summary>
      /// <para>Creates a new string with the name of the SI quantity for the <paramref name="prefix"/>, in the <paramref name="format"/> using the <paramref name="formatProvider"/>, <paramref name="unitSpacing"/> and whether to <paramref name="preferPlural"/>.</para>
      /// </summary>
      /// <param name="prefix"></param>
      /// <param name="format"></param>
      /// <param name="formatProvider"></param>
      /// <param name="unitSpacing"></param>
      /// <param name="preferPlural"></param>
      /// <returns></returns>
      string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing, bool preferPlural);
      //=> GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

      /// <summary>
      /// <para>Creates a new string with the symbol of the SI quantity for the <paramref name="prefix"/>, in the <paramref name="format"/> using the <paramref name="formatProvider"/>, <paramref name="unitSpacing"/> and whether to <paramref name="preferUnicode"/>.</para>
      /// </summary>
      /// <param name="prefix">The prefix to project.</param>
      /// <param name="format"></param>
      /// <param name="formatProvider"></param>
      /// <param name="unitSpacing"></param>
      /// <param name="preferUnicode"></param>
      /// <returns></returns>
      string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing, bool preferUnicode);
      //=> GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);
    }
  }
}
