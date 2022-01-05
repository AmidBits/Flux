using System;
using System.Linq;

using Flux;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  public class Program
  {
//    private static void GroupByOrdered<T>(System.Collections.Generic.IEnumerable<T>)

    private static void TimedMain(string[] args)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

      var s = "LCLYTHIGRNIYYGSYLYSETWNTGIMLLLITMATAFMGYVLPWGQMSFWGATVITNLFSAIPYIGTNLV";
      var t = "EWIWGGFSVDKATLNRFFAFHFILPFTMVALAGVHLTFLHETGSNNPLGLTSDSDKIPFHPYYTIKDFLG";

      //s = "seeking";
      //t = "research";

      s = "night";
      t = "nacht";

      //s = "significant";
      //t = "capabilities";

      //s = "aaaaabbbb";
      //t = "ababababa";

      //var mfk = new Flux.Metrical.MostFreqK<char>(2, 10);

      //var r = mfk.GetMeasuredDistance(s, t);

      //WriteMostFreqK(2, 10, "research", "seeking");
      //WriteMostFreqK(2, 10, "night", "nacht");
      //WriteMostFreqK(2, 10, "my", "a");
      //WriteMostFreqK(2, 10, "research", "research");
      //WriteMostFreqK(2, 10, "aaaaabbbb", "ababababa");
      //WriteMostFreqK(2, 10, "significant", "capabilities");

      //WriteMostFreqK(2, 100, "LCLYTHIGRNIYYGSYLYSETWNTGIMLLLITMATAFMGYVLPWGQMSFWGATVITNLFSAIPYIGTNLV", "EWIWGGFSVDKATLNRFFAFHFILPFTMVALAGVHLTFLHETGSNNPLGLTSDSDKIPFHPYYTIKDFLG");

      //static void WriteMostFreqK(int k, int maxDistance, string source, string target)
      //{
      //  var mfk = new Flux.Metrical.MostFreqK<char>(k, maxDistance);
      //  System.Console.WriteLine($"source: \"{source}\"");
      //  System.Console.WriteLine($"target: \"{target}\"");
      //  System.Console.WriteLine($"     k: {k}");
      //  System.Console.WriteLine($"   max: {maxDistance}");
      //  System.Console.WriteLine($" hashS: {string.Concat(mfk.Hashing(source).Select(kvp => $"{kvp.Key}{kvp.Value}"))}");
      //  System.Console.WriteLine($" hashT: {string.Concat(mfk.Hashing(target).Select(kvp => $"{kvp.Key}{kvp.Value}"))}");
      //  System.Console.WriteLine($" score: {mfk.GetMeasuredDistance(source, target)}");
      //  System.Console.WriteLine();
      //}
    }

    private static void Main(string[] args)
    {
      var originalOutputEncoding = System.Console.OutputEncoding;

      try { System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false); }
      catch { System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8; }

      System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} {System.Console.OutputEncoding.HeaderName.ToUpper()} (code page {System.Console.OutputEncoding.CodePage})");
      System.Console.WriteLine();

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      System.Console.OutputEncoding = originalOutputEncoding;

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
