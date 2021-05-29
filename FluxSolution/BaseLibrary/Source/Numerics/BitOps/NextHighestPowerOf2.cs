namespace Flux.Numerics
{
	public static partial class BitOps
	{
		/// <summary>Computes the next power of 2 greater than or optionally equal to the specified number. This is equivalent to (MSB << 1).</summary>
		[System.CLSCompliant(false)]
		public static uint NextHighestPowerOf2(uint value)
		{
			value |= (value >> 1);
			value |= (value >> 2);
			value |= (value >> 4);
			value |= (value >> 8);
			value |= (value >> 16);
			return value + 1;
		}
		/// <summary>Computes the next power of 2 greater than or optionally equal to the specified number. This is equivalent to (MSB << 1).</summary>
		[System.CLSCompliant(false)]
		public static ulong NextHighestPowerOf2(ulong value)
		{
			value |= (value >> 1);
			value |= (value >> 2);
			value |= (value >> 4);
			value |= (value >> 8);
			value |= (value >> 16);
			value |= (value >> 32);
			return value + 1;
		}
	}
}
