namespace Flux
{
	public static partial class ExtensionMethodsStringBuilder
	{
		/// <summary>Returns a string containing the left most specified number of characters, if available, otherwise as many as there are.</summary>
		public static string LeftMost(this System.Text.StringBuilder source, int maxCount)
			=> (source ?? throw new System.ArgumentNullException(nameof(source))).ToString(0, System.Math.Min(source.Length, System.Math.Max(maxCount, 0)));
	}
}
