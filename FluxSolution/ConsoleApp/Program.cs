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


      var d = Flux.Globalization.EnUs.Language.RelativeFrequencyOfLetters.ToDictionary(kvp => kvp.Key, kvp => 0);
      for (var i = 0; i < 100000; i++)
        d[Flux.Random.NumberGenerator.Crypto.NextProbabilityRuneEnUs()]++;
      foreach (var kvp in d.Take(7))
        System.Console.WriteLine($"'{kvp.Key}' = {kvp.Value}");

      for (var i = 0; i < 7; i++)
        System.Console.WriteLine(Flux.Random.NumberGenerator.Pseudo.NextRandomNameEnUs(6));

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
      var count = 1;
      while (e.MoveNext())
        if (Flux.Random.NumberGenerator.Crypto.NextDouble() > 0.01)
        {
          System.Console.WriteLine($"Field values [{count++}][{e.Current.Length}]");
          System.Console.WriteLine(string.Join('|', e.Current));
          System.Console.WriteLine();
        }
      */

      /*
      var showIPStatistics = true;

      foreach (var nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
      {
        System.Console.WriteLine($"Network Interface");
        System.Console.WriteLine($"Description: {nic.Description}");
        System.Console.WriteLine($"ID: {new System.Guid(nic.Id).ToString()}");
        System.Console.WriteLine($"Name: {nic.Name}");
        System.Console.WriteLine($"Operational status: {nic.OperationalStatus}");
        System.Console.WriteLine($"Receive only: {nic.IsReceiveOnly}");
        System.Console.WriteLine($"Speed: {nic.Speed.ToGroupString()}");
        System.Console.WriteLine($"Supports multicast: {nic.SupportsMulticast}");
        System.Console.WriteLine($"Type: {nic.NetworkInterfaceType}");
        if (nic.GetPhysicalAddress().ToStringMAC() is var mac && !string.IsNullOrWhiteSpace(mac))
          System.Console.WriteLine($"Physical address: {mac}");
        System.Console.WriteLine();

        if (showIPStatistics)
        {
          var ips = nic.GetIPStatistics();
          System.Console.WriteLine($"\tIP Statistics");
          System.Console.WriteLine($"\tIncoming packets: {ips.IncomingPacketsDiscarded.ToGroupString()} discarded, {ips.IncomingPacketsWithErrors.ToGroupString()} errors, {ips.IncomingUnknownProtocolPackets.ToGroupString()} unknown.");
          System.Console.WriteLine($"\tNon-unicast packets: {ips.NonUnicastPacketsReceived.ToGroupString()} received, {ips.NonUnicastPacketsSent.ToGroupString()} sent.");
          System.Console.WriteLine($"\tOutgoing packets: {ips.OutgoingPacketsDiscarded.ToGroupString()} discarded, {ips.OutgoingPacketsWithErrors.ToGroupString()} errors.");
          System.Console.WriteLine($"\tOutput queue length: {ips.OutputQueueLength.ToGroupString()}");
          System.Console.WriteLine($"\tUnicast packets: {ips.UnicastPacketsReceived.ToGroupString()} received, {ips.UnicastPacketsSent.ToGroupString()} sent.");
          System.Console.WriteLine();
        }
      }
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
