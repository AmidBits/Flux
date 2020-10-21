namespace Flux.Resources.Census
{
  /// <summary>The Unicode block database.</summary>
  /// <see cref="https://www.census.gov/content/census/en/data/tables/time-series/demo/popest/2010s-counties-detail.html"/>
  // Download URL: https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv
  public class CountiesAllData
    : Conversions
  {
    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\Census\cc-est2019-alldata-04.csv");
    public static System.Uri SourceUri
      => new System.Uri(@"https://www2.census.gov/programs-surveys/popest/datasets/2010-2019/counties/asrh/cc-est2019-alldata-04.csv");

    public override System.Collections.Generic.IList<string> FieldNames
      => new string[] { "SUMLEV", "STATE", "COUNTY", "STNAME", "CTYNAME", "YEAR", "AGEGRP", "TOT_POP", "TOT_MALE", "TOT_FEMALE", "WA_MALE", "WA_FEMALE", "BA_MALE", "BA_FEMALE", "IA_MALE", "IA_FEMALE", "AA_MALE", "AA_FEMALE", "NA_MALE", "NA_FEMALE", "TOM_MALE", "TOM_FEMALE", "WAC_MALE", "WAC_FEMALE", "BAC_MALE", "BAC_FEMALE", "IAC_MALE", "IAC_FEMALE", "AAC_MALE", "AAC_FEMALE", "NAC_MALE", "NAC_FEMALE", "NH_MALE", "NH_FEMALE", "NHWA_MALE", "NHWA_FEMALE", "NHBA_MALE", "NHBA_FEMALE", "NHIA_MALE", "NHIA_FEMALE", "NHAA_MALE", "NHAA_FEMALE", "NHNA_MALE", "NHNA_FEMALE", "NHTOM_MALE", "NHTOM_FEMALE", "NHWAC_MALE", "NHWAC_FEMALE", "NHBAC_MALE", "NHBAC_FEMALE", "NHIAC_MALE", "NHIAC_FEMALE", "NHAAC_MALE", "NHAAC_FEMALE", "NHNAC_MALE", "NHNAC_FEMALE", "H_MALE", "H_FEMALE", "HWA_MALE", "HWA_FEMALE", "HBA_MALE", "HBA_FEMALE", "HIA_MALE", "HIA_FEMALE", "HAA_MALE", "HAA_FEMALE", "HNA_MALE", "HNA_FEMALE", "HTOM_MALE", "HTOM_FEMALE", "HWAC_MALE", "HWAC_FEMALE", "HBAC_MALE", "HBAC_FEMALE", "HIAC_MALE", "HIAC_FEMALE", "HAAC_MALE", "HAAC_FEMALE", "HNAC_MALE", "HNAC_FEMALE" };
    public override System.Collections.Generic.IList<System.Type> FieldTypes
      => new System.Type[] { typeof(int), typeof(int), typeof(int), typeof(string), typeof(string), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) };

    public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
    {
      using var r = new Text.CsvReader(uri.GetStream(), new Text.CsvOptions());
      using var e = r.ReadArrays().GetEnumerator();

      if (e.MoveNext()) // Skip the column headers.
      {
        while (e.MoveNext())
        {
          yield return e.Current;
        }
      }
    }
  }
}
