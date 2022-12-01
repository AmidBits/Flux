namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary></summary>
    /// <param name="x">Any positive value.</param>
    public static TSelf Gamma<TSelf>(TSelf x)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      if (x <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(x), @"Must be positive.");

      // Split the function domain into three intervals:
      // (0, 0.001), [0.001, 12), and (12, infinity)

      ///////////////////////////////////////////////////////////////////////////
      // First interval: (0, 0.001)
      //
      // For small x, 1/Gamma(x) has power series x + gamma x^2  - ...
      // So in the range, 1/Gamma(x) = x + gamma x^2 with error on the order of x^3.
      // The relative error over the interval is less than 6e-7.

      TSelf gamma = TSelf.CreateChecked(0.577215664901532860606512090); // Euler's gamma constant

      if (x < TSelf.CreateChecked(0.001))
      {
        return TSelf.One / (x * (TSelf.One + gamma * x));
      }

      ///////////////////////////////////////////////////////////////////////////
      // Second interval: [0.001, 12)

      if (x < TSelf.CreateChecked(12.0))
      {
        // The algorithm directly approximates gamma over (1,2) and uses
        // reduction identities to reduce other arguments to the interval.

        var y = x;
        var n = 0;
        var arg_was_less_than_one = (y < TSelf.One);

        // Add or subtract integers as necessary to bring y into (1,2)
        // Will correct for, below
        if (arg_was_less_than_one)
        {
          y += TSelf.One;
        }
        else
        {
          n = int.CreateChecked(TSelf.Floor(y)) - 1;  // will use n later
          y -= TSelf.CreateChecked(n);
        }

        // numerator coefficients for approximation over the interval (1,2)
        TSelf[] p = { TSelf.CreateChecked(-1.71618513886549492533811E+0), TSelf.CreateChecked(2.47656508055759199108314E+1), TSelf.CreateChecked(-3.79804256470945635097577E+2), TSelf.CreateChecked(6.29331155312818442661052E+2), TSelf.CreateChecked(8.66966202790413211295064E+2), TSelf.CreateChecked(-3.14512729688483675254357E+4), TSelf.CreateChecked(-3.61444134186911729807069E+4), TSelf.CreateChecked(6.64561438202405440627855E+4) };

        // denominator coefficients for approximation over the interval (1,2)
        TSelf[] q = { TSelf.CreateChecked(-3.08402300119738975254353E+1), TSelf.CreateChecked(3.15350626979604161529144E+2), TSelf.CreateChecked(-1.01515636749021914166146E+3), TSelf.CreateChecked(-3.10777167157231109440444E+3), TSelf.CreateChecked(2.25381184209801510330112E+4), TSelf.CreateChecked(4.75584627752788110767815E+3), TSelf.CreateChecked(-1.34659959864969306392456E+5), TSelf.CreateChecked(-1.15132259675553483497211E+5) };

        var num = TSelf.Zero;
        var den = TSelf.One;

        var z = y - TSelf.One;

        for (var i = 0; i < 8; i++)
        {
          num = (num + p[i]) * z;
          den = den * z + q[i];
        }

        var result = num / den + TSelf.One;

        // Apply correction if argument was not initially in (1,2)
        if (arg_was_less_than_one)
        {
          // Use identity gamma(z) = gamma(z+1)/z
          // The variable "result" now holds gamma of the original y + 1
          // Thus we use y-1 to get back the orginal y.
          result /= (y - TSelf.One);
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

      if (x > TSelf.CreateChecked(171.624))
      {
        return TSelf.PositiveInfinity; // Correct answer too large to display. 
      }

      return TSelf.Exp(LogGamma(x));
    }

    /// <summary></summary>
    /// <param name="x">Any positive value.</param>
    public static TSelf LogGamma<TSelf>(TSelf x)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      if (x <= TSelf.Zero)
      {
        throw new System.ArgumentOutOfRangeException(nameof(x), @"Must be positive.");
      }

      if (x < TSelf.CreateChecked(12.0))
      {
        return TSelf.Log(TSelf.Abs(Gamma(x)));
      }

      // Abramowitz and Stegun 6.1.41
      // Asymptotic series should be good to at least 11 or 12 figures
      // For error analysis, see Whittiker and Watson
      // A Course in Modern Analysis (1927), page 252

      var z = TSelf.One / (x * x);

      var sum = TSelf.CreateChecked(-3617.0 / 122400.0);

      sum *= z;
      sum += TSelf.CreateChecked(1.0 / 156.0);

      sum *= z;
      sum += TSelf.CreateChecked(-691.0 / 360360.0);

      sum *= z;
      sum += TSelf.CreateChecked(1.0 / 1188.0);

      sum *= z;
      sum += TSelf.CreateChecked(-1.0 / 1680.0);

      sum *= z;
      sum += TSelf.CreateChecked(1.0 / 1260.0);

      sum *= z;
      sum += TSelf.CreateChecked(-1.0 / 360.0);

      sum *= z;
      sum += TSelf.CreateChecked(1.0 / 12.0);

      var series = sum / x;

      var halfLogTwoPi = TSelf.CreateChecked(0.91893853320467274178032973640562);

      var logGamma = (x - TSelf.One.Divide(2)) * TSelf.Log(x) - x + halfLogTwoPi + series;

      return logGamma;
    }
  }
}
