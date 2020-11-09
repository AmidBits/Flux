using Flux;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace ConsoleApp
{
  class Program
  {
    static double Integral(double range, int steps, System.Func<double, double> function)
    {
      var stepSize = range / steps;

      return System.Linq.Enumerable.Range(1, steps).AsParallel().Sum(i => function((double)i / (double)steps) * stepSize);

      var sum = 0d;

      for (var iteration = 1; iteration <= steps; iteration++)
        sum += function(iteration / steps) * stepSize;

      return sum;
    }

    private static void TimedMain(string[] _)
    {
      for (var steps = 1; steps < 100; steps++)
      {
        System.Console.WriteLine(Integral(1, steps, v => System.Math.Sqrt(v)));
        System.Console.WriteLine(System.Linq.Enumerable.Range(1,steps).AsParallel().Sum());
      }
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
