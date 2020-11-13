namespace Flux.Resources
{
  /// <summary></summary>
  public abstract class Conversions
  {
    public abstract System.Collections.Generic.IList<string> FieldNames { get; }
    public abstract System.Collections.Generic.IList<System.Type> FieldTypes { get; }

    /// <summary>This is an abstract method for basic string data enumeration. This is considered the 'first' layer of data enumeration.</summary>
    public abstract System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri);

    /// <summary>This is a virtual method for basic object data enumeration. By default the data from the method GetStrings() is converted using the method ConvertStringToObject(). This can be considered the 'second' layer of data enumeration. Override in derived class for complete control, e.g. make this the 'first' layer (skipping the GetStrings() altogether.</summary>
    public virtual System.Collections.Generic.IEnumerable<object[]> GetObjects(System.Uri uri)
      => GetStrings(uri).ToTypedObjects((string value, int index) =>
      {
        if (FieldTypes is not null && index >= 0 && index < FieldTypes.Count && FieldTypes[index] != typeof(string)) // Are we converting?
          return string.IsNullOrEmpty(value) ? Reflection.Helper.GetDefaultValue(FieldTypes[index]) ?? System.DBNull.Value : Convert.ChangeType(value, null, FieldTypes[index]); // Either convert the value to the specified field type, or if the value is empty (or externally null) then return the default for the specified type.

        return value; // Either we have no field type, or the field type is a string, so we simply return the value, as is.
      });

    /// <summary>Returns data as an IDataReader, by using GetObjects (for better type matching). An IDataReader can be passed to a SqlBulkCopy for import to SQL Server.</summary>
    public Data.EnumerableDataReader GetDataReader(System.Uri uri)
      => new Data.EnumerableDataReader(GetObjects(uri), FieldNames, FieldTypes);

    /// <summary>Returns the data in a DataTable, by using GetObjects (for better type matching).</summary>
    public System.Data.DataTable GetDataTable(System.Uri uri)
      => GetObjects(uri).ToDataTable(GetType().Name, FieldNames, FieldTypes);
  }
}

/*
 * Example (using in-assembly functionality):
 
#region Local URIs
System.Console.WriteLine(nameof(Flux.Resources.Census.CountiesAllData));
foreach (var strings in new Flux.Resources.Census.CountiesAllData().GetStrings(Flux.Resources.Census.CountiesAllData.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows));
foreach (var strings in new Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows().GetStrings(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TableOfContents));
foreach (var strings in new Flux.Resources.ProjectGutenberg.TableOfContents().GetStrings(Flux.Resources.ProjectGutenberg.TableOfContents.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings));
foreach (var strings in new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings().GetStrings(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary));
foreach (var strings in new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary().GetStrings(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Scowl.TwoOfTwelveFull));
foreach (var strings in new Flux.Resources.Scowl.TwoOfTwelveFull().GetStrings(Flux.Resources.Scowl.TwoOfTwelveFull.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Scrape.ZipCodes));
foreach (var strings in new Flux.Resources.Scrape.ZipCodes().GetStrings(Flux.Resources.Scrape.ZipCodes.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Ucd.Blocks));
foreach (var strings in new Flux.Resources.Ucd.Blocks().GetStrings(Flux.Resources.Ucd.Blocks.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.Ucd.UnicodeData));
foreach (var strings in new Flux.Resources.Ucd.UnicodeData().GetStrings(Flux.Resources.Ucd.UnicodeData.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.W3c.NamedCharacterReferences));
foreach (var strings in new Flux.Resources.W3c.NamedCharacterReferences().GetStrings(Flux.Resources.W3c.NamedCharacterReferences.LocalUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
#endregion

#region Source URIs
System.Console.WriteLine(nameof(Flux.Resources.Census.CountiesAllData));
foreach (var strings in new Flux.Resources.Census.CountiesAllData().GetStrings(Flux.Resources.Census.CountiesAllData.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows));
foreach (var strings in new Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows().GetStrings(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TableOfContents));
foreach (var strings in new Flux.Resources.ProjectGutenberg.TableOfContents().GetStrings(Flux.Resources.ProjectGutenberg.TableOfContents.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings));
foreach (var strings in new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings().GetStrings(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary));
foreach (var strings in new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary().GetStrings(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Scowl.TwoOfTwelveFull));
foreach (var strings in new Flux.Resources.Scowl.TwoOfTwelveFull().GetStrings(Flux.Resources.Scowl.TwoOfTwelveFull.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Scrape.ZipCodes));
foreach (var strings in new Flux.Resources.Scrape.ZipCodes().GetStrings(Flux.Resources.Scrape.ZipCodes.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.Ucd.Blocks));
foreach (var strings in new Flux.Resources.Ucd.Blocks().GetStrings(Flux.Resources.Ucd.Blocks.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
System.Console.WriteLine(nameof(Flux.Resources.Ucd.UnicodeData));
foreach (var strings in new Flux.Resources.Ucd.UnicodeData().GetStrings(Flux.Resources.Ucd.UnicodeData.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));

System.Console.WriteLine(nameof(Flux.Resources.W3c.NamedCharacterReferences));
foreach (var strings in new Flux.Resources.W3c.NamedCharacterReferences().GetStrings(Flux.Resources.W3c.NamedCharacterReferences.SourceUri).Take(1))
  System.Console.WriteLine(string.Join('|', strings));
#endregion
*/
