#if NET7_0_OR_GREATER
namespace Flux
{
  public sealed class BaseExponentValueCache
  {
    public System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>> m_lookup = new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>>();

    public System.Numerics.BigInteger this[System.Numerics.BigInteger x, System.Numerics.BigInteger n]
      => Search(x, n);

    public System.Numerics.BigInteger AddValue(System.Numerics.BigInteger x, System.Numerics.BigInteger n)
    {
      if (!m_lookup.TryGetValue(x, out var powerDictionary))
      {
        powerDictionary = new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>();

        powerDictionary[0] = 1;
        powerDictionary[1] = x;

        m_lookup[x] = powerDictionary;
      }

      if (!powerDictionary.TryGetValue(n, out var value))
      {
        var countPower = powerDictionary.Count - 1;
        value = powerDictionary.Max(kvp => kvp.Value);

        do
        {
          countPower++;
          value *= x;

          m_lookup[x][countPower] = value;
        }
        while (countPower < n);
      }

      return value;
    }
    public void Add(System.Numerics.BigInteger x, System.Numerics.BigInteger n)
      => AddValue(x, n);

    public bool Contains(System.Numerics.BigInteger x, System.Numerics.BigInteger n)
      => m_lookup.TryGetValue(x, out var powerDictionary) && powerDictionary.ContainsKey(n);

    public System.Numerics.BigInteger Search(System.Numerics.BigInteger x, System.Numerics.BigInteger n)
    {
      if (m_lookup.TryGetValue(x, out var powerDictionary) && powerDictionary.TryGetValue(n, out var value))
        return value;

      return AddValue(x, n);
    }
  }

  public static partial class GenericMath
  {
    private static BaseExponentValueCache m_integerPowEx = new BaseExponentValueCache();

    /// <summary>PREVIEW! Returns x raised to the power of n, using a caching system to speed up subsequent lookups.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IntegerPowEx<TSelf, TExponent>(this TSelf x, TExponent n)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
    {
      AssertRadix(x);
      AssertNonNegativeValue(n);

      return TSelf.CreateChecked(m_integerPowEx.Search(System.Numerics.BigInteger.CreateChecked(x), System.Numerics.BigInteger.CreateChecked(n)));
    }
  }
}
#endif
