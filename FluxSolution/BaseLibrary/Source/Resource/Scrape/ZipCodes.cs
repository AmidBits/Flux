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
					var objectArray = new object[e.Current.Length];

					for (var i = objectArray.Length - 1; i >= 0; i--)
					{
						objectArray[i] = i switch
						{
							0 => System.Int32.Parse(e.Current[i], System.Globalization.NumberStyles.Integer, null),
							6 or 7 => System.Double.Parse(e.Current[i], System.Globalization.NumberStyles.Number, null),
							_ => e.Current[i],
						};
					}

					yield return objectArray;
				}
			}
		}
	}
}
