using Flux;
using System;
using System.Linq;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] _)
    {
      // #r "System.Runtime"
      // #r "System.Runtime.Numerics"
      // #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"
      System.Console.WriteLine(Flux.BitOps.BitLength((-670530).ToBigInteger()));
      return;
      for (var index = 0; index < 65; index++)
      {
        System.Console.WriteLine($"number {index} : bl {Flux.BitOps.BitLength((-index).ToBigInteger())} : ppo2 {Flux.BitOps.PreviousPowerOf2(index)} : po2 {Flux.BitOps.PowerOf2(index)} ({/*System.Numerics.BitOperations.*/Flux.BitOps.Log2((uint)index)}) : npo2 {Flux.BitOps.NextPowerOf2(index)}");
      }
      return;

      //var startIndex = 670530;
      //for (index = startIndex; index != startIndex + 30; index++)
      //{
      //  //System.Console.WriteLine($"ILogB: {index} = {System.Math.ILogB(index)} = {Flux.Maths.Log2(index)} = {System.Numerics.BitOperations.PopCount((uint)(1 << (31 - System.Numerics.BitOperations.LeadingZeroCount((uint)index)))-1)}");
      //  //	System.Console.WriteLine($"{index} : {System.Numerics.BitOperations.PopCount((uint)index)} : {Flux.Maths.PopCount(index)}");

      //  System.Console.WriteLine($"{index}");
      //  var value = index;
      //  System.Console.WriteLine($"fPC: {Flux.Maths.PopCount(value)} : sPC: {System.Numerics.BitOperations.PopCount((uint)value)} : BL: {Flux.Maths.BitLength(value)} : X: {value:X4}");
      //  value = (-index);
      //  System.Console.WriteLine($"fPC: {Flux.Maths.PopCount(value)} : sPC: {System.Numerics.BitOperations.PopCount((uint)value)} : BL: {Flux.Maths.BitLength(value)} : X: {value:X4}");
      //}
      //return;
      var ui = 670530203U;
      var bi = System.Numerics.BigInteger.Parse("67053020396705302039670530203967053020396705302039");
      //			bi = 512;
      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => System.Numerics.BitOperations.PopCount(ui)));
      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => System.Math.ILogB(ui)));

      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.PopCount(bi)));
      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.Log2(bi)));

      var n = 8;
      System.Console.WriteLine($"{n} = {Flux.Convert.ToRadixString(n, 2).PadLeft(32, '0')}");
      var nlzc = System.Numerics.BitOperations.LeadingZeroCount((uint)n);
      System.Console.WriteLine($"LZC = {nlzc}");
      var ms1b = 1 << (32 - nlzc);
      System.Console.WriteLine($"{ms1b} = {Flux.Convert.ToRadixString(ms1b, 2).PadLeft(32, '0')} ({ms1b - 1})");
      System.Console.WriteLine($"MS1B: {Flux.BitOps.MostSignificant1Bit(n)}");
      System.Console.WriteLine($"LOG2: {Flux.BitOps.Log2(n)}");
      var nfl = Flux.BitOps.FoldLeastSignificantBits(n);
      System.Console.WriteLine($">>= : {Flux.Convert.ToRadixString(nfl, 2).PadLeft(32, '0')}");
      System.Console.WriteLine($"{System.Numerics.BitOperations.PopCount((uint)nfl) - 1}");
      System.Console.WriteLine($"{(int)System.Math.Log2(nfl)}");
      var nfm = (uint)Flux.BitOps.FoldMostSignificantBits(n);
      System.Console.WriteLine($"<<= : {Flux.Convert.ToRadixString(nfm, 2).PadLeft(32, '0')}");

      using var algo = System.Security.Cryptography.SymmetricAlgorithm.Create(nameof(System.Security.Cryptography.Rijndael));

      algo.SetKeyIV(@"Test", @"Trial", System.Text.Encoding.Unicode);

      var plainText = @"Rob";
      algo.TryEncrypt(plainText, out var encrypted);
      System.Console.WriteLine(encrypted);
      algo.TryDecrypt(encrypted, out var decrypted);
      System.Console.WriteLine(decrypted);

      var a = new char[] { 'a', 'b', 'c', 'd', 'h', 'i', 'j', 'k', 'l' };
      System.Console.WriteLine($"{string.Join('|', a)} ({a.Length})\r\n");
      var ai = new char[] { 'e', 'f', 'g' };
      System.Console.WriteLine($"{string.Join('|', ai)} ({ai.Length})\r\n");

      a.CopyInto(ai, 1, 2);

      //RegularForLoop();
      //ParallelForLoop();
    }

    static void RegularForLoop()
    {
      var startDateTime = DateTime.Now;
      System.Console.WriteLine($"{nameof(RegularForLoop)} started at {startDateTime}.");
      for (int i = 0; i < 10; i++)
      {
        var total = ExpensiveTask();
        System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
      }
      var endDateTime = DateTime.Now;
      System.Console.WriteLine($"{nameof(RegularForLoop)} ended at {endDateTime}.");
      var span = endDateTime - startDateTime;
      System.Console.WriteLine($"{nameof(RegularForLoop)} executed in {span.TotalSeconds} seconds.");
      System.Console.WriteLine();
    }

    static void ParallelForLoop()
    {
      var startDateTime = DateTime.Now;
      System.Console.WriteLine($"{nameof(ParallelForLoop)} started at {startDateTime}.");
      System.Threading.Tasks.Parallel.For(0, 10, i =>
      {
        var total = ExpensiveTask();
        System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
      });
      var endDateTime = DateTime.Now;
      System.Console.WriteLine($"{nameof(ParallelForLoop)} ended at {endDateTime}.");
      var span = endDateTime - startDateTime;
      System.Console.WriteLine($"{nameof(ParallelForLoop)} executed in {span.TotalSeconds} seconds");
      System.Console.WriteLine();
    }

    static long ExpensiveTask()
    {
      var total = 0L;
      for (var i = 1; i < int.MaxValue; i++)
        total += i;
      return total;
    }

    static void Main(string[] args)
    {
      System.Console.InputEncoding = System.Text.Encoding.UTF8;
      System.Console.OutputEncoding = System.Text.Encoding.UTF8;

      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => TimedMain(args), 1));

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
