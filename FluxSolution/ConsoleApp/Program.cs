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
      var date = new System.DateTime(1, 3, 2);
      //date = System.DateTime.Now;
      System.Console.WriteLine($"{System.DateTime.MinValue} - {System.DateTime.MaxValue}");
      System.Console.WriteLine($"{date} = {date.ToJulianDate()} = {Flux.JulianDayNumber.FromGregorianDate(date.Year, date.Month, date.Day)}");
      System.Console.WriteLine($"{2459227.32708} = {Flux.JulianDayNumber.ToGregorianDate(2459227)}");
      //var dr = Flux.Resources.Presets.GetScowlTwoOfTwelveFull(Flux.Resources.Presets.UriScowlTwoOfTwelveFullLocal);

      //var a = dr.ToArray();

      //var cs = a.ToConsoleStringJagged();

      //System.Console.WriteLine(cs);

      //foreach (var r in dr)
      //{
      //	System.Console.WriteLine(string.Join(',', r.GetNames()));
      //	System.Console.WriteLine(string.Join(',', r.GetValues()));
      //}

      //System.Console.WriteLine(dt.ToConsoleString());

      //var showIPStatistics = true;

      //foreach (var nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
      //{
      //	System.Console.WriteLine($"Network Interface");
      //	System.Console.WriteLine($"Description: {nic.Description}");
      //	System.Console.WriteLine($"ID: {new System.Guid(nic.Id).ToString()}");
      //	System.Console.WriteLine($"Name: {nic.Name}");
      //	System.Console.WriteLine($"Operational status: {nic.OperationalStatus}");
      //	System.Console.WriteLine($"Receive only: {nic.IsReceiveOnly}");
      //	System.Console.WriteLine($"Speed: {nic.Speed.ToGroupString()}");
      //	System.Console.WriteLine($"Supports multicast: {nic.SupportsMulticast}");
      //	System.Console.WriteLine($"Type: {nic.NetworkInterfaceType}");
      //	if (nic.GetPhysicalAddress().ToStringMAC() is var mac && !string.IsNullOrWhiteSpace(mac))
      //		System.Console.WriteLine($"Physical address: {mac}");
      //	System.Console.WriteLine();

      //	if (showIPStatistics)
      //	{
      //		var ips = nic.GetIPStatistics();
      //		System.Console.WriteLine($"\tIP Statistics");
      //		System.Console.WriteLine($"\tIncoming packets: {ips.IncomingPacketsDiscarded.ToGroupString()} discarded, {ips.IncomingPacketsWithErrors.ToGroupString()} errors, {ips.IncomingUnknownProtocolPackets.ToGroupString()} unknown.");
      //		System.Console.WriteLine($"\tNon-unicast packets: {ips.NonUnicastPacketsReceived.ToGroupString()} received, {ips.NonUnicastPacketsSent.ToGroupString()} sent.");
      //		System.Console.WriteLine($"\tOutgoing packets: {ips.OutgoingPacketsDiscarded.ToGroupString()} discarded, {ips.OutgoingPacketsWithErrors.ToGroupString()} errors.");
      //		System.Console.WriteLine($"\tOutput queue length: {ips.OutputQueueLength.ToGroupString()}");
      //		System.Console.WriteLine($"\tUnicast packets: {ips.UnicastPacketsReceived.ToGroupString()} received, {ips.UnicastPacketsSent.ToGroupString()} sent.");
      //		System.Console.WriteLine();
      //	}

      //}

      //using (var sr = new System.IO.StreamReader(@"C:\Test\Xml.xml"))
      //	foreach (var rune in sr.EnumerateTextElements())
      //		System.Console.Write(rune.ToString());

      //using (var sr = new System.IO.StreamReader(@"C:\Test\Xml.xml"))
      //	foreach (var rune in sr.EnumerateRunes())
      //		System.Console.Write(rune.ToString());

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
