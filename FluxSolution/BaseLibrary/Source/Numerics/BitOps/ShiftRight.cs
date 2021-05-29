namespace Flux.Numerics
{
	public static partial class BitOps
	{
		[System.CLSCompliant(false)]
		public static bool ShiftRight(ref uint value)
		{
			var carryFlag = (value & 1) != 0;
			value >>= 1;
			return carryFlag;
		}

		/// <summary>Shifts the bits one position to the right.</summary>
		/// <returns>Whether the LSB (least significant BIT), or overflow bit, was set.</returns>
		[System.CLSCompliant(false)]
		public static bool ShiftRight(ref ulong value)
		{
			var carryFlag = (value & 1) != 0;
			value >>= 1;
			return carryFlag;
		}

		/// <summary>Shifts the bits one position to the right.</summary>
		/// <returns>Whether the LSB (least significant BIT), or overflow bit, was set.</returns>
		[System.CLSCompliant(false)]
		public static bool ShiftRight(ref uint value, int count)
		{
			var carryFlag = (value & 1) != 0;
			value >>= count;
			return carryFlag;
		}

		/// <summary>Shifts the bits one position to the right.</summary>
		/// <returns>Whether the LSB (least significant BIT), or overflow bit, was set.</returns>
		[System.CLSCompliant(false)]
		public static bool ShiftRight(ref ulong value, int count)
		{
			var carryFlag = (value & 1) != 0;
			value >>= count;
			return carryFlag;
		}
	}
}
