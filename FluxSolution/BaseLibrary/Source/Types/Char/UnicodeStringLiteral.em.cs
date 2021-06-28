namespace Flux
{
  public static partial class ExtensionMethods
	{
		/// <summary>Returns a string representing the <see cref="System.Text.Rune"/> as a Unicode literal (e.g. as used in C#).</summary>
		public static string ToUnicodeStringLiteral(this char source)
			=> Text.UnicodeStringLiteral.ToString(source);
	}
}