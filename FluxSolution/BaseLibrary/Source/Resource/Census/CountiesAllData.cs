namespace Flux.Resources.Census
{
	public class CountiesAllData
		: ITabularDataAcquirer
	{
		public static System.Uri UriLocal
			=> new System.Uri(@"file://\Resources\Census\cc-est2019-alldata-04.csv");
		public static System.Uri UriSource
			=> new System.Uri(@"https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv");

		public System.Uri Uri { get; private set; }

		public CountiesAllData(System.Uri uri)
			=> Uri = uri;

		/// <summary>Census all data.</summary>
		/// <see cref="https://www.census.gov/content/census/en/data/tables/time-series/demo/popest/2010s-counties-detail.html"/>
		// Download URL: https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv
		public System.Collections.Generic.IEnumerable<object[]> AcquireTabularData()
		{
			using var e = Uri.GetStream().ReadCsv(new Text.Csv.CsvOptions()).GetEnumerator();

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
							>= 5 => System.Int32.Parse(e.Current[i], System.Globalization.NumberStyles.Integer, null),
							_ => e.Current[i],
						};
					}

					yield return objectArray;
				}
			}
		}

	}
}
