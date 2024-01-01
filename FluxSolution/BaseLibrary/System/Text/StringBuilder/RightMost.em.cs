namespace Flux
{
	public static partial class Fx
	{
		/// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
		public static string RightMost(this System.Text.StringBuilder source, int maxCount)
			=> maxCount < 0 ? throw new System.ArgumentOutOfRangeException(nameof(maxCount)) : maxCount < (source ?? throw new System.ArgumentNullException(nameof(source))).Length ? source.ToString(source.Length - maxCount, maxCount) : source.ToString();
	}
}
