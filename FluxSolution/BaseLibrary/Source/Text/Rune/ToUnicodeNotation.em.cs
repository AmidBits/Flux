namespace Flux
{
  public static partial class RuneEm
	{
		/// <summary>Returns a string representing the <see cref="System.Text.Rune"/> as a Unicode notation.</summary>
		public static string ToUnicodeNotation(this System.Text.Rune source)
			=> Unicode.ToUnotationString(source);
	}
}