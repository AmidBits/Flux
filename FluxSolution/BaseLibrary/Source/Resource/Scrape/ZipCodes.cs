using System.Linq;

namespace Flux.Resources.Scrape
{
	public static class ZipCodes
	{
		public static System.Uri UriLocal
			=> new System.Uri(@"file://\Resources\Scrape\free-zipcode-database.csv");
		public static System.Uri UriSource
			=> new System.Uri(@"http://federalgovernmentzipcodes.us/free-zipcode-database.csv");

		/// <summary>A free zip code file.</summary>
		/// <see cref="http://federalgovernmentzipcodes.us/"/>
		// Download URL: http://federalgovernmentzipcodes.us/free-zipcode-database.csv
		public static System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
			=> uri.GetStream().ReadCsv(new Text.CsvOptions());

		/// <summary>Same as GetStrings, but objects instead, and some complete row values converted to numerical values, e.g. RecordNumber, Lat, Long.</summary>
		public static System.Collections.Generic.IEnumerable<object[]> GetObjects(System.Uri uri)
		{
			using var e = GetStrings(uri).GetEnumerator();

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
							0 or 16 or 17 or 18 => int.TryParse(e.Current[index], System.Globalization.NumberStyles.Integer, null, out var value) ? value : System.DBNull.Value,
							6 or 7 or 8 or 9 or 10 => double.TryParse(e.Current[index], System.Globalization.NumberStyles.Float, null, out var value) ? value : System.DBNull.Value,
							_ => e.Current[index],
						};
					}

					yield return objects;
				}
			}
		}
	}
}
