using System.Runtime.Intrinsics;

namespace Flux.Dsp
{
  public sealed class OnePole
  {
    private double m_a; // pole coefficient
    private double m_b0; // feedforward coefficient for x[n]
    private double m_b1; // feedforward coefficient for x[n-1]
    private double m_y1; // previous output
    private double m_x1; // previous input

    public double Process(double x)
    {
      var y = m_a * m_y1 + m_b0 * x + m_b1 * m_x1;

      m_y1 = y;
      m_x1 = x;

      return y;
    }

    public void Reset()
    {
      m_y1 = 0.0;
      m_x1 = 0.0;
    }

    public void SetCoefficient(double a, double b0, double b1)
    {
      m_a = a;
      m_b0 = b0;
      m_b1 = b1;
    }

    #region Static methods

    public static void Process4Filters(double x, OnePole[] op, out System.Runtime.Intrinsics.Vector256<double> y)
    {
      var xv = Vector256.Create(x);

      var av = Vector256.Create(op[0].m_a, op[1].m_a, op[2].m_a, op[3].m_a);
      var b0v = Vector256.Create(op[0].m_b0, op[1].m_b0, op[2].m_b0, op[3].m_b0);
      var b1v = Vector256.Create(op[0].m_b1, op[1].m_b1, op[2].m_b1, op[3].m_b1);

      var y1v = Vector256.Create(op[0].m_y1, op[1].m_y1, op[2].m_y1, op[3].m_y1);
      var x1v = Vector256.Create(op[0].m_x1, op[1].m_x1, op[2].m_x1, op[3].m_x1);

      var yv = (av * y1v) + (b0v * xv) + (b1v * x1v); // y = a*y1 + b0*x + b1*x1

      y = yv;

      for (var i = 0; i < 4; i++) // Write back state
      {
        op[i].m_y1 = yv[i];
        op[i].m_x1 = x;
      }
    }

    public static void Process4Samples(System.Runtime.Intrinsics.Vector256<double> x, OnePole op, out System.Runtime.Intrinsics.Vector256<double> y)
    {
      var av = Vector256.Create(op.m_a);
      var b0v = Vector256.Create(op.m_b0);
      var b1v = Vector256.Create(op.m_b1);

      var y1v = Vector256.Create(op.m_y1);
      var x1v = Vector256.Create(op.m_x1);

      var yv = (av * y1v) + (b0v * x) + (b1v * x1v); // y = a*y1 + b0*x + b1*x1

      y = yv;

      op.m_y1 = yv[3]; // Update scalar state from last lane.
      op.m_x1 = x[3];
    }

    public static void Process8Samples(System.Runtime.Intrinsics.Vector512<double> x, OnePole op, out System.Runtime.Intrinsics.Vector512<double> y)
    {
      var av = Vector512.Create(op.m_a);
      var b0v = Vector512.Create(op.m_b0);
      var b1v = Vector512.Create(op.m_b1);

      var y1v = Vector512.Create(op.m_y1);
      var x1v = Vector512.Create(op.m_x1);

      var yv = (av * y1v) + (b0v * x) + (b1v * x1v); // y = a*y1 + b0*x + b1*x1

      y = yv;

      op.m_y1 = yv[7]; // Update scalar state from last lane.
      op.m_x1 = x[7];
    }

    public static void ProcessNPole4Samples(System.Runtime.Intrinsics.Vector256<double> x, OnePole[] stages, out System.Runtime.Intrinsics.Vector256<double> y)
    {
      var yv = x;

      for (var i = 0; i < stages.Length; i++)
      {
        ref var op = ref stages[i];

        var av = Vector256.Create(op.m_a);
        var b0v = Vector256.Create(op.m_b0);
        var b1v = Vector256.Create(op.m_b1);

        var y1v = Vector256.Create(op.m_y1);
        var x1v = Vector256.Create(op.m_x1);

        var yStage = (av * y1v) + (b0v * yv) + (b1v * x1v); // y = a*y1 + b0*input + b1*x1

        op.m_y1 = yStage[3]; // Update scalar state for this stage from last lane.
        op.m_x1 = yv[3]; // Last input sample into this stage.

        yv = yStage; // Becomes input to next stage.
      }

      y = yv;
    }

    public static void ProcessNPole8Samples(System.Runtime.Intrinsics.Vector512<double> x, OnePole[] stages, out System.Runtime.Intrinsics.Vector512<double> y)
    {
      var yv = x;

      for (var i = 0; i < stages.Length; i++)
      {
        ref var op = ref stages[i];

        var av = Vector512.Create(op.m_a);
        var b0v = Vector512.Create(op.m_b0);
        var b1v = Vector512.Create(op.m_b1);

        var y1v = Vector512.Create(op.m_y1);
        var x1v = Vector512.Create(op.m_x1);

        var yStage = (av * y1v) + (b0v * yv) + (b1v * x1v); // y = a*y1 + b0*input + b1*x1

        op.m_y1 = yStage[7]; // Update scalar state for this stage from last lane.
        op.m_x1 = yv[7]; // Last input sample into this stage.

        yv = yStage; // Becomes input to next stage.
      }

      y = yv;
    }

    #endregion

    public static class Designer
    {
      private static void Setup(double sampleRate, double cutoffHz, double S, double dBgain, out double a, out double G)
      {
        var effectiveFc = cutoffHz * S;

        if (effectiveFc <= 0.0)
          effectiveFc = 1e-6;
        if (effectiveFc >= 0.5 * sampleRate)
          effectiveFc = 0.5 * sampleRate - 1e-6;

        a = double.Exp(-double.Tau * effectiveFc / sampleRate);
        G = double.Pow(10.0, dBgain / 20.0); // convert dB → linear gain
      }

      public static void AllPass(OnePole op, double sampleRate, double cutoffHz, double S = 1)
      {
        Setup(sampleRate, cutoffHz, S, 0, out var a, out var _);

        var b0 = -a;
        var b1 = 1.0;

        op.SetCoefficient(a, b0, b1);
      }

      public static void HighPass(OnePole op, double sampleRate, double cutoffHz, double S)
      {
        Setup(sampleRate, cutoffHz, S, 0, out var a, out var _);

        var b0 = a;
        var b1 = -a;

        op.SetCoefficient(a, b0, b1);
      }

      public static void HighShelf(OnePole op, double sampleRate, double cutoffHz, double S, double dBgain)
      {
        Setup(sampleRate, cutoffHz, S, dBgain, out var a, out var G);

        var b0 = a * (G - 1.0);
        var b1 = a * (1.0 - G);

        op.SetCoefficient(a, b0, b1);
      }

      public static void LowPass(OnePole op, double sampleRate, double cutoffHz, double S = 1)
      {
        Setup(sampleRate, cutoffHz, S, 0, out var a, out var _);

        var b0 = 1.0 - a;
        var b1 = 0.0;

        op.SetCoefficient(a, b0, b1);
      }

      public static void LowShelf(OnePole op, double sampleRate, double cutoffHz, double S, double dBgain)
      {
        Setup(sampleRate, cutoffHz, S, dBgain, out var a, out var G);

        var b0 = (1.0 - a) * G;
        var b1 = 0.0;

        op.SetCoefficient(a, b0, b1);
      }
    }
  }
}
