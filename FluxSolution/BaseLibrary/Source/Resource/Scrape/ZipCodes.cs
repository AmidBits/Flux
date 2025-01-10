namespace Flux
{
  public static partial class Resource
  {
    #region Scrape ZipCode

    /// <summary>
    /// <para>A free zip code file.</para>
    /// <para><see href="http://federalgovernmentzipcodes.us/"/></para>
    /// <para>Remote: <see href="http://federalgovernmentzipcodes.us/free-zipcode-database.csv"/></para>
    /// <para>Local: <see href="file://\Resources\Scrape\free-zipcode-database.csv"/></para>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<string[]> GetScrapeZipCodeDatabase(string file = @"file://\Resources\Scrape\free-zipcode-database.csv")
    {
      using var stream = new System.Uri(file).GetStream();
      using var reader = new StreamReader(stream);

      foreach (var fields in reader.ReadCsv())
        yield return fields;
    }

    #endregion // Scrape ZipCode
  }
}
