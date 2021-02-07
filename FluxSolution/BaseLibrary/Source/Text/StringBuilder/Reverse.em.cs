namespace Flux
{
	public static partial class SystemTextStringBuilderEm
	{
		/// <summary>Reverse all ranged characters, in-place.</summary>
		internal static System.Text.StringBuilder ReverseImpl(this System.Text.StringBuilder source, int startIndex, int lastIndex)
		{
			for (int indexL = startIndex, indexR = lastIndex; indexL < indexR; indexL++, indexR--)
			{
				var tmp = source[indexL];
				source[indexL] = source[indexR];
				source[indexR] = tmp;
			}

			return source;
		}

		/// <summary>Reverse all characters in-place.</summary>
		public static System.Text.StringBuilder Reverse(this System.Text.StringBuilder source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			return ReverseImpl(source, 0, source.Length - 1);
		}
		/// <summary>Reverse all ranged characters in-place.</summary>
		public static System.Text.StringBuilder Reverse(this System.Text.StringBuilder source, int startIndex, int endIndex)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (startIndex < 0 || startIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
			if (endIndex < startIndex || endIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

			return ReverseImpl(source, startIndex, endIndex);
		}
	}
}
