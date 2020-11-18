namespace Flux
{
	public static partial class ArrayInPlace
	{
		/// <summary>Performs an in-place (source) bitwise AND using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
		public static void BitwiseAndInPlace(ref byte[] source, int startAt, byte[] other, int otherStartAt, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (other is null) throw new System.ArgumentNullException(nameof(other));

			while (count-- > 0)
				source[startAt++] &= other[otherStartAt++];
		}

		/// <summary>Performs an in-place negating of source[sourceStartIndex..count].</summary>
		public static void BitwiseNotInPlace(ref byte[] source, int startAt, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			for (int index = startAt, overflowIndex = startAt + count; index < overflowIndex; index++)
				source[index] = (byte)~source[index];
		}
		/// <summary>Performs an in-place negating of source.</summary>
		public static void BitwiseNotInPlace(ref byte[] source)
			=> BitwiseNotInPlace(ref source, 0, (source ?? throw new System.ArgumentNullException(nameof(source))).Length);

		/// <summary>Performs an in-place (source) bitwise OR using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
		public static void BitwiseOrInPlace(ref byte[] source, int startAt, byte[] other, int otherStartAt, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (other is null) throw new System.ArgumentNullException(nameof(other));

			while (count-- > 0)
				source[startAt++] |= other[otherStartAt++];
		}

		/// <summary>Performs an in-place (source) bitwise XOR using source[sourceStartAt..count] and other[otherStartAt..count].</summary>
		public static void BitwiseXorInPlace(ref byte[] source, int startAt, byte[] other, int otherStartAt, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (other is null) throw new System.ArgumentNullException(nameof(other));

			while (count-- > 0)
				source[startAt++] ^= other[otherStartAt++];
		}
	}
}
