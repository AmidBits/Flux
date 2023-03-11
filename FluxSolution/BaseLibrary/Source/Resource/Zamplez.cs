#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunResource()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunResource));
      System.Console.WriteLine();

      var tdas = new ITabularDataAcquirable[]
      {
        new Flux.Resources.Census.CountiesAllData(new System.Uri(Flux.Resources.Census.CountiesAllData.LocalFile)),
        new Flux.Resources.Scrape.ZipCodes(new System.Uri(Flux.Resources.Scrape.ZipCodes.LocalFile)),
        new Flux.Resources.Ucd.UnicodeData(new System.Uri(Flux.Resources.Ucd.UnicodeData.LocalFile)),
        new Flux.Resources.Ucd.Blocks(new System.Uri(Flux.Resources.Ucd.Blocks.LocalFile)),
        new Flux.Resources.Scowl.TwoOfTwelveFull(new System.Uri(Flux.Resources.Scowl.TwoOfTwelveFull.LocalFile)),
        new Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows(new System.Uri(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows.LocalFile)),
        new Flux.Resources.ProjectGutenberg.TableOfContents(new System.Uri(Flux.Resources.ProjectGutenberg.TableOfContents.LocalFile)),
        new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings(new System.Uri(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.LocalFile)),
        new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary(new System.Uri(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.LocalFile)),
        new Flux.Resources.W3c.NamedCharacterReferences(new System.Uri(Flux.Resources.W3c.NamedCharacterReferences.LocalFile)),
        new Flux.Resources.DotNet.FxSequence(TimeZoneInfo.GetSystemTimeZones()),
      };

      foreach (var tda in tdas)
      {
        var fn = tda.FieldNames;
        var ft = tda.FieldTypes;
        var fv = tda.GetFieldValues();
        var dr = new Flux.Data.EnumerableTabularDataReader(fv, fn);
        var dt = dr.ToDataTable(tda.GetType().Name);

        System.Console.WriteLine($"'{dt.TableName}' with {dt.Columns.Count} columns ({(ft.Distinct().Count() is var types ? types : types)} {(types > 1 ? "types" : "type")}) and {dt.Rows.Count} rows.");
      }
    }
  }
}
#endif
