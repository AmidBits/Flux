using System.Linq;

namespace Flux.NumberSequences
{
  public record class MöbiusNumber
    : INumericSequence<System.Numerics.BigInteger>
  {
    private readonly int m_maxValue;
    private readonly sbyte[] m_sieve;

    // Create a sieve using a mobius function.
    public MöbiusNumber(int maxValue)
    {
      if (maxValue < 0 || maxValue >= int.MaxValue) throw new System.ArgumentOutOfRangeException(nameof(maxValue));

      m_maxValue = maxValue;
      m_sieve = new sbyte[m_maxValue + 1];

      for (var i = 0; i < m_maxValue; i++)
        m_sieve[i] = 1;

      var sqrt = (int)System.Math.Sqrt(m_maxValue);

      for (var i = 2; i <= sqrt; i++)
      {
        if (m_sieve[i] == 1)
        {
          for (var j = i; j <= m_maxValue; j += i) // For each factor found, swap + and -.
            m_sieve[j] = (sbyte)(m_sieve[j] * -i);

          for (var j = i * i; j <= m_maxValue; j += i * i) // Square factor = 0.
            m_sieve[j] = 0;
        }
      }

      for (var i = 2; i <= m_maxValue; i++)
      {
        if (m_sieve[i] == i)
          m_sieve[i] = 1;
        else if (m_sieve[i] == -i)
          m_sieve[i] = -1;
        else if (m_sieve[i] < 0)
          m_sieve[i] = 1;
        else if (m_sieve[i] > 0)
          m_sieve[i] = -1;
      }
    }
    public MöbiusNumber()
      : this((2147483591 - 1) / 1000)
    {
    }

    private System.Numerics.BigInteger GetMöbiusNumber(int number)
      => System.Numerics.BigInteger.CreateChecked(m_sieve[number]);

    #region Implemented interfaces
    // INumberSequence

    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      => System.Linq.Enumerable.Range(1, int.MaxValue - 1).Select(i => GetMöbiusNumber(i));


    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
