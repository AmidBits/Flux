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
			//var mra = new Flux.Text.PhoneticAlgorithm.MatchRatingApproach();

			//var nm1 = "Catherine";
			//var mra1 = mra.EncodePhoneticAlgorithm(nm1);
			//System.Console.WriteLine($"{nm1} = {mra1}");

			//var nm2 = "Kathryn";
			//var mra2 = mra.EncodePhoneticAlgorithm(nm2);
			//System.Console.WriteLine($"{nm2} = {mra2}");

			//var isGood = mra.CompareEncodings(nm1, nm2, out var minimumRating, out var similarityRating);
			//System.Console.WriteLine($"{isGood}, {minimumRating}, {similarityRating}");
			
			//return;

			var data1 = Flux.Resources.Census.CountiesAllData.GetObjects(Flux.Resources.Census.CountiesAllData.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.Census.CountiesAllData)} = {data1.GetLength(0).ToGroupString()} rows, {data1[0].GetLength(0)} columns = {System.DateTime.Now}");
			var data2 = Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows.GetStrings(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows)} = {data2.GetLength(0).ToGroupString()} rows, {data2[0].GetLength(0)} columns {System.DateTime.Now}");
			var data3 = Flux.Resources.ProjectGutenberg.TableOfContents.GetStrings(Flux.Resources.ProjectGutenberg.TableOfContents.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.ProjectGutenberg.TableOfContents)} = {data3.GetLength(0).ToGroupString()} rows, {data3[0].GetLength(0)} columns {System.DateTime.Now}");
			var data4 = Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.GetStrings(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings)} = {data4.GetLength(0).ToGroupString()} rows, {data4[0].GetLength(0)} columns {System.DateTime.Now}");
			var data5 = Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.GetStrings(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary)} = {data5.GetLength(0).ToGroupString()} rows, {data5[0].GetLength(0)} columns {System.DateTime.Now}");
			var data6 = Flux.Resources.Scowl.TwoOfTwelveFull.GetStrings(Flux.Resources.Scowl.TwoOfTwelveFull.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.Scowl.TwoOfTwelveFull)} = {data6.GetLength(0).ToGroupString()} rows, {data6[0].GetLength(0)} columns {System.DateTime.Now}");
			var data7 = Flux.Resources.Scrape.ZipCodes.GetObjects(Flux.Resources.Scrape.ZipCodes.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.Scrape.ZipCodes)} = {data7.GetLength(0).ToGroupString()} rows, {data7[0].GetLength(0)} columns {System.DateTime.Now}");
			var data8 = Flux.Resources.Ucd.Blocks.GetObjects(Flux.Resources.Ucd.Blocks.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.Ucd.Blocks)} = {data8.GetLength(0).ToGroupString()} rows, {data8[0].GetLength(0)} columns {System.DateTime.Now}");
			var data9 = Flux.Resources.Ucd.UnicodeData.GetObjects(Flux.Resources.Ucd.UnicodeData.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.Ucd.UnicodeData)} = {data9.GetLength(0).ToGroupString()} rows, {data9[0].GetLength(0)} columns {System.DateTime.Now}");
			var data0 = Flux.Resources.W3c.NamedCharacterReferences.GetStrings(Flux.Resources.W3c.NamedCharacterReferences.UriLocal).ToArray();
			System.Console.WriteLine($"{nameof(Flux.Resources.W3c.NamedCharacterReferences)} = {data0.GetLength(0).ToGroupString()} rows, {data0[0].GetLength(0)} columns {System.DateTime.Now}");

			//using var e = data0.GetEnumerator();

			//if (e.MoveNext())
			//{
			//	System.Console.WriteLine($"Field names [{e.Current.Length}]");
			//	System.Console.WriteLine(string.Join('|', e.Current));

			//	var count = 1;

			//	while (e.MoveNext())
			//	{
			//		if (Flux.Random.NumberGenerator.Crypto.NextDouble() > 0.01)
			//		{
			//			System.Console.WriteLine($"Field values [{count++}][{e.Current.Length}]");
			//			System.Console.WriteLine(string.Join('|', e.Current));
			//			System.Console.WriteLine();
			//		}
			//	}
			//}

			//var jd = new Flux.JulianDay(2459227.9722685).AddHours(2).AddMinutes(4).AddSeconds(6);
			//var jd = new Flux.JulianDay(0);//.AddHours(2).AddMinutes(4).AddSeconds(6);
			//November 24, 4714 BC 12:00:00 = Monday, January 1, 4713
			//var jd = new JulianDay(Flux.JulianDay.GregorianDateToJulianDay(1957, 10, 4.81));
			//var jd = new JulianDate(Flux.JulianDate.JulianDateToJulianDay(-4712, 1, 1.5));
			//var jd = new JulianDay(Flux.JulianDay.JulianDateToJulianDay(1, 1, 1.5));

			//var jd = Flux.JulianDate.FromGregorianCalendarDate(33, 5, 30);
			//var jd = Flux.JulianDate.FromJulianCalendarDate(-4712, 1, 1);

			//var addDays = (int)(365.25 * 4 * 1178);
			//var addHours = 6;
			//var addMinutes = 2;
			//var addSeconds = 2;

			//System.Console.WriteLine($"{jd}{System.Environment.NewLine}{jd.ToGregorianCalendarDateString()}{System.Environment.NewLine}{jd.ToJulianCalendarDateString()}{System.Environment.NewLine}(time) {jd.ToTimeString()}{System.Environment.NewLine}");
			//jd = jd.AddDays(addDays);
			//System.Console.WriteLine($"{jd}{System.Environment.NewLine}{jd.ToGregorianCalendarDateString()}{System.Environment.NewLine}{jd.ToJulianCalendarDateString()}{System.Environment.NewLine}(added {addDays} days) {jd.ToTimeString()}{System.Environment.NewLine}");
			//jd = jd.AddHours(addHours);
			//System.Console.WriteLine($"{jd}{System.Environment.NewLine}{jd.ToGregorianCalendarDateString()}{System.Environment.NewLine}{jd.ToJulianCalendarDateString()}{System.Environment.NewLine}(added {addHours} hours) {jd.ToTimeString()}{System.Environment.NewLine}");
			//jd = jd.AddMinutes(addMinutes);
			//System.Console.WriteLine($"{jd}{System.Environment.NewLine}{jd.ToGregorianCalendarDateString()}{System.Environment.NewLine}{jd.ToJulianCalendarDateString()}{System.Environment.NewLine}(added {addMinutes} minutes) {jd.ToTimeString()}{System.Environment.NewLine}");
			//jd = jd.AddSeconds(addSeconds);
			//System.Console.WriteLine($"{jd}{System.Environment.NewLine}{jd.ToGregorianCalendarDateString()}{System.Environment.NewLine}{jd.ToJulianCalendarDateString()}{System.Environment.NewLine}(added {addSeconds} seconds) {jd.ToTimeString()}{System.Environment.NewLine}");

			//var date = new System.DateTime(1, 3, 2);

			//date = System.DateTime.Now;
			//System.Console.WriteLine($"{System.DateTime.MinValue} - {System.DateTime.MaxValue}");
			//System.Console.WriteLine($"{date} = {date.ToJulianDate()} = {Flux.JulianDayNumber.FromGregorianDate(date.Year, date.Month, date.Day)}");
			//System.Console.WriteLine($"{2459227.32708} = {Flux.JulianDayNumber.ToGregorianDate(2459227)}");
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
