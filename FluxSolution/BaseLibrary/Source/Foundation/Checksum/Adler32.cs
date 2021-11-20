namespace Flux.Checksum
{
	/// <summary></summary>
	/// <see cref="https://en.wikipedia.org/wiki/Adler-32"/>
	public struct Adler32
		: IChecksumGenerator32, System.IEquatable<Adler32>
	{
		public static readonly Adler32 Empty;

		private uint m_hash;// = 1;

		public int Checksum32 { get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

		public Adler32(int hash = 1) => m_hash = unchecked((uint)hash);

		public int GenerateChecksum32(byte[] bytes, int startAt, int count)
		{
			if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

			unchecked
			{
				var a = m_hash & 0xffff;
				var b = (m_hash >> 16) & 0xffff;

				for (var maxCount = (count < 5552) ? count : 5552; count > 0; count -= maxCount, maxCount = count < 5552 ? count : 5552)
				{
					for (int counter = 0; counter < maxCount; counter++)
					{
						a += bytes[startAt++];
						b += a;
					}

					a %= 65521;
					b %= 65521;
				}

				m_hash = (b << 16) | a;
			}

			return Checksum32;
		}

		// Operators
		public static bool operator ==(Adler32 a, Adler32 b)
			=> a.Equals(b);
		public static bool operator !=(Adler32 a, Adler32 b)
			=> !a.Equals(b);

		// IEquatable
		public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Adler32 other)
			=> m_hash == other.m_hash;

		// Object (overrides)
		public override bool Equals(object? obj)
			=> obj is Adler32 o && Equals(o);
		public override int GetHashCode()
			=> m_hash.GetHashCode();
		public override string ToString()
			=> $"{nameof(Adler32)} {{ CheckSum = {m_hash} }}";
	}
}