namespace Flux
{
  public static partial class Doubles
  {
    /// <summary></summary>
    /// <param name="x">Any positive value.</param>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/csharp_gamma/"/>
    public static double Gamma(this double x)
    {
      if (x <= 0) throw new System.ArgumentOutOfRangeException(nameof(x), @"Must be positive.");

      // Split the function domain into three intervals:
      // (0, 0.001), [0.001, 12), and (12, infinity)

      ///////////////////////////////////////////////////////////////////////////
      // First interval: (0, 0.001)
      //
      // For small x, 1/Gamma(x) has power series x + gamma x^2  - ...
      // So in the range, 1/Gamma(x) = x + gamma x^2 with error on the order of x^3.
      // The relative error over the interval is less than 6e-7.

      var gamma = 0.577215664901532860606512090; // Euler's gamma constant

      if (x < 0.001)
      {
        return 1 / (x * (1 + gamma * x));
      }

      ///////////////////////////////////////////////////////////////////////////
      // Second interval: [0.001, 12)

      if (x < 12.0)
      {
        // The algorithm directly approximates gamma over (1,2) and uses
        // reduction identities to reduce other arguments to the interval.

        var y = x;
        var n = 0;
        var arg_was_less_than_one = (y < 1);

        // Add or subtract integers as necessary to bring y into (1,2)
        // Will correct for, below
        if (arg_was_less_than_one)
        {
          y += 1;
        }
        else
        {
          n = System.Convert.ToInt32(double.Floor(y)) - 1;  // will use n later
          y -= n;
        }

        // numerator coefficients for approximation over the interval (1,2)
        double[] p = { -1.71618513886549492533811E+0, 2.47656508055759199108314E+1, -3.79804256470945635097577E+2, 6.29331155312818442661052E+2, 8.66966202790413211295064E+2, -3.14512729688483675254357E+4, -3.61444134186911729807069E+4, 6.64561438202405440627855E+4 };

        // denominator coefficients for approximation over the interval (1,2)
        double[] q = { -3.08402300119738975254353E+1, 3.15350626979604161529144E+2, -1.01515636749021914166146E+3, -3.10777167157231109440444E+3, 2.25381184209801510330112E+4, 4.75584627752788110767815E+3, -1.34659959864969306392456E+5, -1.15132259675553483497211E+5 };

        var num = 0d;
        var den = 1d;

        var z = y - 1;

        for (var i = 0; i < 8; i++)
        {
          num = (num + p[i]) * z;
          den = den * z + q[i];
        }

        var result = num / den + 1;

        // Apply correction if argument was not initially in (1,2)
        if (arg_was_less_than_one)
        {
          // Use identity gamma(z) = gamma(z+1)/z
          // The variable "result" now holds gamma of the original y + 1
          // Thus we use y-1 to get back the orginal y.
          result /= (y - 1);
        }
        else
        {
          // Use the identity gamma(z+n) = z*(z+1)* ... *(z+n-1)*gamma(z)
          for (var i = 0; i < n; i++)
          {
            result *= y++;
          }
        }

        return result;
      }

      ///////////////////////////////////////////////////////////////////////////
      // Third interval: [12, infinity)

      if (x > 171.624)
      {
        return double.PositiveInfinity; // Correct answer too large to display. 
      }

      return double.Exp(LogGamma(x));
    }
  }
}
