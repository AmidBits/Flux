using System.Linq;

namespace Flux
{
  public static class Electricity
  {
    /// <summary>Converts resistor values as if in parallel configuration.</summary>
    public static double ParallelResistors(double[] resistors)
      => 1d / resistors.Sum(r => 1d / r);
    /// <summary>Converts resistor values as if in parallel configuration.</summary>
    public static double SerialResistors(double[] resistors)
      => resistors.Sum();

    /// <summary>Calculate current (flow) V/R = I.</summary>
    public static double ToCurrentI(double voltageV, double resistanceR)
      => voltageV / resistanceR;
    /// <summary>Calculate resistance V/I = R.</summary>
    public static double ToResistanceR(double voltageV, double currentI)
      => voltageV / currentI;
    /// <summary>Calculate voltage (pressure) I/R = V.</summary>
    public static double ToVoltageV(double currentI, double resistanceR)
      => currentI * resistanceR;
  }
}
