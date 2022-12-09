namespace Flux.Globalization.EnUs
{
  public static partial class FiscalYear
  {
    /// <summary>Gets the fiscal year of the specified scope for the datetime.</summary>
    ///[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "Easier to read switch statement")]
    public static int Get(System.DateTime source, FiscalYearScope scope)
      => scope switch
      {
        FiscalYearScope.OtherStates or FiscalYearScope.TerritoryOfPuertoRico => source.Month >= 7 ? source.Year + 1 : source.Year,
        FiscalYearScope.FederalGovernment or FiscalYearScope.OtherTerritories or FiscalYearScope.StateOfAlabama or FiscalYearScope.StateOfMichigan => source.Month >= 10 ? source.Year + 1 : source.Year,
        FiscalYearScope.StateOfTexas => source.Month >= 9 ? source.Year + 1 : source.Year,
        FiscalYearScope.StateOfNewYork => source.Month >= 3 ? source.Year + 1 : source.Year,
        _ => throw new System.ArgumentOutOfRangeException(nameof(scope)),
      };
  }
}
