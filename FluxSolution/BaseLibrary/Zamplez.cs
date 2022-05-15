namespace Flux
{
  public static partial class Zamplez
  {
    public static bool IsSupported
#if ZAMPLEZ
      => true;
#else
      => false;
#endif

    /// <summary>Run all zamplez available.</summary>
    public static void Run()
    {
#if ZAMPLEZ
      RunAmbOperator();
      RunArrayRank2();
      RunColors();
      RunCoordinateSystems();
      RunDataStructuresGraphs();
      RunImmutableDataStructures();
      RunLocale();
      ParallelVsSerial.Run();
      RunPhoneticAlgorithms();
      RunReflection();
      RunResource();
      RunRulesEngine();
      RunSetOps();
      RunStatistics();
      RunTemporal();
#else
      throw new System.NotImplementedException(@"/define:ZAMPLEZ");
#endif
    }

#if ZAMPLEZ
    private static class ParallelVsSerial
    {
      public static void Run()
      {
        System.Console.WriteLine();
        System.Console.WriteLine($"{nameof(ParallelVsSerial)} comparison:");
        System.Console.WriteLine();
        System.Console.WriteLine(Flux.Services.Performance.Measure(() => RegularForLoop(10, 0.05), 1));
        System.Console.WriteLine(Flux.Services.Performance.Measure(() => ParallelForLoop(10, 0.05), 1));
        System.Console.WriteLine();
      }

      static void RegularForLoop(int taskCount = 10, double taskLoad = 1)
      {
        //var startDateTime = DateTime.Now;
        //System.Console.WriteLine($"{nameof(RegularForLoop)} started at {startDateTime}.");
        for (int i = 0; i < taskCount; i++)
        {
          ExpensiveTask(taskLoad);
          //var total = ExpensiveTask(taskLoad);
          //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
        }
        //var endDateTime = DateTime.Now;
        //System.Console.WriteLine($"{nameof(RegularForLoop)} ended at {endDateTime}.");
        //var span = endDateTime - startDateTime;
        //System.Console.WriteLine($"{nameof(RegularForLoop)} executed in {span.TotalSeconds} seconds.");
        //System.Console.WriteLine();
      }
      static void ParallelForLoop(int taskCount = 10, double taskLoad = 1)
      {
        //var startDateTime = DateTime.Now;
        System.Threading.Tasks.Parallel.For(0, taskCount, i =>
        {
          ExpensiveTask(taskLoad);
          //var total = ExpensiveTask(taskLoad);
          //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
        });
        //var endDateTime = DateTime.Now;
        //System.Console.WriteLine($"{nameof(ParallelForLoop)} ended at {endDateTime}.");
        //var span = endDateTime - startDateTime;
        //System.Console.WriteLine($"{nameof(ParallelForLoop)} executed in {span.TotalSeconds} seconds");
        //System.Console.WriteLine();
      }
      static long ExpensiveTask(double taskLoad = 1)
      {
        var total = 0L;
        for (var i = 1; i < int.MaxValue * taskLoad; i++)
          total += i;
        return total;
      }
    }
#endif
  }
}
