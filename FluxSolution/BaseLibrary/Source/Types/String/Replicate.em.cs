namespace Flux
{
	public static partial class ExtensionMethodsString
	{
		/// <summary>Replicates the string a specified number of times.</summary>
		public static System.Text.StringBuilder Replicate(this string source, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			else if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

			var sb = new System.Text.StringBuilder(source);
			while (count-- > 0)
				sb.Append(source);
			return sb;
		}

		public static System.Text.StringBuilder ReplicateLeft(this string source, int desiredLength)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var quotient = System.Math.DivRem(desiredLength, source.Length, out var remainder);

			return Replicate(source, quotient + (remainder > 0 ? 1 : 0)).Remove(0, source.Length - remainder);
		}

		public static System.Text.StringBuilder ReplicateRight(this string source, int desiredLength)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var quotient = System.Math.DivRem(desiredLength, source.Length, out var remainder);

			return Replicate(source, quotient + (remainder > 0 ? 1 : 0)).Remove(source.Length * quotient + remainder, source.Length - remainder);
		}
	}
}
