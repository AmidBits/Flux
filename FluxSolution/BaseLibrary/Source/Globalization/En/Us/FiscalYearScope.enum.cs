namespace Flux
{
  public static partial class ExtensionMethodsEnUs
  {
    /// <summary>Gets the fiscal year of the specified scope for the datetime.</summary>
    ///[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "Easier to read switch statement")]
    public static int GetFiscalYear(this Globalization.EnUs.FiscalYearScope source, System.DateTime timestamp)
      => source switch
      {
        Globalization.EnUs.FiscalYearScope.OtherStates or Globalization.EnUs.FiscalYearScope.TerritoryOfPuertoRico => timestamp.Month >= 7 ? timestamp.Year + 1 : timestamp.Year,
        Globalization.EnUs.FiscalYearScope.FederalGovernment or Globalization.EnUs.FiscalYearScope.OtherTerritories or Globalization.EnUs.FiscalYearScope.StateOfAlabama or Globalization.EnUs.FiscalYearScope.StateOfMichigan => timestamp.Month >= 10 ? timestamp.Year + 1 : timestamp.Year,
        Globalization.EnUs.FiscalYearScope.StateOfTexas => timestamp.Month >= 9 ? timestamp.Year + 1 : timestamp.Year,
        Globalization.EnUs.FiscalYearScope.StateOfNewYork => timestamp.Month >= 3 ? timestamp.Year + 1 : timestamp.Year,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      };

  }

  namespace Globalization.EnUs
  {
    public enum FiscalYearScope
    {
      FederalGovernment,
      OtherStates,
      OtherTerritories,
      StateOfAlabama,
      StateOfMichigan,
      StateOfNewYork,
      StateOfTexas,
      TerritoryOfPuertoRico,
    }
  }
}
