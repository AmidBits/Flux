using Flux;
namespace Flux.Resources.Scrape
{
  /// <summary>A free zip code file.</summary>
  /// <see cref="http://federalgovernmentzipcodes.us/"/>
  // Download URL: http://federalgovernmentzipcodes.us/free-zipcode-database.csv
  public sealed class ZipCodes
    : ITabularDataAcquirable
  {
    public static readonly System.Uri Local = new(@"file://\Resources\Scrape\free-zipcode-database.csv");
    public static readonly System.Uri Origin = new(@"http://federalgovernmentzipcodes.us/free-zipcode-database.csv");

    public System.Uri Uri { get; private set; } = Local;

    /// <summary>Returns zip codes with the first line being field names.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      using var stream = uri.GetStream();
      using var reader = new StreamReader(stream);

      foreach (var fields in reader.ReadCsv(",", true))
        yield return fields;
    }

    #region Implemented interfaces

    private string[]? m_fieldNames = null;
    public string[] FieldNames => m_fieldNames ??= GetData(Uri).First().ToArray();

    private System.Type[]? m_fieldTypes = null;
    public System.Type[] FieldTypes => m_fieldTypes ??= FieldNames.Select((e, i) => i switch
    {
      0 or 16 or 17 or 18 => typeof(int),
      6 or 7 or 8 or 9 or 10 => typeof(double),
      _ => typeof(string),
    }).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri).Skip(1);

    #endregion // Implemented interfaces
  }
}
