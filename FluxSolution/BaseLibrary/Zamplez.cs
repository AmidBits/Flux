﻿#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run all zamplez available.</summary>
    public static void Run()
    {
      RunAmb();
      RunArrayRank2();
      RunCoordinateSystems();
      RunLocale();
      RunPhoneticAlgorithms();
      RunRulesEngine();
      RunSetOps();
    }
  }
}
#endif