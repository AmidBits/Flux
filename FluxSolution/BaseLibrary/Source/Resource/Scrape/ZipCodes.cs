using System.Linq;

namespace Flux.Resources.Scrape
{
  /// <summary>A free zip code file.</summary>
  /// <see cref="http://federalgovernmentzipcodes.us/"/>
  // Download URL: http://federalgovernmentzipcodes.us/free-zipcode-database.csv
  public class ZipCodes
    : ResourceFactory
  {
    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\Scrape\free-zipcode-database.csv");
    public static System.Uri SourceUri
      => new System.Uri(@"http://federalgovernmentzipcodes.us/free-zipcode-database.csv");

    public override System.Collections.Generic.IList<string> FieldNames
      => new string[] { "RecordNumber", "ZipCode", "ZipCodeType", "City", "State", "LocationType", "Lat", "Long", "Xaxis", "Yaxis", "Zaxis", "WorldRegion", "Country", "LocationText", "Location", "Decomissioned", "TaxReturnsFiled", "EstimatedPopulation", "TotalWages", "Notes" };
    public override System.Collections.Generic.IList<System.Type> FieldTypes
      => new System.Type[] { typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(double), typeof(double), typeof(double), typeof(double), typeof(double), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(long), typeof(long), typeof(string) };

    public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
      => uri.GetStream().ReadCsv(new Text.CsvOptions());
  }
}

//System.Console.WriteLine(nameof(Flux.Resources.Scrape.ZipCodes));
//foreach (var strings in new Flux.Resources.Scrape.ZipCodes().GetStrings(Flux.Resources.Scrape.ZipCodes.SourceUri))
//  System.Console.WriteLine(string.Join('|', strings));
