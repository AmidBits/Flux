namespace Flux.Resources.Scrape
{
	public class ZipCodes
		: ITabularDataAcquirer
	{
		public static System.Uri UriLocal
			=> new System.Uri(@"file://\Resources\Scrape\free-zipcode-database.csv");
		public static System.Uri UriSource
			=> new System.Uri(@"http://federalgovernmentzipcodes.us/free-zipcode-database.csv");

		public System.Uri Uri { get; private set; }

		public ZipCodes(System.Uri uri)
			=> Uri = uri;

		/// <summary>A free zip code file.</summary>
		/// <see cref="http://federalgovernmentzipcodes.us/"/>
		// Download URL: http://federalgovernmentzipcodes.us/free-zipcode-database.csv
		public System.Collections.Generic.IEnumerable<object[]> AcquireTabularData()
		{
			using var e = Uri.GetStream().ReadCsv(new Text.Csv.CsvOptions()).GetEnumerator();

			if (e.MoveNext())
			{
				yield return e.Current; // This is the field names (column headers).

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
