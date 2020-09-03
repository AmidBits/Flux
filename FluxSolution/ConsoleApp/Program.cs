using Flux;
using Flux.Model;
using Flux.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ConsoleApp
{
  class Program
  {
    static double[] felix = new double[] { 0.003027523, 0.002012256, -0.001369238, -0.001737660, -0.001647287,
        0.000275154, 0.002017238, 0.001372621, 0.000274148, -0.000913576, 0.001920263, 0.001186456, -0.000364631,
        0.000638337, 0.000182266, -0.001275626, -0.000821093, 0.001186998, -0.000455996, -0.000547445, -0.000182582,
        -0.000547845, 0.001279006, 0.000456204, 0.000000000, -0.001550388, 0.001552795, 0.000729594, -0.000455664,
        -0.002188184, 0.000639620, 0.000091316, 0.001552228, -0.001002826, 0.000182515, -0.000091241, -0.000821243,
        -0.002009132, 0.000000000, 0.000823572, 0.001920088, -0.001368863, 0.000000000, 0.002101800, 0.001094291,
        0.001639643, 0.002637323, 0.000000000, -0.000172336, -0.000462665, -0.000136141 };

    static double[] kahn1 = new double[] { 2, 2, 4, 4 };
    static double[] kahn2 = new double[] { 1, 1, 6, 4 };

    static double[] thoughtco = new double[] { 1, 2, 2, 3, 5, 7, 7, 7, 7, 9 };

    static double[] aad = new double[] { 2, 2, 3, 4, 14 };

    //public class Test
    //  : Flux.Collections.Generic.IQuadtree
    //{
    //  public Rectangle Bounds { get; set; }
    //}

    private static void TimedMain(string[] args)
    {
      var ja = thoughtco.ToJaggedArray((e, i) => new object[] { e, e * e });
      var tda = ja.ToTwoDimensionalArray();

      System.Console.WriteLine(tda.ToConsoleString());
      //var quad = new Flux.Collections.Generic.Quadtree<Test>(new Rectangle(0, 0, 600, 600));

      //quad.Insert(new Test() { Bounds = new Rectangle(450, 150, 10, 10) });
      //quad.Insert(new Test() { Bounds = new Rectangle(150, 350, 10, 10) });
      //quad.Insert(new Test() { Bounds = new Rectangle(400, 250, 10, 10) });
      //quad.Insert(new Test() { Bounds = new Rectangle(425, 125, 10, 10) });

      //var q = quad.Retrieve(new Rectangle(301,301, 298, 298));

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => aad.AverageAbsoluteDeviationFromMean()));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => aad.AverageAbsoluteDeviationFromMedian()));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => aad.AverageAbsoluteDeviationFromMode()));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => felix.Variance()));
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
