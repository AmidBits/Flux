namespace Flux
{
	public static partial class UriEm
	{

		public static System.Collections.Generic.IEnumerable<string[]> ReadCsv(this System.Uri uri, Text.CsvOptions? options)
		{
			using var reader = new Text.CsvReader(uri.GetStream(), options ?? new Text.CsvOptions());

			foreach (var array in reader.ReadArrays())
				yield return array;
		}
	}
}
