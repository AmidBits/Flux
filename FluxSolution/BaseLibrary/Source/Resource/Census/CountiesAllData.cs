using System.Linq;

namespace Flux.Resources.Census
{
  /// <summary>Census all data.</summary>
  /// <see cref="https://www.census.gov/content/census/en/data/tables/time-series/demo/popest/2010s-counties-detail.html"/>
  // Download URL: https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv
  public sealed class CountiesAllData
    : ITabularDataAcquirable
  {
    public static string LocalFile
      => @"file://\Resources\Census\cc-est2019-alldata-04.csv";
    public static System.Uri SourceUri
      => new(@"https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv");

    public System.Uri Uri { get; private set; }

    public CountiesAllData(System.Uri uri)
      => Uri = uri;

    private string[]? m_fieldNames = null;
    public string[] FieldNames
      => m_fieldNames ??= GetStrings().First().ToArray();

    private System.Type[]? m_fieldTypes = null;
    public System.Type[] FieldTypes
      => m_fieldTypes ??= FieldNames.Select((e, i) =>
      {
        return i switch
        {
          >= 5 => typeof(int),
          _ => typeof(string),
        };
      }).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
    {
      using var e = GetStrings().GetEnumerator();

      while (e.MoveNext())
      {
        var objectArray = new object[e.Current.Length];

        for (var i = objectArray.Length - 1; i >= 0; i--)
        {
          objectArray[i] = i switch
          {
            //var ic when ic >= 5 => System.Int32.Parse(e.Current[i], System.Globalization.NumberStyles.Integer, null),
            _ => e.Current[i],
          };
        }

        yield return objectArray;
      }
    }

    /// <summary>Returns counties all data with the first line being field names.</summary>
    public System.Collections.Generic.IEnumerable<string[]> GetStrings()
    {
      using var sr = new System.IO.StreamReader(Uri.GetStream(), System.Text.Encoding.UTF8);

      foreach (var line in sr.ReadLines(false))
        yield return line.Split(',');
    }
  }
}
