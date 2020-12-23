namespace Flux.Random
{
	/// <remarks>In earlier .net implementations, only Sample() was required to be overridden, whereas later implementations changed requirements of more overrides.</remarks>
	/// <see cref="https://docs.microsoft.com/en-us/dotnet/api/system.random"/>
	public abstract class RandomUInt64
		: System.Random
	{
		/// <summary>Returns a non-negative random integer.</summary>
		/// <returns>A non-negative random integer.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override int Next()
			=> Next(int.MaxValue);
		/// <summary>Returns a non-negative random integer that is less than the specified maximum.</summary>
		/// <returns>A non-negative random integer that is less than the specified maximum.</returns>
		public override int Next(int maxValue)
			=> maxValue >= 2 ? (int)System.Math.Floor(maxValue * Sample()) : throw new System.ArgumentOutOfRangeException(nameof(maxValue), @"The maximum value must be greater than zero.");
		/// <summary>Returns a random integer that is within a specified range.</summary>
		/// <returns>A random integer that is within a specified range, greater than or equal to specified minValue, and less than the specified maxValue.</returns>
		public override int Next(int minValue, int maxValue)
			=> minValue < maxValue ? minValue + Next(maxValue - minValue) : throw new System.ArgumentOutOfRangeException(nameof(minValue), @"The minimum value must be less than the maximum value.");
		/// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
		public override void NextBytes(byte[] buffer)
		{
			if (buffer is null) throw new System.ArgumentNullException(nameof(buffer));

			for (var index = 0; index < buffer.Length;)
			{
				var value = SampleUInt64();

				for (var count = 0; count < 8 && index < buffer.Length; count++, value >>= 8)
					buffer[index++] = unchecked((byte)value);
			}
		}
		/// <summary>Needs to return a value that is greater than or equal to 0.0, and less than 1.0</summary>
		/// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override double NextDouble()
			=> Sample();

		/// <summary>Needs to return a value that is greater than or equal to 0.0, and less than 1.0</summary>
		/// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
		protected override double Sample()
			=> (double)(SampleUInt64() >> 11) / (double)(1L << 53);

		internal abstract ulong SampleUInt64();
	}
}
