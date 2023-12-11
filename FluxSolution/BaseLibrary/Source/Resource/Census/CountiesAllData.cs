namespace Flux.Resources.Census
{
  /// <summary>Census all data.</summary>
  /// <see cref="https://www.census.gov/content/census/en/data/tables/time-series/demo/popest/2010s-counties-detail.html"/>
  // Download URL: https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv
  public sealed class CountiesAllData
    : ITabularDataAcquirable
  {
    public static readonly System.Uri Local = new(@"file://\Resources\Census\cc-est2019-alldata-04.csv");
    public static readonly System.Uri Origin = new(@"https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv");

    public System.Uri Uri { get; private set; } = Local;

    /// <summary>Returns counties all data with the first line being field names.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      using var stream = uri.GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var line in reader.ReadLines(s => s.Length > 0, s => s))
        yield return line.Split(',');
    }

    #region Implemented interfaces

    private string[]? m_fieldNames = null;
    public string[] FieldNames => m_fieldNames ??= GetData(Uri).First().ToArray();

    private System.Type[]? m_fieldTypes = null;
    public System.Type[] FieldTypes => m_fieldTypes ??= FieldNames.Select((e, i) => i switch
    {
      >= 5 => typeof(int),
      _ => typeof(string),
    }).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri).Skip(1); // Skip one row because the source has field names.

    #endregion // Implemented interfaces
  }
}
