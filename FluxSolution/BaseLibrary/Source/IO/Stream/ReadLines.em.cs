namespace Flux
{
	public static partial class SystemIoEm
	{
		/// <summary>Returns a new sequence with all lines from the <see cref="System.IO.Stream"/> with the <see cref="System.Text.Encoding"/> and whether to keep empty lines.</summary>
		public static System.Collections.Generic.IEnumerable<string> ReadLines(this System.IO.Stream source, System.Text.Encoding encoding, bool keepEmptyLines = false)
		{
			using var reader = new System.IO.StreamReader(source, encoding);

			for (var line = reader.ReadLine(); line is not null; line = reader.ReadLine())
				if (line.Length > 0 || keepEmptyLines)
					yield return line;
		}
	}
}
