namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Returns a string containing the right specified number of characters.</summary>
		public static string Right(this System.Text.StringBuilder source, int count)
			=> (source ?? throw new System.ArgumentNullException(nameof(source))).ToString(source.Length - count, count);
	}
}
