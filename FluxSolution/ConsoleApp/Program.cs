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
      //var lat1 = Flux.Quantity.Angle.ConvertDegreeToRadian(80);
      //var lon1 = Flux.Quantity.Angle.ConvertDegreeToRadian(-85);
      var sm_a = 200.0;
      var sm_b = 200.0;

      //for (int lat1 = -90, lon1 = -180; lat1 <= 90 && lon1 <= 180; lat1 += .1, lon1 += .2)
      for (var index = -360; index <= 360; index += 15)
      {
        var lat = new Flux.Quantity.Latitude(index / 4.0);
        var lon = new Flux.Quantity.Longitude(index / 2.0);

        var x1 = lon.MercatorProjectX;
        var y1 = lat.MercatorProjectY;

        System.Console.WriteLine($"{lat}, {lon} = {x1}, {y1}");
      }

      //var pixel = Flux.Geopoint.MercatorProjectPixel(-80, 10, 100, 100);
      //var ll = Flux.Geopoint.MercatorUnprojectPixel(pixel.X, pixel.Y, 100, 100);


      //var a = 0.5;
      //var b = 0.5;

      //var ec1 = Flux.Geometry.Ellipse.EccentricityEx(a, b);
      //var ec2 = Flux.Geometry.Ellipse.EccentricityEx(b, a);
      //var eh1 = Flux.Geometry.Ellipse.H(a, b);
      //var eh2 = Flux.Geometry.Ellipse.H(b, a);
      //var h31 = eh1 * 3;
      //var h32 = eh2 * 3;
      //var hp1 = eh1 * System.Math.PI;
      //var hp2 = eh2 * System.Math.PI;

      //var sp = Flux.Geometry.Ellipse.SurfacePerimeter(a, b);

      //return;

      //foreach (var p in Flux.Reflect.GetPropertyInfos(typeof(Flux.Earth)))
      //  System.Console.WriteLine($"{p.Name} = '{p.GetValueEx(typeof(Flux.Earth))}'");
      //System.Console.WriteLine(Flux.Earth.Volume.ToUnitValue(Flux.Quantity.VolumeUnit.CubicKilometer));
      //System.Console.WriteLine(Flux.Earth.Volume.ToUnitValue(Flux.Quantity.VolumeUnit.CubicMile));

      //System.Console.WriteLine(Flux.Earth.EquatorialCircumference.ToUnitValue(Flux.Quantity.LengthUnit.Mile));
      //System.Console.WriteLine(Flux.Earth.PolarCircumference.ToUnitValue(Flux.Quantity.LengthUnit.Mile));
      //var g = new Flux.Geopoint(32.253460, -110.911789, 728);
      //System.Console.WriteLine($"Tucson: {g.ToString()}");

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
