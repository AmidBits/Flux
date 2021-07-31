using System;
using System.Buffers;
using System.Linq;
using Flux;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] _)
    {
      var wf = new Flux.WaveForm(1, 1, 0);
      wf.Phase = 0.75;
      var pw = wf.PulseWave();
      var sw = wf.SawWave();
      var sn = wf.SineWave();
      var sq = wf.SquareWave();
      var tr = wf.TriangleWave();

      return;

      //var positive = 16;
      //var pc = Flux.Maths.RoundToInterval(positive, 16, FullRoundingBehavior.FullCeiling);
      //var pf = Flux.Maths.RoundToInterval(positive, 16, FullRoundingBehavior.FullFloor);
      //var ptz = Flux.Maths.RoundToInterval(positive, 16, FullRoundingBehavior.FullTowardZero);
      //var pafz = Flux.Maths.RoundToInterval(positive, 16, FullRoundingBehavior.FullAwayFromZero);
      //System.Console.WriteLine($"(F:{pf}, C:{pc}) TZ:{ptz} < N:{positive} < AFZ:{pafz}");
      //var negative = -positive;
      //var nc = Flux.Maths.RoundToInterval(negative, 16, FullRoundingBehavior.FullCeiling);
      //var nf = Flux.Maths.RoundToInterval(negative, 16, FullRoundingBehavior.FullFloor);
      //var ntz = Flux.Maths.RoundToInterval(negative, 16, FullRoundingBehavior.FullTowardZero);
      //var nafz = Flux.Maths.RoundToInterval(negative, 16, FullRoundingBehavior.FullAwayFromZero);
      //System.Console.WriteLine($"(F:{nf}, C:{nc}) AFZ:{nafz} < N:{negative} < TZ:{ntz}");

      foreach (var block in System.Enum.GetValues(typeof(Flux.Text.UnicodeBlock)).Cast<Flux.Text.UnicodeBlock>().Select(b => (ublock: b, count: b.GetUtf32Last() - b.GetUtf32First())).OrderByDescending(b => b.count))
        System.Console.WriteLine($"{block.ublock.ToString()} = {block.count}");

      System.Console.WriteLine(Flux.Text.UnicodeBlock.MathematicalOperators.ToConsoleTable(0, 0));

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => RegularForLoop(10, 0.1), 1));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => ParallelForLoop(10, 0.1), 1));
    }

    #region Serial vs. Parallel Loops
    //static void RegularForLoop(int taskCount = 10, double taskLoad = 1)
    //{
    //  //var startDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} started at {startDateTime}.");
    //  for (int i = 0; i < taskCount; i++)
    //  {
    //    ExpensiveTask(taskLoad);
    //    //var total = ExpensiveTask(taskLoad);
    //    //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
    //  }
    //  //var endDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} ended at {endDateTime}.");
    //  //var span = endDateTime - startDateTime;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} executed in {span.TotalSeconds} seconds.");
    //  //System.Console.WriteLine();
    //}
    //static void ParallelForLoop(int taskCount = 10, double taskLoad = 1)
    //{
    //  //var startDateTime = DateTime.Now;
    //  System.Threading.Tasks.Parallel.For(0, taskCount, i =>
    //  {
    //    ExpensiveTask(taskLoad);
    //    //var total = ExpensiveTask(taskLoad);
    //    //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
    //  });
    //  //var endDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(ParallelForLoop)} ended at {endDateTime}.");
    //  //var span = endDateTime - startDateTime;
    //  //System.Console.WriteLine($"{nameof(ParallelForLoop)} executed in {span.TotalSeconds} seconds");
    //  //System.Console.WriteLine();
    //}
    //static long ExpensiveTask(double taskLoad = 1)
    //{
    //  var total = 0L;
    //  for (var i = 1; i < int.MaxValue * taskLoad; i++)
    //    total += i;
    //  return total;
    //}
    #endregion Serial vs. Parallel Loops

    static void Main(string[] args)
    {
      var originalOutputEncoding = System.Console.OutputEncoding;

      try
      {
        try
        {
          System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false);
          System.Console.WriteLine("Output encoding set to UTF-16");
        }
        catch
        {
          System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8;
          System.Console.WriteLine("Output encoding set to UTF-8");
        }

        System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} (code page {System.Console.OutputEncoding.CodePage})");
        System.Console.WriteLine();

        System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));
      }
      catch { }
      finally
      {
        Console.OutputEncoding = originalOutputEncoding;
      }

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
