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
      var rgb = Flux.Colors.Rgb.Random();
      System.Console.WriteLine($"{rgb}\r\n");
      var cmyk = rgb.ToCmyk();
      System.Console.WriteLine($"{cmyk}\r\n {cmyk.ToRgb()}\r\n");
      //var hsi = rgb.ToHsi();
      //System.Console.WriteLine($"{hsi}\r\n {hsi.ToRgb()}\r\n");
      var hsl = rgb.ToHsl();
      System.Console.WriteLine($"{hsl}\r\n {hsl.ToRgb()}\r\n");
      var hsv = rgb.ToHsv();
      System.Console.WriteLine($"{hsv}\r\n {hsv.ToRgb()}\r\n {hsv.ToHwb()}\r\n");
      var hwb = rgb.ToHwb();
      System.Console.WriteLine($"{hwb}\r\n {hwb.ToRgb()}\r\n {hwb.ToHsv()}\r\n");

      //Flux.Colors.
      //var k = 2`
      //var n = 3;

      //var alphabet = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
      ////alphabet = new string[] { "a", "b", "c", "d" };

      //var dbs = Flux.Maths.GetDeBruijnSequence(k, n, alphabet).Take((int)System.Math.Pow(k, n)).ToArray();
      //var dbs2 = Flux.Maths.GetDeBruijnSequence2(k, n, alphabet).ToList();
      //var dbx = dbs.PartitionNgram(n, (e, i) => string.Concat(e.Select(i => alphabet[i]))).ToList();
      //var edbs = Flux.Maths.ExpandDeBruijnSequence(dbs, k, n).ToList();
      //var ddbs = Flux.Maths.DecodeDeBruijnSequence(dbs, k, n, b => string.Join(string.Empty, b.Select(i => alphabet[i]))).ToList();
      //var ddbsa = Flux.Maths.DecodeDeBruijnSequence(dbs, k, n, alphabet).ToList();

      //System.Console.WriteLine($"   Bruijn: {dbs.Count}");
      //System.Console.WriteLine($"First 130: {string.Join(string.Empty, dbs.Take(130))}");
      //System.Console.WriteLine($" Last 130: {string.Join(string.Empty, dbs.TakeLast(130))}");
      //dbs[4443] = 255;
      ////			var dbd = Flux.Maths.DecodeDeBruijnSequence(dbs, k, n, b => string.Join(string.Empty, b.Where(n => n >= 0 && n < 255))).ToArray();
      ////		var dbcs = dbd.Histogram((e, i) => e);
      ////dbs[4443] = (byte)255;0
      ////System.Console.WriteLine($"\"abcd\": {string.Join(string.Empty, dbs.Select(n => (char)(n + 'a')))}");

      //var dbc = Flux.Maths.GetDeBruijnCounts(dbs, k, n);
      //System.Console.WriteLine($"Counts: {dbc.Count} ({string.Join(',', dbc.Select((kvp, i) => (s: $"{i,4} ({string.Join(',', kvp.Key)})", count: kvp.Value)).Where(sc => sc.count != 1))})");
      //var dbe = Flux.Maths.GenerateDeBruijnErrors(dbs, k, n);
      //System.Console.WriteLine($"Errors: {dbe.Count} ({string.Join(',', dbe)})");

      //var db = Flux.Maths.GetDeBruijnSequence(10, 4, '0', '1', '2', '3', '4', '5', '6', '7', '8', '9').ToList();
      //var db = new Flux.MoserDeBruijnSequence().GetThem(30).ToList();
      //			db.Reverse();
      //System.Console.WriteLine($"({x.Count}) {string.Join(string.Empty, x)}");
      //Flux.Maths.ValidateDeBruijn(x, 10, 4);

      //var m = new string[8, 8];

      //for (var r = 0; r < 8; r++)
      //{
      //  for (var c = 0; c < 8; c++)
      //  {
      //    m[r, c] = "  ";
      //  }
      //}

      //for (var r = 0; r < 3; r++)
      //{
      //  for (var c = 0; c < 3; c++)
      //  {
      //    m[r, c] = $"{(char)(r + 'A')}{(char)(c + 'A')}";
      //  }
      //}

      //System.Console.WriteLine(m.ToConsoleString());
      //System.Console.WriteLine();

      //m = m.ToArray(1, 1, 2, 2, 1, 1, 1, 1);

      //System.Console.WriteLine(m.ToConsoleString());

      //var al = new Flux.Collections.Generic.Graph.AdjacentList<char, int>();

      //al.AddVertex('a');
      //al.AddVertex('b');
      //al.AddVertex('c');
      //al.AddVertex('d');

      ////g.AddDirectedEdge('a', 'b', 1);
      ////g.AddDirectedEdge('a', 'c', 1);
      ////g.AddDirectedEdge('b', 'a', 1);
      ////g.AddDirectedEdge('b', 'c', 1);
      ////g.AddDirectedEdge('c', 'a', 1);
      ////g.AddDirectedEdge('c', 'b', 1);
      ////g.AddDirectedEdge('c', 'd', 1);
      ////g.AddDirectedEdge('d', 'c', 1);

      //al.AddUndirectedEdge('a', 'b', 1);
      //al.AddUndirectedEdge('a', 'c', 1);
      //al.AddUndirectedEdge('b', 'c', 1);
      //al.AddUndirectedEdge('c', 'd', 1);

      //al.RemoveUndirectedEdge('c', 'b', 1);

      //System.Console.WriteLine(al.ToString());



      //var am = new Flux.Collections.Generic.Graph.AdjacentMatrix<char, int>();

      //am.AddVertex('a');
      //am.AddVertex('b');
      //am.AddVertex('c');
      //am.AddVertex('d');

      ////am.AddDirectedEdge('a', 'b', 1);
      ////am.AddDirectedEdge('a', 'c', 1);
      ////am.AddDirectedEdge('b', 'a', 1);
      ////am.AddDirectedEdge('b', 'c', 1);
      ////am.AddDirectedEdge('c', 'a', 1);
      ////am.AddDirectedEdge('c', 'b', 1);
      ////am.AddDirectedEdge('c', 'd', 1);
      ////am.AddDirectedEdge('d', 'c', 1);

      //am.AddUndirectedEdge('a', 'b', 1);
      //am.AddUndirectedEdge('a', 'c', 1);
      //am.AddUndirectedEdge('b', 'c', 1);
      //am.AddUndirectedEdge('c', 'd', 1);

      //am.RemoveUndirectedEdge('c', 'b', 1);

      //System.Console.WriteLine(am.ToConsoleString());



      //foreach (var edge in g.GetEdges())
      //  System.Console.WriteLine(edge);

      /*
      var ipaes = Flux.Reflection.ApplicationDomain.GetTypesImplementingInterface<Flux.Text.IPhoneticAlgorithmEncoder>().Select(t => (Flux.Text.IPhoneticAlgorithmEncoder)System.Activator.CreateInstance(t));
      var names = new string[] { "Dougal", "Glinde", "Plumridge", "Simak", "Webberley" };

      foreach (var ipae in ipaes)
        foreach (var name in names)
          System.Console.WriteLine($"{ipae.GetType().Name}.\"{name}\", \"{ipae.EncodePhoneticAlgorithm(name)}\"");
      */

      /*
      var data1 = new Flux.Resources.Census.CountiesAllData(Flux.Resources.Census.CountiesAllData.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.Census.CountiesAllData)} = {data1.GetLength(0).ToGroupString()} rows, {data1[0].GetLength(0)} columns = {System.DateTime.Now}");
      var data2 = new Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows)} = {data2.GetLength(0).ToGroupString()} rows, {data2[0].GetLength(0)} columns {System.DateTime.Now}");
      var data3 = new Flux.Resources.ProjectGutenberg.TableOfContents(Flux.Resources.ProjectGutenberg.TableOfContents.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.ProjectGutenberg.TableOfContents)} = {data3.GetLength(0).ToGroupString()} rows, {data3[0].GetLength(0)} columns {System.DateTime.Now}");
      var data4 = new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings)} = {data4.GetLength(0).ToGroupString()} rows, {data4[0].GetLength(0)} columns {System.DateTime.Now}");
      var data5 = new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary)} = {data5.GetLength(0).ToGroupString()} rows, {data5[0].GetLength(0)} columns {System.DateTime.Now}");
      var data6 = new Flux.Resources.Scowl.TwoOfTwelveFull(Flux.Resources.Scowl.TwoOfTwelveFull.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.Scowl.TwoOfTwelveFull)} = {data6.GetLength(0).ToGroupString()} rows, {data6[0].GetLength(0)} columns {System.DateTime.Now}");
      var data7 = new Flux.Resources.Scrape.ZipCodes(Flux.Resources.Scrape.ZipCodes.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.Scrape.ZipCodes)} = {data7.GetLength(0).ToGroupString()} rows, {data7[0].GetLength(0)} columns {System.DateTime.Now}");
      var data8 = new Flux.Resources.Ucd.Blocks(Flux.Resources.Ucd.Blocks.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.Ucd.Blocks)} = {data8.GetLength(0).ToGroupString()} rows, {data8[0].GetLength(0)} columns {System.DateTime.Now}");
      var data9 = new Flux.Resources.Ucd.UnicodeData(Flux.Resources.Ucd.UnicodeData.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.Ucd.UnicodeData)} = {data9.GetLength(0).ToGroupString()} rows, {data9[0].GetLength(0)} columns {System.DateTime.Now}");
      var data0 = new Flux.Resources.W3c.NamedCharacterReferences(Flux.Resources.W3c.NamedCharacterReferences.UriLocal).AcquireTabularData().ToArray();
      System.Console.WriteLine($"{nameof(Flux.Resources.W3c.NamedCharacterReferences)} = {data0.GetLength(0).ToGroupString()} rows, {data0[0].GetLength(0)} columns {System.DateTime.Now}");
      */

      /*
      using (var sr = new System.IO.StreamReader(@"C:\Test\Xml.xml"))
        foreach (var rune in sr.EnumerateTextElements())
          System.Console.Write(rune.ToString());

      using (var sr = new System.IO.StreamReader(@"C:\Test\Xml.xml"))
        foreach (var rune in sr.EnumerateRunes())
          System.Console.Write(rune.ToString());
      */

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => RegularForLoop(10, 0.1), 1));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => ParallelForLoop(10, 0.1), 1));
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
