namespace Flux.Resources1.Scrape
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
    public static System.Collections.Generic.IEnumerable<string[]> Get(System.Uri uri)
      => uri.GetStream().ReadCsv(new Text.CsvOptions());
  }
}
