namespace Flux.Random
{
	/// <summary></summary>
	/// <see cref="https://en.wikipedia.org/wiki/ISAAC_(cipher)"/>
	/// <seealso cref="http://burtleburtle.net/bob/rand/isaacafa.html"/>
	/// <seealso cref="http://rosettacode.org/wiki/The_ISAAC_Cipher#C.23"/>
	public class IsaacRandom
		: RandomUInt32
	{
		public static System.Random Default
			=> new IsaacRandom();

		#region Isaac
		private uint m_randcnt;
		private readonly uint[] m_randrsl = new uint[256];
		private readonly uint[] m_randmem = new uint[256];
		private uint m_randa;
		private uint m_randb;
		private uint m_randc;

		public void Seed(string seed, bool flag)
		{
			if (seed is null) throw new System.ArgumentNullException(nameof(seed));

			for (var i = 0; i < 256; i++)
				m_randmem[i] = 0;
			for (var i = 0; i < 256; i++)
				m_randrsl[i] = 0;

			for (var i = 0; i < seed.Length; i++)
				m_randrsl[i] = seed[i];

			RandInit(flag);
		}

		void ReIsaac()
		{
			uint x, y;

			m_randc++;
			m_randb = unchecked(m_randb + m_randc);

			for (var i = 0U; i <= 255; i++)
			{
				x = m_randmem[i];

				switch (i & 3)
				{
					case 0: m_randa ^= (m_randa << 13); break;
					case 1: m_randa ^= (m_randa >> 6); break;
					case 2: m_randa ^= (m_randa << 2); break;
					case 3: m_randa ^= (m_randa >> 16); break;
				}

				m_randa = unchecked(m_randmem[(i + 128) & 255] + m_randa);
				y = unchecked(m_randmem[(x >> 2) & 255] + m_randa + m_randb);
				m_randmem[i] = y;

				m_randb = unchecked(m_randmem[(y >> 10) & 255] + x);
				m_randrsl[i] = m_randb;
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

		private void RandInit(bool flag)
		{
			uint a, b, c, d, e, f, g, h;

			m_randa = m_randb = m_randc = 0;

			a = b = c = d = e = f = g = h = 0x9e3779b9;

			for (var i = 0; i <= 3; i++) // scramble it 
				Mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

			for (var i = 0; i < 256; i += 8) // fill in _randmem[] with messy stuff
			{
				if (flag)
				{
					a += m_randrsl[i]; b += m_randrsl[i + 1]; c += m_randrsl[i + 2]; d += m_randrsl[i + 3];
					e += m_randrsl[i + 4]; f += m_randrsl[i + 5]; g += m_randrsl[i + 6]; h += m_randrsl[i + 7];
				}

				Mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

				m_randmem[i] = a; m_randmem[i + 1] = b; m_randmem[i + 2] = c; m_randmem[i + 3] = d;
				m_randmem[i + 4] = e; m_randmem[i + 5] = f; m_randmem[i + 6] = g; m_randmem[i + 7] = h;
			}

			if (flag)
			{
				for (var i = 0; i < 256; i += 8) // do a second pass to make all of the seed affect all of mm 
				{
					unchecked
					{
						a += m_randmem[i]; b += m_randmem[i + 1]; c += m_randmem[i + 2]; d += m_randmem[i + 3];
						e += m_randmem[i + 4]; f += m_randmem[i + 5]; g += m_randmem[i + 6]; h += m_randmem[i + 7];

						Mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

						m_randmem[i] = a; m_randmem[i + 1] = b; m_randmem[i + 2] = c; m_randmem[i + 3] = d;
						m_randmem[i + 4] = e; m_randmem[i + 5] = f; m_randmem[i + 6] = g; m_randmem[i + 7] = h;
					}
				}
			}

			ReIsaac();

			m_randcnt = 0;
		}

		internal override uint SampleUInt32()
		{
			var result = m_randrsl[m_randcnt];

			if (++m_randcnt > 255)
			{
				ReIsaac();

				m_randcnt = 0;
			}

			return result;
		}
		#endregion

		public IsaacRandom(string seed)
			=> Seed(seed, true);
		[System.CLSCompliant(false)]
		public IsaacRandom(ulong seed)
			: this(seed.ToString("x2", System.Globalization.CultureInfo.InvariantCulture))
		{
		}
		public IsaacRandom(long seed)
			: this(unchecked((ulong)seed))
		{
		}
		public IsaacRandom()
			: this(System.Diagnostics.Stopwatch.GetTimestamp())
		{
		}
	}
}
