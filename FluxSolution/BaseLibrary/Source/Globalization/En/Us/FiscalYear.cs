namespace Flux
{
  public static partial class ExtensionMethodsEnUs
  {
    /// <summary>Gets the fiscal year of the specified scope for the datetime.</summary>
    ///[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "Easier to read switch statement")]
    public static int GetFiscalYear(this System.DateTime source, Globalization.EnUs.FiscalYearScope scope)
      => scope switch
      {
        Globalization.EnUs.FiscalYearScope.OtherStates or Globalization.EnUs.FiscalYearScope.TerritoryOfPuertoRico => source.Month >= 7 ? source.Year + 1 : source.Year,
        Globalization.EnUs.FiscalYearScope.FederalGovernment or Globalization.EnUs.FiscalYearScope.OtherTerritories or Globalization.EnUs.FiscalYearScope.StateOfAlabama or Globalization.EnUs.FiscalYearScope.StateOfMichigan => source.Month >= 10 ? source.Year + 1 : source.Year,
        Globalization.EnUs.FiscalYearScope.StateOfTexas => source.Month >= 9 ? source.Year + 1 : source.Year,
        Globalization.EnUs.FiscalYearScope.StateOfNewYork => source.Month >= 3 ? source.Year + 1 : source.Year,
        _ => throw new System.ArgumentOutOfRangeException(nameof(scope)),
      };
  }
}
