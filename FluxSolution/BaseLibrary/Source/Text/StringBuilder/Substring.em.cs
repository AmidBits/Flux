namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Returns a string containing the left most specified number of characters, if available, otherwise as many as there are.</summary>
		public static string LeftMost(this System.Text.StringBuilder source, int maxCount)
			=> (source ?? throw new System.ArgumentNullException(nameof(source))).ToString(0, System.Math.Min(source.Length, System.Math.Max(maxCount, 0)));

		/// <summary>Returns a string containing the right specified number of characters.</summary>
		public static string Right(this System.Text.StringBuilder source, int count)
			=> (source ?? throw new System.ArgumentNullException(nameof(source))).ToString(source.Length - count, count);

		/// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
		public static string RightMost(this System.Text.StringBuilder source, int maxCount)
			=> maxCount < 0 ? throw new System.ArgumentOutOfRangeException(nameof(maxCount)) : maxCount < (source ?? throw new System.ArgumentNullException(nameof(source))).Length ? source.ToString(source.Length - maxCount, maxCount) : source.ToString();
	}
}
