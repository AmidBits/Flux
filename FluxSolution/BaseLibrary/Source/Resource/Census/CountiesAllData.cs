namespace Flux.Resources.Census
{
  public static class CountiesAllData
  {
    public static System.Uri UriLocal
      => new System.Uri(@"file://\Resources\Census\cc-est2019-alldata-04.csv");
    public static System.Uri UriSource
      => new System.Uri(@"https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv");

    /// <summary>Census all data.</summary>
    /// <see cref="https://www.census.gov/content/census/en/data/tables/time-series/demo/popest/2010s-counties-detail.html"/>
    // Download URL: https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv
    public static System.Collections.Generic.IEnumerable<string[]> Get(System.Uri uri)
      => uri.GetStream().ReadCsv(new Text.CsvOptions());
  }
}
