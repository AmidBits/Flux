namespace Flux.RandomNumberGenerators
{
  /// <summary>A 32-bit random number generator.</summary>
  /// <see href="https://en.wikipedia.org/wiki/ISAAC_(cipher)"/>
  /// <seealso cref="http://burtleburtle.net/bob/rand/isaacafa.html"/>
  /// <seealso cref="http://rosettacode.org/wiki/The_ISAAC_Cipher#C.23"/>
  public class Isaac32
    : ASystemRandom32
  {
    private static readonly System.Threading.ThreadLocal<System.Random> m_shared = new(() => new Isaac32());
    new public static System.Random Shared => m_shared.Value!;

    public Isaac32(string seed) => Seed(seed, true);

    [System.CLSCompliant(false)]
    public Isaac32(ulong seed) : this(seed.ToString("X8", System.Globalization.CultureInfo.InvariantCulture)) { }
    public Isaac32(long seed) : this(unchecked((ulong)seed)) { }
    public Isaac32() : this((ulong)System.Diagnostics.Stopwatch.GetTimestamp()) { }

    #region Isaac

    private uint m_count;
    private readonly uint[] m_rsl = new uint[256];
    private readonly uint[] m_mem = new uint[256];
    private uint m_a;
    private uint m_b;
    private uint m_c;

    internal void Seed(string seed, bool flag)
    {
      System.ArgumentNullException.ThrowIfNull(seed);

      System.Array.Clear(m_mem);
      System.Array.Clear(m_rsl);

      for (var i = 0; i < seed.Length; i++)
        m_rsl[i] = seed[i];

      Initialize(flag);
    }

    private void ReIsaac()
    {
      uint x, y;

      m_c++;
      m_b = unchecked(m_b + m_c);

      for (var i = 0U; i <= 255; i++)
      {
        x = m_mem[i];

        switch (i & 3)
        {
          case 0: m_a ^= (m_a << 13); break;
          case 1: m_a ^= (m_a >> 6); break;
          case 2: m_a ^= (m_a << 2); break;
          case 3: m_a ^= (m_a >> 16); break;
        }

        m_a = unchecked(m_mem[(i + 128) & 255] + m_a);
        y = unchecked(m_mem[(x >> 2) & 255] + m_a + m_b);
        m_mem[i] = y;

        m_b = unchecked(m_mem[(y >> 10) & 255] + x);
        m_rsl[i] = m_b;
      }
    }

    private static void Mix(ref uint a, ref uint b, ref uint c, ref uint d, ref uint e, ref uint f, ref uint g, ref uint h)
    {
      unchecked
      {
        a ^= b << 11; d += a; b += c;
        b ^= c >> 2; e += b; c += d;
        c ^= d << 8; f += c; d += e;
        d ^= e >> 16; g += d; e += f;
        e ^= f << 10; h += e; f += g;
        f ^= g >> 4; a += f; g += h;
        g ^= h << 8; b += g; h += a;
        h ^= a >> 9; c += h; a += b;
      }
    }

    private void Initialize(bool flag)
    {
      uint a, b, c, d, e, f, g, h;

      m_a = m_b = m_c = 0;

      a = b = c = d = e = f = g = h = 0x9e3779b9;

      for (var i = 0; i <= 3; i++) // scramble it 
        Mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

      for (var i = 0; i < 256; i += 8) // fill in _randmem[] with messy stuff
      {
        if (flag)
        {
          a += m_rsl[i]; b += m_rsl[i + 1]; c += m_rsl[i + 2]; d += m_rsl[i + 3];
          e += m_rsl[i + 4]; f += m_rsl[i + 5]; g += m_rsl[i + 6]; h += m_rsl[i + 7];
        }

        Mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

        m_mem[i] = a; m_mem[i + 1] = b; m_mem[i + 2] = c; m_mem[i + 3] = d;
        m_mem[i + 4] = e; m_mem[i + 5] = f; m_mem[i + 6] = g; m_mem[i + 7] = h;
      }

      if (flag)
      {
        for (var i = 0; i < 256; i += 8) // do a second pass to make all of the seed affect all of mm 
        {
          unchecked
          {
            a += m_mem[i]; b += m_mem[i + 1]; c += m_mem[i + 2]; d += m_mem[i + 3];
            e += m_mem[i + 4]; f += m_mem[i + 5]; g += m_mem[i + 6]; h += m_mem[i + 7];

            Mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

            m_mem[i] = a; m_mem[i + 1] = b; m_mem[i + 2] = c; m_mem[i + 3] = d;
            m_mem[i + 4] = e; m_mem[i + 5] = f; m_mem[i + 6] = g; m_mem[i + 7] = h;
          }
        }
      }

      ReIsaac();

      m_count = 0;
    }

    #endregion

    #region Implemented interfaces

    internal override uint SampleUInt32()
    {
      var result = m_rsl[m_count];

      if (++m_count > 255)
      {
        ReIsaac();

        m_count = 0;
      }

      return result;
    }

    #endregion
  }

  //  public record class Iisac64
  //  {
  //    ulong[] result = new ulong[256];
  //    /** Internal field. */
  //    ulong[] mem = new ulong[256];
  //    /** Internal field. */
  //    ulong a;
  //    /** Internal field. */
  //    ulong b;
  //    /** Internal field. */
  //    ulong c;
  //    /**
  //     * Index of the next value to output in the stream.
  //     *
  //     * Note: this value could be a uint16_t instead of a isaac_uint_t, but by
  //     * using an isaac_uint_t we avoid any padding at the end of the struct.
  //     */
  //    ulong stream_index;

  //private ulong    ISAAC_IND(mm, int x)=>  ((uint8_t*)(mm)                           + ((x) & ((ISAAC_ELEMENTS - 1) << 3)));

  //    private static void Mix(ref ulong a, ref ulong b, ref ulong c, ref ulong d, ref ulong e, ref ulong f, ref ulong g, ref ulong h)
  //    {
  //      a ^= b << 11; d += a; b += c;
  //      b ^= c >> 2; e += b; c += d;
  //      c ^= d << 8; f += c; d += e;
  //      d ^= e >> 16; g += d; e += f;
  //      e ^= f << 10; h += e; f += g;
  //      f ^= g >> 4; a += f; g += h;
  //      g ^= h << 8; b += g; h += a;
  //      h ^= a >> 9; c += h; a += b;
  //    }

  //    private ulong GOLDEN_RATIO = 0x9e3779b97f4a7c13L;

  //    private void Initialize(byte[] seed, int seed_bytes)
  //    {
  //      ulong a, b, c, d, e, f, g, h;

  //      int i;

  //      stream_index = a = b = c = 0;

  //      a = b = c = d = e = f = g = h = GOLDEN_RATIO;

  //      /* Scramble it */
  //      for (i = 0; i < 4; i++)
  //        Mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

  //      set_seed(ctx, seed, seed_bytes);

  //      /* Initialise using the contents of result[] as the seed. */
  //      for (i = 0; i < 256; i += 8)
  //      {
  //        a += result[i + 0];
  //        b += result[i + 1];
  //        c += result[i + 2];
  //        d += result[i + 3];
  //        e += result[i + 4];
  //        f += result[i + 5];
  //        g += result[i + 6];
  //        h += result[i + 7];

  //        Mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

  //        mem[i + 0] = a;
  //        mem[i + 1] = b;
  //        mem[i + 2] = c;
  //        mem[i + 3] = d;
  //        mem[i + 4] = e;
  //        mem[i + 5] = f;
  //        mem[i + 6] = g;
  //        mem[i + 7] = h;
  //      }
  //      /* Do a second pass to make all of the seed affect all of ctx->mem. */
  //      for (i = 0; i < 256; i += 8)
  //      {
  //        a += mem[i + 0];
  //        b += mem[i + 1];
  //        c += mem[i + 2];
  //        d += mem[i + 3];
  //        e += mem[i + 4];
  //        f += mem[i + 5];
  //        g += mem[i + 6];
  //        h += mem[i + 7];

  //        Mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

  //        mem[i + 0] = a;
  //        mem[i + 1] = b;
  //        mem[i + 2] = c;
  //        mem[i + 3] = d;
  //        mem[i + 4] = e;
  //        mem[i + 5] = f;
  //        mem[i + 6] = g;
  //        mem[i + 7] = h;
  //      }

  //      /* Fill in the first set of results. */
  //      isaac_shuffle(ctx);
  //      /* Prepare to use the first set of results with next32() and next8(). */
  //    }

  //    void SetSeed(byte[] seed, int seed_bytes)
  //    {
  //      //ulong i;

  //      if (seed is null || seed.Length == 0 || seed_bytes == 0)
  //        seed_bytes = 0;
  //      else
  //      {
  //        if (seed_bytes > 256)
  //          seed_bytes = 256;

  //        for (var i = 0; i < seed_bytes; i++)
  //        {
  //          /* The copy is performed VALUE-wise, not byte wise.
  //           * By doing so we have same result[] on architectures with different
  //           * endianness. */
  //          result[i] = seed[i];
  //        }
  //      }

  //      for (var i = seed_bytes; i < 256; i++)
  //        result[i] = 0;
  //    }
  //  }

  //  public record class Isaac64
  //  {
  //    private const int ISAAC64_SZ_LOG = 8;
  //    private const int ISAAC64_SZ = 1 << ISAAC64_SZ_LOG;
  //    private const int ISAAC64_SEED_SZ_MAX = ISAAC64_SZ << 3;

  //    private const ulong ISAAC64_MASK = 0xFFFFFFFFFFFFFFFFUL;

  //    private ulong n;
  //    private ulong[] r = new ulong[ISAAC64_SZ];
  //    private ulong[] m = new ulong[ISAAC64_SZ];
  //    private ulong a;
  //    private ulong b;
  //    private ulong c;

  //    /* Extract ISAAC64_SZ_LOG bits (starting at bit 3). */
  //    static uint lower_bits(ulong x)
  //    {
  //      return (uint)((x & ((ISAAC64_SZ - 1) << 3)) >> 3);
  //    }

  //    /* Extract next ISAAC64_SZ_LOG bits (starting at bit ISAAC64_SZ_LOG+2). */
  //    static uint upper_bits(ulong y)
  //    {
  //      return (uint)((y >> (ISAAC64_SZ_LOG + 3)) & (ISAAC64_SZ - 1));
  //    }

  //    void isaac64_update()
  //    {
  //      ulong x;
  //      ulong y;
  //      int i;

  //      b += ++c;

  //      for (i = 0; i < ISAAC64_SZ / 2; i++)
  //      {
  //        x = m[i];
  //        a = ~(a ^ a << 21) + m[i + ISAAC64_SZ / 2];
  //        m[i] = y = m[lower_bits(x)] + a + b;
  //        r[i] = b = m[upper_bits(y)] + x;
  //        x = m[++i];
  //        a = (a ^ a >> 5) + m[i + ISAAC64_SZ / 2];
  //        m[i] = y = m[lower_bits(x)] + a + b;
  //        r[i] = b = m[upper_bits(y)] + x;
  //        x = m[++i];
  //        a = (a ^ a << 12) + m[i + ISAAC64_SZ / 2];
  //        m[i] = y = m[lower_bits(x)] + a + b;
  //        r[i] = b = m[upper_bits(y)] + x;
  //        x = m[++i];
  //        a = (a ^ a >> 33) + m[i + ISAAC64_SZ / 2];
  //        m[i] = y = m[lower_bits(x)] + a + b;
  //        r[i] = b = m[upper_bits(y)] + x;
  //      }

  //      for (i = ISAAC64_SZ / 2; i < ISAAC64_SZ; i++)
  //      {
  //        x = m[i];
  //        a = ~(a ^ a << 21) + m[i - ISAAC64_SZ / 2];
  //        m[i] = y = m[lower_bits(x)] + a + b;
  //        r[i] = b = m[upper_bits(y)] + x;
  //        x = m[++i];
  //        a = (a ^ a >> 5) + m[i - ISAAC64_SZ / 2];
  //        m[i] = y = m[lower_bits(x)] + a + b;
  //        r[i] = b = m[upper_bits(y)] + x;
  //        x = m[++i];
  //        a = (a ^ a << 12) + m[i - ISAAC64_SZ / 2];
  //        m[i] = y = m[lower_bits(x)] + a + b;
  //        r[i] = b = m[upper_bits(y)] + x;
  //        x = m[++i];
  //        a = (a ^ a >> 33) + m[i - ISAAC64_SZ / 2];
  //        m[i] = y = m[lower_bits(x)] + a + b;
  //        r[i] = b = m[upper_bits(y)] + x;
  //      }

  //      n = ISAAC64_SZ;
  //    }

  //    static void isaac64_mix(ulong[] x)
  //    {
  //      var SHIFT = new byte[8] { 9, 9, 23, 15, 14, 20, 17, 14 };

  //      for (var i = 0; i < 8; i++)
  //      {
  //        x[i] -= x[(i + 4) & 7];
  //        x[(i + 5) & 7] ^= x[(i + 7) & 7] >> SHIFT[i];
  //        x[(i + 7) & 7] += x[i];
  //        i++;
  //        x[i] -= x[(i + 4) & 7];
  //        x[(i + 5) & 7] ^= x[(i + 7) & 7] << SHIFT[i];
  //        x[(i + 7) & 7] += x[i];
  //      }
  //    }

  //    private void isaac64_init(byte[] seed)
  //    {
  //      a = b = c = 0;

  //      System.Array.Clear(r, 0, r.Length);

  //      isaac64_reseed(seed);
  //    }

  //    void isaac64_reseed(byte[] seed)
  //    {
  //      var x = new ulong[8];

  //      if (seed.Length > ISAAC64_SEED_SZ_MAX)
  //        seed = System.Array.ToCopy(seed, 0, ISAAC64_SEED_SZ_MAX);

  //      int i, j;

  //      for (i = 0; i < seed.Length >> 3; i++)
  //      {
  //        r[i] ^= (ulong)seed[i << 3 | 7] << 56 | (ulong)seed[i << 3 | 6] << 48 |
  //         (ulong)seed[i << 3 | 5] << 40 | (ulong)seed[i << 3 | 4] << 32 |
  //         (ulong)seed[i << 3 | 3] << 24 | (ulong)seed[i << 3 | 2] << 16 |
  //         (ulong)seed[i << 3 | 1] << 8 | seed[i << 3];
  //      }
  //      var nseed = seed.Length - (i << 3);

  //      if (nseed > 0)
  //      {
  //        ulong ri;
  //        ri = seed[i << 3];
  //        for (j = 1; j < nseed; j++) ri |= (ulong)seed[i << 3 | j] << (j << 3);
  //        r[i++] ^= ri;
  //      }

  //      x[0] = x[1] = x[2] = x[3] = x[4] = x[5] = x[6] = x[7] = (ulong)0x9E3779B97F4A7C13UL;

  //      for (i = 0; i < 4; i++)
  //        isaac64_mix(x);

  //      for (i = 0; i < ISAAC64_SZ; i += 8)
  //      {
  //        for (j = 0; j < 8; j++) x[j] += r[i + j];

  //        isaac64_mix(x);

  //        System.Array.Copy(m, i, x, 0, x.Length);
  //        //memcpy(m + i, x, sizeof(x));
  //      }
  //      for (i = 0; i < ISAAC64_SZ; i += 8)
  //      {
  //        for (j = 0; j < 8; j++) x[j] += m[i + j];

  //        isaac64_mix(x);

  //        System.Array.Copy(m, i, x, 0, x.Length);
  //        //memcpy(m + i, x, sizeof(x));
  //      }

  //      isaac64_update();
  //    }

  //    [System.CLSCompliant(false)]
  //    public ulong NextUInt64()
  //    {
  //      if (n == 0)
  //        isaac64_update();

  //      return r[--n];
  //    }

  //    public long NextInt64()
  //      => (long)(NextUInt64() & 0x7FFFFFFFFFFFFFFFUL);
  //  }
}
