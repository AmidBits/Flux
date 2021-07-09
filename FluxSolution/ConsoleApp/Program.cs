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
  public interface IM<T> { }

  public interface IX { }
  public class A
    : IX, IM<A>
  {
  }

  public interface IY { }
  public class B
    : A, IY, IM<B>
  {
  }

  public interface IZ { }
  public class C
    : B, IZ, IM<C>
  {
  }

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

    private static void TimedMain(string[] _)
    {
      var point1 = new Flux.Geometry.Point2(0, 10);
      var p1 = point1;
      System.Console.WriteLine(p1);
      var point2 = -(point1 / 2.0);
      System.Console.WriteLine(point2);
      var p2 = point1.PerpendicularCw() - point2;
      System.Console.WriteLine(p2);
      var p3 = point1.PerpendicularCcw() - point2;
      System.Console.WriteLine(p3);

      System.Console.WriteLine();
      var (x1, y1) = Flux.Units.Angle.FromUnitValue(Flux.Units.AngleUnit.Degree, 0).ToCartesianEx();
      var (x2, y2) = Flux.Units.Angle.FromUnitValue(Flux.Units.AngleUnit.Degree, 120).ToCartesianEx();
      var (x3, y3) = Flux.Units.Angle.FromUnitValue(Flux.Units.AngleUnit.Degree, 240).ToCartesianEx();
      var (x4, y4) = Flux.Units.Angle.FromUnitValue(Flux.Units.AngleUnit.Degree, 180).ToCartesianEx();

      x1 *= 10;
      y1 *= 10;
      x2 *= 10;
      y2 *= 10;
      x3 *= 10;
      y3 *= 10;
      x4 *= 10;
      y4 *= 10;

      System.Console.WriteLine($"{x1:N1}, {y1:N1} : {x2:N1}, {y2:N1} : {x3:N1}, {y3:N1}");
      System.Console.WriteLine($"{System.Convert.ToInt32(x4)}, {System.Convert.ToInt32(y4)}");


      foreach (var pnt in Flux.Geometry.Ellipse.CreateCircularArcPoints(4, 10, 10, (x, y) => $"XY = {x:N1}, {y:N1}", 0.0, 0.0))
        System.Console.WriteLine(pnt);

      var c1 = new Flux.Units.Cent(1);
      var c2 = new Flux.Units.Cent(2);
      var c3 = c1 + c2;

      var f1 = Flux.Units.Cent.PitchShift(440, 600);
      var f2 = Flux.Units.Semitone.PitchShift(440, 6);


      var pr10 = Flux.Units.PowerRatio.FromDecibelChange(10);
      var pr8 = Flux.Units.PowerRatio.FromDecibelChange(8);
      var pr7 = Flux.Units.PowerRatio.FromDecibelChange(7);

      var pr25 = pr10 + pr8 + pr7;

      var pr = Flux.Units.PowerRatio.FromDecibelChange(25);
      var ar = Flux.Units.AmplitudeRatio.FromDecibelChange(25);

      //var cad = new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings(new System.Uri(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.LocalFile));

      //var dt = cad.AcquireDataTable();

      //var find = "Sweden";

      //foreach (var dr in dt.Rows.Cast<System.Data.DataRow>())
      //{
      //  var s0 = dr[0].ToString();
      //  var s1 = dr[1].ToString();

      //  //if (s0.Contains(find, StringComparison.InvariantCultureIgnoreCase) || s1.Contains(find, StringComparison.InvariantCultureIgnoreCase))
      //  {
      //    System.Console.Clear();
      //    System.Console.WriteLine($"<{s0}>");
      //    System.Console.WriteLine(s1);
      //    System.Console.ReadKey();
      //  }
      //}

      //return;

      //var s = "A𠈓B\u0061C\u0061\u0301D\U0001F469\U0001F3FD\u200D\U0001F692E";

      //var gs = Flux.Text.GraphemeCluster.GetAll(s).ToArray();

      ////s = "\uD83D\uDC69\uD83C\uDFFD\u200D\uD83D\uDE92";
      //System.Console.WriteLine(s.Length);

      //using var sr = new System.IO.StringReader(s);
      //using var tee = new Flux.Text.GraphemeEnumerator(sr, 8);

      //var index = 0;
      //foreach (var te in tee)
      //  System.Console.WriteLine($"{index++} : {te}");
      ////System.Console.WriteLine($"{index++} : {te} ({te.Utf16SequenceLength})");

      //return;

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
      System.Console.InputEncoding = System.Text.Encoding.UTF8;
      System.Console.OutputEncoding = System.Text.Encoding.UTF8;

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
