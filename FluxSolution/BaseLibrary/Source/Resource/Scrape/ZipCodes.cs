namespace Flux.Resources.Scrape
{
  /// <summary>A free zip code file.</summary>
  /// <see cref="http://federalgovernmentzipcodes.us/"/>
  // Download URL: http://federalgovernmentzipcodes.us/free-zipcode-database.csv
  public sealed class ZipCodes
    : ITabularDataAcquirable
  {
    public static string LocalFile
      => @"file://\Resources\Scrape\free-zipcode-database.csv";
    public static System.Uri SourceUri
      => new(@"http://federalgovernmentzipcodes.us/free-zipcode-database.csv");

    public System.Uri Uri { get; private set; }

    public ZipCodes(System.Uri uri)
      => Uri = uri ?? throw new System.ArgumentNullException(nameof(uri));

    private string[]? m_fieldNames = null;
    public string[] FieldNames
      => m_fieldNames ??= GetStrings().First().ToArray();

    private System.Type[]? m_fieldTypes = null;
    public System.Type[] FieldTypes
      => m_fieldTypes ??= FieldNames.Select((e, i) =>
      {
        return i switch
        {
          0 or 16 or 17 or 18 => typeof(int),
          6 or 7 or 8 or 9 or 10 => typeof(double),
          _ => typeof(string),
        };
      }).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
    {
      using var e = GetStrings().Skip(1).GetEnumerator();

      while (e.MoveNext())
      {
        var objects = new object[e.Current.Length];

        for (var index = objects.Length - 1; index >= 0; index--)
        {
          objects[index] = index switch
          {
            0 or 16 or 17 or 18 => int.TryParse(e.Current[index], System.Globalization.NumberStyles.Integer, null, out var intValue) ? intValue : System.DBNull.Value,
            6 or 7 or 8 or 9 or 10 => double.TryParse(e.Current[index], System.Globalization.NumberStyles.Float, null, out var doubleValue) ? doubleValue : System.DBNull.Value,
            _ => e.Current[index],
          };
        }

        yield return objects;
      }
    }

    /// <summary>Returns zip codes with the first line being field names.</summary>
    public System.Collections.Generic.IEnumerable<string[]> GetStrings()
      => Uri.GetStream().ReadCsv(new CsvOptions());
  }
}
