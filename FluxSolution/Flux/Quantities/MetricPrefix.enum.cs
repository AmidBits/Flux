namespace Flux
{
  /// <summary>
  /// <para>The MetricPrefix enum represents the SI metrix prefix decimal (base 10) multiples.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Metric_prefix#SI_prefixes_table"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/International_System_of_Units#Prefixes"/></para>
  /// </summary>
  [System.ComponentModel.DefaultValue(Unprefixed)]
  public enum MetricPrefix
  {
    /// <summary>Represents a value that is not a metric multiple. A.k.a. one.</summary>
    Unprefixed = 0,
    /// <summary></summary>
    Quetta = 30,
    /// <summary></summary>
    Ronna = 27,
    /// <summary>A.k.a. septillion/quadrillion.</summary>
    Yotta = 24,
    /// <summary>A.k.a. sextillion/trilliard.</summary>
    Zetta = 21,
    /// <summary>A.k.a. quintillion/trillion.</summary>
    Exa = 18,
    /// <summary>A.k.a. quadrillion/billiard.</summary>
    Peta = 15,
    /// <summary>A.k.a. trillion/billion.</summary>
    Tera = 12,
    /// <summary>A.k.a. billion/milliard.</summary>
    Giga = 9,
    /// <summary>A.k.a. million.</summary>
    Mega = 6,
    /// <summary>A.k.a. thousand.</summary>
    Kilo = 3,
    /// <summary>A.k.a. hundred.</summary>
    Hecto = 2,
    /// <summary>A.k.a. ten.</summary>
    Deca = 1,
    /// <summary>A.k.a. tenth.</summary>
    Deci = -1,
    /// <summary>A.k.a. hundredth.</summary>
    Centi = -2,
    /// <summary>A.k.a. thousandth.</summary>
    Milli = -3,
    /// <summary>A.k.a. millionth.</summary>
    Micro = -6,
    /// <summary>A.k.a. billionth/milliardth.</summary>
    Nano = -9,
    /// <summary>A.k.a. trillionth/billionth.</summary>
    Pico = -12,
    /// <summary>A.k.a. quadrillionth/billiardth.</summary>
    Femto = -15,
    /// <summary>A.k.a. quintillionth/trillionth.</summary>
    Atto = -18,
    /// <summary>A.k.a. sextillionth/trilliardth.</summary>
    Zepto = -21,
    /// <summary>A.k.a. septillionth/quadrillionth.</summary>
    Yocto = -24,
    /// <summary></summary>
    Ronto = -27,
    /// <summary></summary>
    Quecto = -30,
  }
}
