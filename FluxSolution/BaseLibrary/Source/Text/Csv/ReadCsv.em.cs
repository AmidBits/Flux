using System.Linq;

namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Returns a new sequence of string arrays from the <see cref="System.IO.Stream"/>, utilizing a <see cref="Flux.Text.CsvReader"/> with the specified options (defaults if null).</summary>
		public static System.Collections.Generic.IEnumerable<string[]> ReadCsv(this System.IO.Stream source, Text.Csv.CsvOptions? options)
		{
			using var reader = new Text.Csv.CsvReader(source, options ?? new Text.Csv.CsvOptions());

			foreach (var idr in reader)
				yield return idr.GetStrings(string.Empty).ToArray();
		}
	}
}
