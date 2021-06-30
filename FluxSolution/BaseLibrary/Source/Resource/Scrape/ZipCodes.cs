namespace Flux.Resources.Scrape
{
  public class ZipCodes
    : ATabularDataAcquirer
  {
    public static string LocalFile
      => @"file://\Resources\Scrape\free-zipcode-database.csv";
    public static System.Uri UriSource
      => new System.Uri(@"http://federalgovernmentzipcodes.us/free-zipcode-database.csv");

    public System.Uri Uri { get; private set; }

    public ZipCodes(System.Uri uri)
      => Uri = uri;

    /// <summary>A free zip code file.</summary>
    /// <see cref="http://federalgovernmentzipcodes.us/"/>
    // Download URL: http://federalgovernmentzipcodes.us/free-zipcode-database.csv
    public override System.Collections.Generic.IEnumerable<object[]> AcquireTabularData()
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
            switch (index)
            {
              case 0:
              case 16:
              case 17:
              case 18:
                if (int.TryParse(e.Current[index], System.Globalization.NumberStyles.Integer, null, out var intValue))
                  objects[index] = intValue;
                else
                  objects[index] = System.DBNull.Value;
                break;
              case 6:
              case 7:
              case 8:
              case 9:
              case 10:
                if (double.TryParse(e.Current[index], System.Globalization.NumberStyles.Float, null, out var doubleValue))
                  objects[index] = doubleValue;
                else
                  objects[index] = System.DBNull.Value;
                break;
              default:
                objects[index] = e.Current[index];
                break;
            }
          }

          yield return objects;
        }
      }
    }
  }
}
