namespace Flux
{
  public static partial class ExtensionMethods
	{
		/// <summary>Returns a string representing the <see cref="System.Text.Rune"/> as a Unicode notation.</summary>
		public static string ToUnicodeNotation(this System.Text.Rune source)
			=> Text.UnicodeNotation.ToString(source);
	}
}