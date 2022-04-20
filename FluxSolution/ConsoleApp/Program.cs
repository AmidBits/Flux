using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Flux;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  public sealed class Histogram<TKey>
    : System.Collections.Generic.IReadOnlyDictionary<TKey, int>
  {
    private readonly System.Collections.Generic.SortedDictionary<TKey, int> m_bins;



    public void Add(TKey key)
    {
      if (ContainsKey(key))
        m_bins[key]++;
      else
        m_bins.Add(key, 1);
    }
    public void Add(System.Collections.Generic.IEnumerable<TKey> keys)
    {
      foreach (var key in keys)
        Add(key);
    }
    public void Add(TKey key, int count)
    {
      if (TryGetValue(key, out var currentCount))
        m_bins[key] = currentCount + count;
      else
        m_bins.Add(key, currentCount);
    }

    // IReadOnlyDictionary<>
    public int this[TKey key] => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins)[key];
    public System.Collections.Generic.IEnumerable<TKey> Keys => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins).Keys;
    public System.Collections.Generic.IEnumerable<int> Values => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins).Values;
    public int Count => ((System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<TKey, int>>)m_bins).Count;
    public bool ContainsKey(TKey key) => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins).ContainsKey(key);
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, int>> GetEnumerator() => ((System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, int>>)m_bins).GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => ((System.Collections.IEnumerable)m_bins).GetEnumerator();
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out int value) => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins).TryGetValue(key, out value);
  }

  public class Program
  {

    private static void TimedMain(string[] args)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }



      var a = new int[] { 13, 12, 11, 8, 4, 3, 2, 1, 1, 1 };
      a = a.Reverse().ToArray();

      var h = new Histogram<int>();
      h.

      //var lower = 0;
      //var middle = 0;
      //var upper = 0;

      // var values = new int[] { 8, 9, 10, 11, 11, 11, 11, 12, 12, 12, 13 };
      // var values = new int[] { 2, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5 };
      // var values = new int[] { 2, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5 };
      //var values = a;// new int[] { 13, 17, 17, 18, 19, 19, 19, 21, 21, 23, 24 };

      //var n = values.Length;
      //var index = 0.0;
      //foreach (var i in values)
      //{
      //  //var pr = ((values.Count(v => v < i) + (.5 * values.Count(v => v == i))) / n);
      //  var pr = index / (index + (values.Length - index - 1));
      //  if (pr < .27)
      //    lower += 1;
      //  else if (pr > .73)
      //    upper += 1;
      //  else
      //    middle += 1;
      //  index++;
      //}

      //System.Console.WriteLine("Upper: " + upper);
      //System.Console.WriteLine("Middle: " + middle);
      //System.Console.WriteLine("Lower: " + lower);

      System.Console.WriteLine(string.Join(System.Environment.NewLine, a.ToHistogram((e, i) => e, out var sum1).CumulativeMassFunction(sum1)));
      System.Console.WriteLine(string.Join(System.Environment.NewLine, a.ToHistogram((e, i) => e, out var sum2).PercentRank(sum2)));
      System.Console.WriteLine(string.Join(System.Environment.NewLine, a.ToHistogram((e, i) => e, out var sum3).PercentileRank(sum3)));
      //return;

      var b = a.Select((e, i) => System.Collections.Generic.KeyValuePair.Create(e, (double)i / (double)(i + (a.Length - i - 1)))).ToArray();
      System.Console.WriteLine(string.Join(System.Environment.NewLine, b));

      var e = b.ExtremaClosestToKey(t => t.Key, 5);
      System.Console.WriteLine(e);

      var ipx = LinearInterpolation.ImputeUnit(e.elementLt.Key, e.elementGt.Key, 5);

      var ip = LinearInterpolation.Interpolate(5, e.elementGt.Value, e.elementLt.Value);
      System.Console.WriteLine(ip);


      System.Console.WriteLine(string.Join(System.Environment.NewLine, a.ToHistogram((e, i) => e, out var sum).CumulativeMassFunction(sum)));
      return;

      var enumElements = Flux.AssemblyInfo.Flux.Assembly.GetTypes().Where(t => t.IsEnum).ToArray();
      foreach (var enumType in enumElements)
        System.Console.WriteLine(enumType.FullName);
      return;

      System.Console.Write($"It's a ");
      Flux.Console.WriteError($"{nameof(Flux.Console.WriteError)}");
      System.Console.Write($" and some ");
      Flux.Console.WriteInformation($"{nameof(Flux.Console.WriteInformation)}");
      System.Console.Write($" with a bit of ");
      Flux.Console.WriteSuccess($"{nameof(Flux.Console.WriteSuccess)}");
      System.Console.Write($" as well as a ");
      Flux.Console.WriteWarning($"{nameof(Flux.Console.WriteWarning)}");
      System.Console.WriteLine($".");

      foreach (System.ConsoleColor color in System.Enum.GetValues(typeof(System.ConsoleColor)))
      {
        System.Console.ForegroundColor = color;
        System.Console.WriteLine(color.ToString());
        System.Console.ResetColor();
      }
    }

    private static void Main(string[] args)
    {
      var originalOutputEncoding = SetEncoding();

      SetSize(0.9);

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      ResetEncoding(originalOutputEncoding);

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();

      #region Support functions
      static void ResetEncoding(System.Text.Encoding originalOutputEncoding)
      {
        System.Console.OutputEncoding = originalOutputEncoding;
      }
      static System.Text.Encoding SetEncoding()
      {
        var originalOutputEncoding = System.Console.OutputEncoding;

        try { System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false); }
        catch { System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8; }

        System.Console.ForegroundColor = System.ConsoleColor.Blue;
        System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} {System.Console.OutputEncoding.HeaderName.ToUpper()} (code page {System.Console.OutputEncoding.CodePage})");
        System.Console.ResetColor();

        return originalOutputEncoding;
      }
      static void SetSize(double percentOfLargestWindowSize)
      {
        if (System.OperatingSystem.IsWindows())
        {
          if (percentOfLargestWindowSize < 0.1 || percentOfLargestWindowSize >= 1.0) throw new System.ArgumentOutOfRangeException(nameof(percentOfLargestWindowSize));

          var width = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowWidth * percentOfLargestWindowSize), System.Console.LargestWindowWidth), short.MaxValue);
          var height = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowHeight * percentOfLargestWindowSize), System.Console.LargestWindowHeight), short.MaxValue);

          System.Console.SetWindowSize(width, height);
        }
      }
      #endregion Support functions
    }
  }
}
