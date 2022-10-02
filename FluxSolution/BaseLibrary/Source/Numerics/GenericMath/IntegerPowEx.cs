#if NET7_0_OR_GREATER
namespace Flux
{
  public sealed class RadixPowerValueCache
  {
    public System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>> m_lookup = new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>>();

    public System.Numerics.BigInteger this[System.Numerics.BigInteger radix, System.Numerics.BigInteger power]
      => Search(radix, power);

    public System.Numerics.BigInteger AddValue(System.Numerics.BigInteger radix, System.Numerics.BigInteger power)
    {
      if (!m_lookup.TryGetValue(radix, out var powerDictionary))
      {
        powerDictionary = new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>();

        powerDictionary[0] = 1;
        powerDictionary[1] = radix;

        m_lookup[radix] = powerDictionary;
      }

      if (!powerDictionary.TryGetValue(power, out var value))
      {
        var countPower = powerDictionary.Count - 1;
        value = powerDictionary.Max(kvp => kvp.Value);

        do
        {
          countPower++;
          value *= radix;

          m_lookup[radix][countPower] = value;
        }
        while (countPower < power);
      }

      return value;
    }
    public void Add(System.Numerics.BigInteger radix, System.Numerics.BigInteger power)
      => AddValue(radix, power);

    public bool Contains(System.Numerics.BigInteger radix, System.Numerics.BigInteger power)
      => m_lookup.TryGetValue(radix, out var powerDictionary) && powerDictionary.ContainsKey(power);

    public System.Numerics.BigInteger Search(System.Numerics.BigInteger radix, System.Numerics.BigInteger power)
    {
      if (m_lookup.TryGetValue(radix, out var powerDictionary) && powerDictionary.TryGetValue(power, out var value))
        return value;

      return AddValue(radix, power);
    }
  }

  public static partial class GenericMath
  {
    private static RadixPowerValueCache m_integerPowEx = new RadixPowerValueCache();

    /// <summary>PREVIEW! Returns x raised to the power of n, using a caching system to speed up subsequent lookups.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IntegerPowEx<TSelf, TExponent>(this TSelf radix, TExponent power)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
    {
      AssertRadix(radix);
      AssertNonNegativeValue(power);

      return TSelf.CreateChecked(m_integerPowEx.Search(System.Numerics.BigInteger.CreateChecked(radix), System.Numerics.BigInteger.CreateChecked(power)));
    }
  }
}
#endif
