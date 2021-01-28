using System.Linq;

namespace Flux
{
	public static partial class SystemIoEm
	{
		/// <summary>Returns a new sequence of string arrays from the <see cref="System.IO.Stream"/>, utilizing a <see cref="Flux.Text.CsvReader"/> with the specified options (defaults if null).</summary>
		public static System.Collections.Generic.IEnumerable<string[]> ReadCsv(this System.IO.Stream source, Text.CsvOptions? options)
		{
			using var reader = new Text.CsvReader(source, options ?? new Text.CsvOptions());

			foreach (var array in reader.ReadArrays())
				yield return array;
		}
	}
}
