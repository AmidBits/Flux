namespace Flux.Resources.Ucd
{
	public sealed class UnicodeData
		: ATabularDataAcquirer
	{
		public static string LocalFile
			=> @"file://\Resources\Ucd\UnicodeData.txt";
		public static System.Uri UriSource
			=> new(@"https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt");

		public System.Uri Uri { get; private set; }

		public UnicodeData(System.Uri uri)
			=> Uri = uri;

		/// <summary>The Unicode character database.</summary>
		/// <see cref="https://www.unicode.org/"/>
		/// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
		/// <seealso cref="https://unicode.org/Public/"/>
		// Download URL: https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt
		public override System.Collections.Generic.IEnumerable<object[]> AcquireTabularData()
		{
			using var e = GetStrings(Uri).GetEnumerator();

			if (e.MoveNext())
			{
				yield return e.Current;

				while (e.MoveNext())
				{
					var objects = new object[e.Current.Length];

					for (var index = objects.Length - 1; index >= 0; index--)
					{
						objects[index] = index switch
						{
							0 => int.TryParse(e.Current[index], System.Globalization.NumberStyles.HexNumber, null, out var value) ? value : System.DBNull.Value,
							2 => Text.Unicode.TryParseCategoryMajorMinor(e.Current[index], out var value) ? value.ToUnicodeCategory() : System.DBNull.Value,
							_ => e.Current[index],
						};
					}

					yield return objects;
				}
			}

			static System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
			{
				yield return new string[] { "CodePoint", "Name", "GeneralCategory", "CanonicalCombiningClass", "BidiClass", "DecompositionTypeMapping", "NumericType6", "NumericType7", "NumericType8", "BidiMirrored", "Unicode1Name", "IsoComment", "SimpleUppercaseMapping", "SimpleLowercaseMapping", "SimpleTitlecaseMapping" };

				if (uri is null) throw new System.ArgumentNullException(nameof(uri));

				foreach (var item in uri.GetStream().ReadCsv(new Text.Csv.CsvOptions() { FieldSeparator = ';' }))
					yield return item;
			}

		}
	}
}
