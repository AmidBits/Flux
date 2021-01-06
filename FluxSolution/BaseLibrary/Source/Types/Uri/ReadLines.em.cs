namespace Flux
{
	public static partial class SystemUriEm
	{
		/// <summary>Returns all lines from the <paramref cref="uri"/> with the <paramref cref="encoding"/>.</summary>
		/// <example>new System.Uri(@"file://\Flux\Resources\Ucd\UnicodeText.txt\").ReadLines(System.Text.Encoding.UTF8)</example>
		public static System.Collections.Generic.IEnumerable<string> ReadLines(this System.Uri uri, System.Text.Encoding encoding, bool keepEmptyLines = false)
		{
			using var reader = new System.IO.StreamReader(GetStream(uri), encoding);

			for (var line = reader.ReadLine(); line is not null; line = reader.ReadLine())
				if (line.Length > 0 || keepEmptyLines)
					yield return line;
		}
	}
}
