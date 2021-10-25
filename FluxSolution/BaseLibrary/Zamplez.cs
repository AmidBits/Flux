namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run all zamplez available.</summary>
    public static void Run()
    {
#if ZAMPLEZ
      RunAmb();
      RunArrayRank2();
      RunCoordinateSystems();
      RunLocale();
      RunPhoneticAlgorithms();
      RunRulesEngine();
      RunSetOps();
      RunTemporal();
#else
      throw new System.NotImplementedException(@"/define:ZAMPLEZ");
#endif
    }
  }
}
