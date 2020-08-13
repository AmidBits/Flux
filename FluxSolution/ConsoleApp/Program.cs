using Flux;
using Flux.Text;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {
      System.Collections.Generic.Dictionary<string, string> m_soundexTests = new System.Collections.Generic.Dictionary<string, string>()
      {
        { "Ashcroft", "A261" },
        { "Fusedale", "F234" },
        { "Grayley", "G640" },
        { "Hatcher", "H326" },
        { "Honeyman", "H555" },
        { "Parade", "P630" },
        { "Pfister", "P236" },
        { "Rubin", "R150" },
        { "Rupert", "R163" },
        { "Tymczak", "T522" },
        { "Catherine", "CTHRN" }
      };

      foreach (var kvp in m_soundexTests)
      {
        System.Console.WriteLine($"{kvp.Key} = \"{(new Flux.Text.PhoneticAlgorithm.MatchRatingApproach().Encode(kvp.Key) is var mra ? mra : throw new System.Exception())}\" ({kvp.Value}) == {mra.Equals(kvp.Value)}");
        //System.Console.WriteLine($"{kvp.Key} = \"{(new Flux.Text.PhoneticAlgorithm.Soundex().Encode(kvp.Key) is var code ? code : throw new System.Exception())}\" ({kvp.Value}) == {code.Equals(kvp.Value)}");
        //System.Console.WriteLine($"{kvp.Key} = \"{(new Flux.Text.PhoneticAlgorithm.SqlSoundex().Encode(kvp.Key) is var sql ? sql : throw new System.Exception())}\" ({kvp.Value}) == {sql.Equals(kvp.Value)}");
      }

      System.Collections.Generic.Dictionary<string, string> m_refinedSoundexTests = new System.Collections.Generic.Dictionary<string, string>()
      {
        { "Braz", "B1905" },
        { "Charon", "C30908" },
        { "Hayers", "H093" },
        { "Lambert", "L7081096" },
        { "Nolton", "N807608" },
      };

      foreach (var kvp in m_refinedSoundexTests)
      {
        System.Console.WriteLine($"{kvp.Key} = \"{(new Flux.Text.PhoneticAlgorithm.RefinedSoundex().Encode(kvp.Key) is var code ? code : throw new System.Exception())}\" ({kvp.Value}) == {code.Equals(kvp.Value)}");
      }

      

      return;

      var x = " Blañk";
      var y = "blank";

      System.Console.WriteLine(x);
      System.Console.WriteLine(y);
      var a = new ReadOnlySpan<string>(x.Select(c => c.ToString()).ToArray());
      var b = new ReadOnlySpan<string>(y.Select(c => c.ToString()).ToArray());

      //a = "AGGTAB".AsSpan();
      //b = "GXTXAYB".AsSpan();
      System.Console.WriteLine(a.ToString());
      System.Console.WriteLine(b.ToString());

      System.Console.WriteLine($"DamerauLevenshteinDistance: {a.DamerauLevenshteinDistance(b, Flux.StringComparer.CurrentCulture)}, {a.DamerauLevenshteinDistance(b, Flux.StringComparer.CurrentCultureIgnoreCase)}, {a.DamerauLevenshteinDistance(b, Flux.StringComparer.CurrentCultureIgnoreNonSpace)}, {a.DamerauLevenshteinDistance(b, Flux.StringComparer.CurrentCultureIgnoreNonSpaceAndCase)}");
      System.Console.WriteLine($"   JackardIndexCoefficient: {a.JackardIndexCoefficient(b, Flux.StringComparer.CurrentCulture)}, {a.JackardIndexCoefficient(b, Flux.StringComparer.CurrentCultureIgnoreCase)}, {a.JackardIndexCoefficient(b, Flux.StringComparer.CurrentCultureIgnoreNonSpace)}, {a.JackardIndexCoefficient(b, Flux.StringComparer.CurrentCultureIgnoreNonSpaceAndCase)}");
      System.Console.WriteLine($"     JaroWinklerSimilarity: {a.JaroWinklerSimilarity(b, Flux.StringComparer.CurrentCulture)}, {a.JaroWinklerSimilarity(b, Flux.StringComparer.CurrentCultureIgnoreCase)}, {a.JaroWinklerSimilarity(b, Flux.StringComparer.CurrentCultureIgnoreNonSpace)}, {a.JaroWinklerSimilarity(b, Flux.StringComparer.CurrentCultureIgnoreNonSpaceAndCase)}");
      System.Console.WriteLine($"       LevenshteinDistance: {a.LevenshteinDistance(b, Flux.StringComparer.CurrentCulture)}, {a.LevenshteinDistance(b, Flux.StringComparer.CurrentCultureIgnoreCase)}, {a.LevenshteinDistance(b, Flux.StringComparer.CurrentCultureIgnoreNonSpace)}, {a.LevenshteinDistance(b, Flux.StringComparer.CurrentCultureIgnoreNonSpaceAndCase)}");
      System.Console.WriteLine($"  LongestCommonSubsequence: {a.LongestCommonSubsequence(b, Flux.StringComparer.CurrentCulture)}, {a.LongestCommonSubsequence(b, Flux.StringComparer.CurrentCultureIgnoreCase)}, {a.LongestCommonSubsequence(b, Flux.StringComparer.CurrentCultureIgnoreNonSpace)}, {a.LongestCommonSubsequence(b, Flux.StringComparer.CurrentCultureIgnoreNonSpaceAndCase)}");
      System.Console.WriteLine($"    LongestCommonSubstring: {a.LongestCommonSubstring(b, Flux.StringComparer.CurrentCulture)}, {a.LongestCommonSubstring(b, Flux.StringComparer.CurrentCultureIgnoreCase)}, {a.LongestCommonSubstring(b, Flux.StringComparer.CurrentCultureIgnoreNonSpace)}, {a.LongestCommonSubstring(b, Flux.StringComparer.CurrentCultureIgnoreNonSpaceAndCase)}");
      System.Console.WriteLine($"    OptimalStringAlignment: {a.OptimalStringAlignment(b, Flux.StringComparer.CurrentCulture)}, {a.OptimalStringAlignment(b, Flux.StringComparer.CurrentCultureIgnoreCase)}, {a.OptimalStringAlignment(b, Flux.StringComparer.CurrentCultureIgnoreNonSpace)}, {a.OptimalStringAlignment(b, Flux.StringComparer.CurrentCultureIgnoreNonSpaceAndCase)}");
      System.Console.WriteLine($"        OverlapCoefficient: {a.OverlapCoefficient(b, Flux.StringComparer.CurrentCulture)}, {a.OverlapCoefficient(b, Flux.StringComparer.CurrentCultureIgnoreCase)}, {a.OverlapCoefficient(b, Flux.StringComparer.CurrentCultureIgnoreNonSpace)}, {a.OverlapCoefficient(b, Flux.StringComparer.CurrentCultureIgnoreNonSpaceAndCase)}");
      System.Console.WriteLine($"         SørensenDiceIndex: {a.SørensenDiceIndex(b, Flux.StringComparer.CurrentCulture)}, {a.SørensenDiceIndex(b, Flux.StringComparer.CurrentCultureIgnoreCase)}, {a.SørensenDiceIndex(b, Flux.StringComparer.CurrentCultureIgnoreNonSpace)}, {a.SørensenDiceIndex(b, Flux.StringComparer.CurrentCultureIgnoreNonSpaceAndCase)}");
      //System.Console.WriteLine($"                   Soundex: {x.AsSpan().ToUpperCase().SoundexEncode().ToString()}, {y.AsSpan().ToUpperCase().SoundexEncode().ToString()}");
      //System.Console.WriteLine($"         SoundexDifference: {x.AsSpan().ToUpperCase().SoundexEncode().SoundexDifference(y.AsSpan().ToUpperCase().SoundexEncode())}");
      //System.Console.WriteLine($"            RefinedSoundex: {x.AsSpan().ToUpperCase().RefinedSoundexEncode().ToString()}, {y.AsSpan().ToUpperCase().RefinedSoundexEncode().ToString()}");

      return;

      //for (var angle = -Flux.Math.PiX2; angle <= Flux.Math.PiX2; angle += Flux.Math.PiX2 / 12)
      //{
      //  var (x1, y1) = Flux.Math.RotationAngleToCartesian(angle);
      //  var (x2, y2) = Flux.Math.RotationAngleToCartesianEx(angle);

      //  var rad1 = Flux.Math.CartesianToRotationAngle(x1, y1);
      //  var rad2 = Flux.Math.CartesianToRotationAngleEx(x2, y2);

      //  System.Console.WriteLine($"{angle:N6} ({Flux.Math.RadianToDegree(angle):N3}) = ({x1:N6}, {y1:N6}) = {rad1:N6} ({Flux.Math.RadianToDegree(rad1):N3}) = Ex({x2:N6}, {y2:N6}) = {rad2:N6} ({Flux.Math.RadianToDegree(rad2):N3})");
      //}
      //return;

      var op = "Microsoft GS Wavetable Synth";

      using var mop = Flux.Media.Midi.MidiOut.Create(op);

      System.Console.WriteLine($"{mop.Index} = {mop.Capabilities.Manufacturer}, {mop.Capabilities.Name}, {mop.Capabilities.DriverVersion}");

      var c = new byte[] { 0x90, 0x3C, 0x7F };
      var e = new byte[] { 0x90, 0x40, 0x7F };
      var g = new byte[] { 0x90, 0x43, 0x7F };

      var chord = new byte[] { 0x90, 0x3C, 0x7F, 0x90, 0x40, 0x7F, 0x90, 0x43, 0x7F };


      foreach (var index in Flux.LinqX.AlternatingRange(60, 24, 1, LinqX.AlternatingRangeDirection.AwayFromMean))
      {
        mop.NoteOn(0, index, 127);
        System.Threading.Thread.Sleep(40);
        mop.NoteOff(0, index, 127);
        System.Threading.Thread.Sleep(40);
      }
      //mop.TrySend(0x90, 0x3C, 0x7F );
      //mop.TrySend(0x007F3C90);
      System.Console.ReadKey();
      //System.Console.WriteLine($"Out: {Flux.Media.WinMm.midiOutGetNumDevs()}");
      //if(Flux.Media.WinMm.midiOutGetNumDevs() > 0)
      //{
      //  foreach(var c in Flux.Media.MidiOutPort.GetDevicePorts())
      //  {
      //    System.Console.WriteLine($"{c.Manufacturer}, {c.Name}, {c.DriverVersion}");
      //  }
      //}


    }

    static void Main(string[] args)
    {
      System.Console.InputEncoding = System.Text.Encoding.UTF8;
      System.Console.OutputEncoding = System.Text.Encoding.UTF8;

      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => TimedMain(args), 1));

      System.Console.WriteLine(System.Environment.NewLine + @"Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
