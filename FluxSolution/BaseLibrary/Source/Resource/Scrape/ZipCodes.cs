namespace Flux.Resources.Scrape
{
  /// <summary>A free zip code file.</summary>
  /// <see cref="http://federalgovernmentzipcodes.us/"/>
  // Download URL: http://federalgovernmentzipcodes.us/free-zipcode-database.csv
  public sealed class ZipCodes
    : ATabularDataAcquirable
  {
    public static string LocalFile
      => @"file://\Resources\Scrape\free-zipcode-database.csv";
    public static System.Uri UriSource
      => new(@"http://federalgovernmentzipcodes.us/free-zipcode-database.csv");

    public System.Uri Uri { get; private set; }

    public ZipCodes(System.Uri uri)
      => Uri = uri ?? throw new System.ArgumentNullException(nameof(uri));

    private string[]? m_fieldNames = null;
    public override string[] FieldNames
      => m_fieldNames ??= GetStrings().First().ToArray();

    private System.Type[]? m_fieldTypes = null;
    public override System.Type[] FieldTypes
      => m_fieldTypes ??= FieldNames.Select((e, i) =>
      {
        return i switch
        {
          0 or 16 or 17 or 18 => typeof(int),
          6 or 7 or 8 or 9 or 10 => typeof(double),
          _ => typeof(string),
        };
      }).ToArray();

    public override System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
    {
      using var e = GetStrings().Skip(1).GetEnumerator();

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
              objects[index] = int.TryParse(e.Current[index], System.Globalization.NumberStyles.Integer, null, out var intValue) ? intValue : System.DBNull.Value;
              break;
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
              objects[index] = double.TryParse(e.Current[index], System.Globalization.NumberStyles.Float, null, out var doubleValue) ? doubleValue : System.DBNull.Value;
              break;
            default:
              objects[index] = e.Current[index];
              break;
          }
        }

        yield return objects;
      }
    }

    /// <summary>Returns zip codes with the first line being field names.</summary>
    public System.Collections.Generic.IEnumerable<string[]> GetStrings()
      => Uri.GetStream().ReadCsv(new CsvOptions());
  }
}
