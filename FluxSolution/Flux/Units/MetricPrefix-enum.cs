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
    /// <summary>A.k.a. nonillion (short scale).</summary>
    Quetta = 30,
    /// <summary>A.k.a. octillion (short scale).</summary>
    Ronna = 27,
    /// <summary>A.k.a. septillion (short scale).</summary>
    Yotta = 24,
    /// <summary>A.k.a. sextillion (short scale).</summary>
    Zetta = 21,
    /// <summary>A.k.a. quintillion (short scale).</summary>
    Exa = 18,
    /// <summary>A.k.a. quadrillion (short scale).</summary>
    Peta = 15,
    /// <summary>A.k.a. trillion (short scale).</summary>
    Tera = 12,
    /// <summary>A.k.a. billion (short scale).</summary>
    Giga = 9,
    /// <summary>A.k.a. million (short scale).</summary>
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
    /// <summary>A.k.a. billionth.</summary>
    Nano = -9,
    /// <summary>A.k.a. trillionth.</summary>
    Pico = -12,
    /// <summary>A.k.a. quadrillionth.</summary>
    Femto = -15,
    /// <summary>A.k.a. quintillionth.</summary>
    Atto = -18,
    /// <summary>A.k.a. sextillionth.</summary>
    Zepto = -21,
    /// <summary>A.k.a. septillionth.</summary>
    Yocto = -24,
    /// <summary></summary>
    Ronto = -27,
    /// <summary></summary>
    Quecto = -30,
  }
}
