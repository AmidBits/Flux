namespace Flux
{
  public static partial class StreamEm
	{
		/// <summary>Returns a new sequence of string arrays from the <see cref="System.IO.Stream"/>, utilizing a <see cref="Text.CsvReader"/> with the specified options (defaults if null).</summary>
		public static System.Collections.Generic.IEnumerable<string[]> ReadCsv(this System.IO.Stream source, CsvOptions? options)
		{
			using var reader = new CsvReader(source, options ?? new CsvOptions());

			foreach (var idr in reader)
				yield return idr.GetStrings(string.Empty).ToArray();
		}
	}
}
