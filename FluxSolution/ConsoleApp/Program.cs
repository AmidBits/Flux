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
    public enum PRODUCT_TYPE
    {
      VER_NT_WORKSTATION = 0x0000001,
      VER_NT_DOMAIN_CONTROLLER = 0x0000002,
      VER_NT_SERVER = 0x0000003
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    public struct OSVERSIONINFOEXW
    {
      public int dwOSVersionInfoSize;
      public int dwMajorVersion;
      public int dwMinorVersion;
      public int dwBuildNumber;
      public int dwPlatformId;
      [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 128)] public string szCSDVersion;
      public ushort wServicePackMajor;
      public ushort wServicePackMinor;
      public ushort wSuiteMask;
      public PRODUCT_TYPE wProductType;
      public byte wReserved;
    }

    [System.Runtime.InteropServices.DllImport("ntdll.dll", SetLastError = true)]
    public static extern bool RtlGetVersion(ref OSVERSIONINFOEXW versionInfo);

    public static OSVERSIONINFOEXW RtlGetVersion()
    {
      var osv = new OSVERSIONINFOEXW();
      osv.dwOSVersionInfoSize = System.Runtime.InteropServices.Marshal.SizeOf(osv);
      RtlGetVersion(ref osv);
      return osv;
    }

    public static string GetOsPlatform()
    {
      var platforms = new string[] { @"Android", @"Browser", @"FreeBSD", @"iOS", @"Linux", @"macOS", @"tvOS", @"watchOS", @"Windows" };

      for (var index = platforms.Length - 1; index >= 0; index--)
        if (System.OperatingSystem.IsOSPlatform(platforms[index]))
          return platforms[index];

      return string.Empty;
    }

    public static Version GetOsPlatformVersion(string platform)
    {
      var major = 0;
      for (; major < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major + 1); major++) ;
      System.Console.Write($"Major = '{major}'");

      var minor = 0;
      for (; minor < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major, minor + 1); minor++) ;
      System.Console.Write($"Minor = '{minor}'");

      var build = 0;
      for (; build < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major, minor, build + 1); build++) ;
      System.Console.Write($"Build = '{build}'");

      var revision = 0;
      for (; revision < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major, minor, build, revision + 1); revision++) ;
      System.Console.Write($"Revision = '{revision}'");

      return new Version(major, minor, build, revision);
    }

    private static void TimedMain(string[] _)
    {
      var platform = GetOsPlatform();
      var version = GetOsPlatformVersion(platform);


      //var e = new Flux.Resources.Ucd.UnicodeData(Flux.Resources.Ucd.UnicodeData.UriLocal).AcquireTabularData().GetEnumerator();
      //e.MoveNext();
      //var names = e.Current.Cast<string>().ToArray();
      //foreach (System.Data.IDataRecord ucd in new Flux.Data.EnumerableDataReader<object[]>(e, 15, (e, i) => names[i], (e, i) => e[i], (e, s) => System.Array.IndexOf(names, s), (e, i) => typeof(string)))
      //{
      //  System.Console.WriteLine(string.Join('|', ucd.GetValues()));
      //}

      foreach (System.Data.IDataRecord ucd in new Flux.Data.EnumerableDataReader<object[]>(new Flux.Resources.Ucd.UnicodeData(Flux.Resources.Ucd.UnicodeData.UriLocal).AcquireTabularData(), 15, true, (e, i) => e[i], (e, i) => typeof(string)))
      {
        //        System.Console.WriteLine(string.Join('|', ucd.GetValues()));
      }

      /*
      for (var i = 0; i <= 360; i += 15)
      {
        var a = Flux.Media.Units.Angle.FromDegree(i);

        var c = Flux.Media.Units.Angle.ConvertRotationAngleToCartesian(a.Radians, out var cx, out var cy);
        var c1 = Flux.Media.Units.Angle.ConvertCartesianToRotationAngle(cx, cy);
        var ce = Flux.Media.Units.Angle.ConvertRotationAngleToCartesianEx(a.Radians, out var cxe, out var cye);
        var ce1 = Flux.Media.Units.Angle.ConvertCartesianToRotationAngleEx(cxe, cye);

        System.Console.WriteLine($"{i} = {a.Degrees:N1} = {a.Radians:N5} = ({c.x:N3}, {c.y:N3} = {c1:N5}) = ({ce.x:N3}, {ce.y:N3} = {ce1:N5})");
      }
      */

      /*
      var allInts = new char[] { 'a', 'b', 'f', 'd', 'd', 'a', 'z', 'z', 'b', 'z', 'd', 'a', 'd', 'b', 'd', 'h', 'a', 'b', 'd' };
      var findInts = new char[] { 'a', 'b', 'd' };

      var im = allInts.GetIndexMap(findInts, c => c);
      System.Console.WriteLine(im.ToDictionary(kvp => kvp.Key, kvp => string.Join(',', kvp.Value)).ToConsoleString());

      // Adjacent List.

      var al = new Flux.Collections.Generic.Graph.AdjacentMatrixTypical<int, int>();

      //al.AddVertex(1);
      //al.AddVertex(2);
      //al.AddVertex(3);
      //al.AddVertex(4);
      //al.AddVertex(5);
      //al.AddVertex(6);

      al.AddEdge(1, 2, 1221);
      al.AddEdge(1, 5, 1551);
      al.AddEdge(2, 3, 2332);
      al.AddEdge(2, 5, 2552);
      al.AddEdge(3, 4, 3443);
      al.AddEdge(4, 5, 4554);
      al.AddEdge(4, 6, 4664);

      al.AddEdge(2, 1, 1221);
      al.AddEdge(5, 1, 1551);
      al.AddEdge(3, 2, 2332);
      al.AddEdge(5, 2, 2552);
      al.AddEdge(4, 3, 3443);
      al.AddEdge(5, 4, 4554);
      al.AddEdge(6, 4, 4664);

      System.Console.WriteLine($"Vertices:");
      var index = 1;
      foreach (var vertex in al.GetVertices())
        System.Console.WriteLine($"{index++}: {vertex}");
      System.Console.WriteLine($"Edges:");
      index = 1;
      foreach (var edge in al.GetEdges().OrderBy(e => e.source).ThenBy(e => e.target))
        System.Console.WriteLine($"{index++}: {edge}");
      */

      /*
      var outer = 6 * 16;
      var inner = outer / 6;

      var array = new int[outer][];

      for (var o = array.GetLength(0) - 1; o >= 0; o--)
      {
        array[o] = new int[1024 * 1024 * 1024 / inner];

        var factorial = Flux.Maths.Factorial(Flux.Random.NumberGenerator.Crypto.Next() & 0x3FFFFFF);

        for (var i = array[o].GetLength(0) - 1; i >= 0; i--)
          array[o][i] = factorial;
      }

      System.Threading.Thread.Sleep(1000);
      */

      /*
      var set = new Flux.Numerics.BigDecimal[] { 1, 2, 2, 3, 5 };
      System.Console.WriteLine($"{System.Environment.NewLine}Set:{System.Environment.NewLine}{string.Join(System.Environment.NewLine, set.Select(v => $"{v:G2}"))}");

      var histogram = set.Histogram(out var sumOfFrequencies);
      System.Console.WriteLine($"{System.Environment.NewLine}Histogram from Set:{System.Environment.NewLine}{histogram.ToConsoleString()}");

      var pmf = histogram.ProbabilityMassFunction(sumOfFrequencies);
      System.Console.WriteLine($"{System.Environment.NewLine}PMF (Probability Mass Function) from Histogram:{System.Environment.NewLine}{pmf.ToConsoleString()}");

      var cdf = histogram.CumulativeMassFunction(sumOfFrequencies);
      System.Console.WriteLine($"{System.Environment.NewLine}CMF (Cumulative Mass Function, a.k.a. CDF) from Histogram:{System.Environment.NewLine}{cdf.ToConsoleString()}");

      var plr = histogram.PercentileRank(sumOfFrequencies);
      System.Console.WriteLine($"{System.Environment.NewLine}Percentile Rank from Histogram:{System.Environment.NewLine}{plr.ToConsoleString()}");

      var pr = set.PercentRank();
      System.Console.WriteLine($"{System.Environment.NewLine}Percent Rank from Set:{System.Environment.NewLine}{string.Join(System.Environment.NewLine, pr)}");
      */

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => RegularForLoop(10, 0.1), 1));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => ParallelForLoop(10, 0.1), 1));
    }

    #region Serial vs. Parallel Loops
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
    #endregion Serial vs. Parallel Loops

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
