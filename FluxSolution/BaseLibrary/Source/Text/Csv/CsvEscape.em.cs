using System;

namespace Flux
{
	public static partial class TextCsvEm
	{
		public static string CsvEscape(this string source, char fieldSeparator)
			=> source.AsSpan().GetCsvEscapeLevel(fieldSeparator) switch
			{
				Text.Csv.CsvEscapeLevel.None => source,
				Text.Csv.CsvEscapeLevel.Enclose => source.Wrap('"', '"'),
				Text.Csv.CsvEscapeLevel.ReplaceAndEnclose => source.ToStringBuilder().Replace("\"", "\"\"").Wrap('"', '"').ToString(),
				_ => source,
			};
	}
}
