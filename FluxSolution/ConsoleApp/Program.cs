using Flux;
using System;
using System.Linq;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {
      foreach (var data in Flux.Resources.Ucd.Blocks.GetData(Flux.Resources.Ucd.Blocks.LocalUri))
      {
        System.Console.WriteLine(string.Join('|', data));
        //System.Console.WriteLine($"{data.GetValue(0):X4} - {data.GetValue(1):X4} = {data.GetValue(2)}");
      }

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


      foreach (var index in Flux.Linq.AlternatingRange(60, 24, 1, Linq.AlternatingRangeDirection.AwayFromMean))
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
