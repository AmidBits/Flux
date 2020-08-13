namespace Flux.Dsp
{
  public static class Fourier
  {
    /// <summary>Fourier transformation.</summary>
    /// <see cref="https://github.com/andrewkirillov/AForge.NET/blob/master/Sources/Math/FourierTransform.cs"/>
    /// <remarks>The class implements one dimensional and two dimensional Discrete and Fast Fourier Transformation.</remarks>
    public static class FourierTransform
    {
      /// <summary>Fourier transformation direction.</summary>
      public enum Direction
      {
        /// <summary>Forward direction of Fourier transformation.</summary>
        Forward = 1,
        /// <summary>Backward direction of Fourier transformation.</summary>
        Backward = -1
      };

      /// <summary>One dimensional Discrete Fourier Transform.</summary>
      /// <param name="data">Data to transform.</param>
      /// <param name="direction">Transformation direction.</param>
      public static void DFT(System.Numerics.Complex[] data, Direction direction)
      {
        var n = data.Length;
        double arg, cos, sin;
        var dst = new System.Numerics.Complex[n];

        for (var i = 0; i < n; i++) // for each destination element
        {
          dst[i] = System.Numerics.Complex.Zero;

          arg = -(int)direction * 2.0 * System.Math.PI * (double)i / (double)n;

          for (var j = 0; j < n; j++) // sum value elements
          {
            cos = System.Math.Cos(j * arg);
            sin = System.Math.Sin(j * arg);

            dst[i] = new System.Numerics.Complex(dst[i].Real + (data[j].Real * cos - data[j].Imaginary * sin), dst[i].Imaginary + (data[j].Real * sin + data[j].Imaginary * cos));
          }
        }

        if (direction == Direction.Forward) // copy elements
        {
          for (var i = 0; i < n; i++) // devide also for forward transform
          {
            data[i] = new System.Numerics.Complex(dst[i].Real / n, dst[i].Imaginary / n);
          }
        }
        else
        {
          for (var i = 0; i < n; i++)
          {
            data[i] = new System.Numerics.Complex(dst[i].Real, dst[i].Imaginary);
          }
        }
      }

      /// <summary>
      /// Two dimensional Discrete Fourier Transform.
      /// </summary>
      /// 
      /// <param name="data">Data to transform.</param>
      /// <param name="direction">Transformation direction.</param>
      /// 
      public static void DFT2(System.Numerics.Complex[,] data, Direction direction)
      {
        var n = data.GetLength(0);  // rows
        var m = data.GetLength(1);  // columns
        double arg, cos, sin;
        var dst = new System.Numerics.Complex[System.Math.Max(n, m)];

        for (var i = 0; i < n; i++) // process rows
        {
          for (var j = 0; j < m; j++)
          {
            dst[j] = System.Numerics.Complex.Zero;

            arg = -(int)direction * 2.0 * System.Math.PI * (double)j / (double)m;

            for (var k = 0; k < m; k++) // sum value elements
            {
              cos = System.Math.Cos(k * arg);
              sin = System.Math.Sin(k * arg);

              dst[j] = new System.Numerics.Complex(dst[j].Real + (data[i, k].Real * cos - data[i, k].Imaginary * sin), dst[j].Imaginary + (data[i, k].Real * sin + data[i, k].Imaginary * cos));
            }
          }

          if (direction == Direction.Forward) // copy elements
          {
            for (var j = 0; j < m; j++) // devide also for forward transform
            {
              data[i, j] = new System.Numerics.Complex(dst[j].Real / m, dst[j].Imaginary / m);
            }
          }
          else
          {
            for (var j = 0; j < m; j++)
            {
              data[i, j] = new System.Numerics.Complex(dst[j].Real, dst[j].Imaginary);
            }
          }
        }

        for (var j = 0; j < m; j++) // process columns
        {
          for (var i = 0; i < n; i++)
          {
            dst[i] = System.Numerics.Complex.Zero;

            arg = -(int)direction * 2.0 * System.Math.PI * (double)i / (double)n;

            for (var k = 0; k < n; k++) // sum value elements
            {
              cos = System.Math.Cos(k * arg);
              sin = System.Math.Sin(k * arg);

              dst[i] = new System.Numerics.Complex(dst[i].Real + (data[k, j].Real * cos - data[k, j].Imaginary * sin), dst[i].Imaginary + (data[k, j].Real * sin + data[k, j].Imaginary * cos));
            }
          }

          if (direction == Direction.Forward) // copy elements
          {
            for (var i = 0; i < n; i++) // devide also for forward transform
            {
              data[i, j] = new System.Numerics.Complex(dst[i].Real / n, dst[i].Imaginary / n);
            }
          }
          else
          {
            for (var i = 0; i < n; i++)
            {
              data[i, j] = new System.Numerics.Complex(dst[i].Real, dst[i].Imaginary);
            }
          }
        }
      }

      /// <summary>One dimensional Fast Fourier Transform.</summary>
      /// <param name="data">Data to transform.</param>
      /// <param name="direction">Transformation direction.</param>
      /// <remarks><para><note>The method accepts <paramref name="data"/> array of 2<sup>n</sup> size
      /// only, where <b>n</b> may vary in the [1, 14] range.</note></para></remarks>
      /// <exception cref="ArgumentException">Incorrect data length.</exception>
      public static void FFT(System.Numerics.Complex[] data, Direction direction)
      {
        var n = data.Length;
        var m = (int)Flux.Maths.Log2(n);

        ReorderData(data); // reorder data first

        int tn = 1, tm; // compute FFT

        for (var k = 1; k <= m; k++)
        {
          var rotation = FourierTransform.GetComplexRotation(k, direction);

          tm = tn;
          tn <<= 1;

          for (var i = 0; i < tm; i++)
          {
            var t = rotation[i];

            for (var even = i; even < n; even += tn)
            {
              var odd = even + tm;
              var ce = data[even];
              var co = data[odd];

              var tr = co.Real * t.Real - co.Imaginary * t.Imaginary;
              var ti = co.Real * t.Imaginary + co.Imaginary * t.Real;

              data[even] = new System.Numerics.Complex(data[even].Real + tr, data[even].Imaginary + ti);

              data[odd] = new System.Numerics.Complex(ce.Real - tr, ce.Imaginary - ti);
            }
          }
        }

        if (direction == Direction.Forward)
        {
          for (var i = 0; i < n; i++)
          {
            data[i] = new System.Numerics.Complex(data[i].Real / n, data[i].Imaginary / n);
          }
        }
      }

      /// <summary>Two dimensional Fast Fourier Transform.</summary>
      /// <param name="data">Data to transform.</param>
      /// <param name="direction">Transformation direction.</param>
      /// <remarks><para><note>The method accepts <paramref name="data"/> array of 2<sup>n</sup> size
      /// only in each dimension, where <b>n</b> may vary in the [1, 14] range. For example, 16x16 array
      /// is valid, but 15x15 is not.</note></para></remarks>
      /// <exception cref="ArgumentException">Incorrect data length.</exception>
      public static void FFT2(System.Numerics.Complex[,] data, Direction direction)
      {
        var k = data.GetLength(0);
        var n = data.GetLength(1);

        if ((!Flux.Maths.IsPowerOf2(k)) || (!Flux.Maths.IsPowerOf2(n)) || (k < minLength) || (k > maxLength) || (n < minLength) || (n > maxLength))
        {
          throw new System.ArgumentException("Incorrect data length.");
        }

        var row = new System.Numerics.Complex[n]; // process rows

        for (var i = 0; i < k; i++)
        {
          for (var j = 0; j < n; j++) // copy row
          {
            row[j] = data[i, j];
          }

          FourierTransform.FFT(row, direction); // transform it

          for (var j = 0; j < n; j++) // copy back
          {
            data[i, j] = row[j];
          }
        }

        var col = new System.Numerics.Complex[k]; // process columns

        for (var j = 0; j < n; j++)
        {
          for (var i = 0; i < k; i++) // copy column
          {
            col[i] = data[i, j];
          }

          FourierTransform.FFT(col, direction);// transform it

          for (var i = 0; i < k; i++) // copy back
          {
            data[i, j] = col[i];
          }
        }
      }

      #region Private Region

      private const int minLength = 2;
      private const int maxLength = 16384;
      private const int minBits = 1;
      private const int maxBits = 14;
      private readonly static int[][] reversedBits = new int[maxBits][];
      private readonly static System.Numerics.Complex[,][] complexRotation = new System.Numerics.Complex[maxBits, 2][];

      /// <summary>Get array, indicating which data members should be swapped before FFT.</summary>
      private static int[] GetReversedBits(int numberOfBits)
      {
        if ((numberOfBits < minBits) || (numberOfBits > maxBits))
        {
          throw new System.ArgumentOutOfRangeException(nameof(numberOfBits));
        }

        if (reversedBits[numberOfBits - 1] == null) // check if the array is already calculated
        {
          var n = (int)System.Math.Pow(numberOfBits, 2);
          var rBits = new int[n];

          for (var i = 0; i < n; i++) // calculate the array
          {
            var oldBits = i;
            var newBits = 0;

            for (var j = 0; j < numberOfBits; j++)
            {
              newBits = (newBits << 1) | (oldBits & 1);
              oldBits = (oldBits >> 1);
            }

            rBits[i] = newBits;
          }

          reversedBits[numberOfBits - 1] = rBits;
        }
        return reversedBits[numberOfBits - 1];
      }

      private static System.Numerics.Complex[] GetComplexRotation(int numberOfBits, Direction direction) // Get rotation of complex number
      {
        var directionIndex = (direction == Direction.Forward) ? 0 : 1;

        if (complexRotation[numberOfBits - 1, directionIndex] == null) // check if the array is already calculated
        {
          var n = 1 << (numberOfBits - 1);
          var uR = 1.0;
          var uI = 0.0;
          var angle = System.Math.PI / n * (int)direction;
          var wR = System.Math.Cos(angle);
          var wI = System.Math.Sin(angle);
          double t;
          var rotation = new System.Numerics.Complex[n];

          for (var i = 0; i < n; i++)
          {
            rotation[i] = new System.Numerics.Complex(uR, uI);
            t = uR * wI + uI * wR;
            uR = uR * wR - uI * wI;
            uI = t;
          }

          complexRotation[numberOfBits - 1, directionIndex] = rotation;
        }

        return complexRotation[numberOfBits - 1, directionIndex];
      }

      /// <summary>Reorder data for FFT using</summary>
      private static void ReorderData(System.Numerics.Complex[] data)
      {
        var len = data.Length;

        // check data length
        if ((len < minLength) || (len > maxLength) || (!Flux.Maths.IsPowerOf2(len)))
        {
          throw new System.ArgumentException("Incorrect data length.");
        }

        var rBits = GetReversedBits((int)Flux.Maths.Log2(len));

        for (var i = 0; i < len; i++)
        {
          var s = rBits[i];

          if (s > i)
          {
            var t = data[i];
            data[i] = data[s];
            data[s] = t;
          }
        }
      }

      #endregion
    }
  }
}
