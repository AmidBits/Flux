namespace Flux
{
	public static partial class Xtensions
	{
		/// <summary>Performs an in-place left rotation of all bits in the array.</summary>
		public static void BitRotateLeft(this byte[] source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			if (BitShiftLeft(source))
				source[source.Length - 1] |= 0x01;
		}
		/// <summary>Performs an in-place right rotation of all bits in the array.</summary>
		public static void BitRotateRight(this byte[] source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			if (BitShiftRight(source))
				source[0] |= 0x80;
		}
	}
}
