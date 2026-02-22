namespace Flux
{
  public static partial class DoubleExtensions
  {
    extension(System.Double)
    {
      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Double"/> without losing precision is <c>9,007,199,254,740,992</c>.</para>
      /// <para>This is because a <see cref="System.Double"/> is a base-2/binary double-precision floating point with a 53-bit mantissa and 15-16 digits of precision, which means it can precisely represent integers up to 9,007,199,254,740,992 = <c>(1 &lt;&lt; 53)</c> = 2⁵³, before precision starts to degrade.</para>
      /// </summary>
      public static double MaxExactInteger => +9007199254740992;

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Double"/> without losing precision is <c>-9,007,199,254,740,992</c>.</para>
      /// <para>This is because a <see cref="System.Double"/> is a base-2/binary double-precision floating point with a 53-bit mantissa and 15-16 digits of precision, which means it can precisely represent integers down to -9,007,199,254,740,992 = <c>-(1 &lt;&lt; 53)</c> = -2⁵³, before precision starts to degrade.</para>
      /// </summary>
      public static double MinExactInteger => -9007199254740992;

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a double.</para>
      /// </summary>
      public static double MaxExactPrimeNumber => 9007199254740881;

      /// <summary>
      /// <para>A <see cref="System.Double"/> has a precision of about 15-17 significant digits.</para>
      /// </summary>
      public static int MaxExactSignificantDigits => 15;

      /// <summary>
      /// <para>The default base epsilon (1e-12d) used for near-equality functions.</para>
      /// </summary>
      public static double DefaultBaseEpsilon => 1e-12d;

      #region GetComponents

      /// <summary>
      /// <para>Get the three binary64 parts of a 64-bit floating point both raw (but shifted to LSB) as out parameters and returned adjusted (see below).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Double-precision_floating-point_format"/></para>
      /// </summary>
      /// <param name="binary64SignBit">This is 1 single sign bit. 0 = positive, 1 = negative.</param>
      /// <param name="binary64ExponentBiased">This is an 8-bit exponent in biased form, where the values [1, 2046] (-1022 to +1023) represents the actual exponent. The two remaining values 0 (-1023) and 2047 (+1024) are reserved for special numbers.</param>
      /// <param name="binary64Significand52"></param>
      /// <returns>
      /// <para>The three adjusted binary64 parts as a tuple: <c>(int Binary64Sign = 1 or -1, int Binary64ExponentUnbiased = [−1022, +1023], long Binary64Significand53 = [0, <see cref="MaxPreciseInteger"/>])</c>.</para>
      /// </returns>
      public static (int Sign, int ExponentUnbiased, long Significand53) GetComponents(System.Double value, out int signBit, out int exponentBiased, out long significand52)
      {
        var bits = System.BitConverter.DoubleToUInt64Bits(value);

        signBit = (int)((bits & 0x8000000000000000UL) >>> 63);

        exponentBiased = (int)((bits & 0x7FF0000000000000UL) >>> 52);

        significand52 = (long)(bits & 0x000FFFFFFFFFFFFFUL);

        var sign = signBit == 0 ? 1 : -1;

        var exponentUnbiased = exponentBiased - 1023;

        var significand53 = 0x0010000000000000L | significand52; // This is the significandPrecision above with the hidden 53-bit added.

        return (sign, exponentUnbiased, significand53);
      }

      #endregion

      #region GetParts

      /// <summary>
      /// <para>Get the integral part and the fractional part of a <see cref="System.Double"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/33996511/3178666"/></para>
      /// </summary>
      /// <returns>
      /// <para>The integral (integer) part and the fractional part of a 64-bit floating point value.</para>
      /// </returns>
      public static (double IntegralPart, double FractionalPart) GetParts(System.Double value)
      {
        var integralPart = double.Truncate(value);
        var fractionalPart = value - integralPart;

        return (integralPart, fractionalPart);
      }

      #endregion

      #region Native..

      public static double NativeDecrement(double value)
        => double.IsNaN(value) || double.IsNegativeInfinity(value)
        ? throw new System.ArithmeticException(value.ToString())
        : double.IsPositiveInfinity(value)
        ? double.MaxValue
        : double.BitDecrement(value);

      public static double NativeIncrement(double value)
        => double.IsNaN(value) || double.IsPositiveInfinity(value)
        ? throw new System.ArithmeticException(value.ToString())
        : double.IsNegativeInfinity(value)
        ? double.MinValue
        : double.BitIncrement(value);

      #endregion

      #region Special functions

      /// <summary>Implementation see reference.</summary>
      /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
      /// <see href="https://www.johndcook.com/blog/2009/01/19/stand-alone-error-function-erf/"/>
      public static double Erf(double x)
      {
        var a1 = 0.254829592;
        var a2 = -0.284496736;
        var a3 = 1.421413741;
        var a4 = -1.453152027;
        var a5 = 1.061405429;

        var p = 0.3275911;

        var absx = double.Abs(x);

        // A&S formula 7.1.26
        var t = 1 / (1 + p * absx);
        var y = 1 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * double.Exp(-absx * absx);

        return double.CopySign(y, x);
      }

      /// <summary>Compute log(1+x) without losing precision for small values of x.</summary>
      /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
      /// <see href="https://www.johndcook.com/blog/csharp_expm1/"/>
      public static double Expm1(double x)
        => double.Abs(x) < 1e-5
        ? x + 0.5 * x * x
        : double.Exp(x) - 1;

      /// <summary></summary>
      /// <param name="x">Any positive value.</param>
      /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
      /// <para><see href="https://www.johndcook.com/blog/stand_alone_code/"/></para>
      /// <para><see href="https://www.johndcook.com/blog/csharp_gamma/"/></para>
      public static double Gamma(double x)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(x);

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

      /// <summary>
      /// <para>Lanczos approximation of the gamma function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Lanczos_approximation"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static double GammaLanczosApproximation(double x)
      {
        var g = 7;

        if (x < 0.5)
          return double.Pi / (double.SinPi(x) * GammaLanczosApproximation(1 - x));

        x -= 1;

        var a = 0.99999999999980993;
        var t = x + g + 0.5;

        a += 676.5203681218851 / (x + 1);
        a += -1259.1392167224028 / (x + 2);
        a += 771.32342877765313 / (x + 3);
        a += -176.61502916214059 / (x + 4);
        a += 12.507343278686905 / (x + 5);
        a += -0.13857109526572012 / (x + 6);
        a += 9.9843695780195716e-6 / (x + 7);
        a += 1.5056327351493116e-7 / (x + 8);

        return double.Sqrt(double.Tau) * double.Pow(t, x + 0.5) * double.Exp(-t) * a;
      }

      /// <summary>
      /// <para>Spouge's approximation.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Spouge%27s_approximation"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="coefficients">A variable number of coefficients from <see cref="Coefficients(int)"/>.</param>
      /// <returns></returns>
      public static double GammaSpougesApproximation(double x, System.Collections.Generic.List<double> coefficients)
      {
        var accm = coefficients[0];

        for (var k = 1; k < coefficients.Count; k++)
          accm += coefficients[k] / (x + k);

        accm *= double.Exp(-(x + coefficients.Count)) * double.Pow(x + coefficients.Count, x + 0.5);

        return accm / x;
      }

      public static double GammaSpougesApproximation13(double x)
        => GammaSpougesApproximation(x, m_coefficients13);

      /// <summary>
      /// <para>Stirling's approximation.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Stirling%27s_approximation"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static double GammaStirlingsApproximation(double x)
        => double.Sqrt(double.Tau / x) * double.Pow((x / double.E), x);

      /// <summary>Compute log(1+x) without losing precision for small values of x.</summary>
      /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
      /// <see href="https://www.johndcook.com/blog/csharp_log_one_plus_x/"/>
      public static double Log1p(double x)
      {
        if (x <= -1) throw new ArgumentOutOfRangeException(nameof(x));

        if (double.Abs(x) > 1e-4) // x is large enough that the obvious evaluation is OK
          return double.Log(1.0 + x);

        // Use Taylor approx. log(1 + x) = x - x^2/2 with error roughly x^3/3
        // Since |x| < 10^-4, |x|^3 < 10^-12, relative error less than 10^-8

        return (-0.5 * x + 1) * x;
      }

      /// <summary>
      /// <para>LogGamma (LGamma) function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function#Log-gamma_function"/></para>
      /// <para><see href="https://www.johndcook.com/blog/stand_alone_code/"/></para>
      /// <para><see href="https://www.johndcook.com/blog/csharp_gamma/"/></para>
      /// </summary>
      /// <param name="x">Any positive value.</param>
      public static double LogGamma(double x)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(x);

        if (x < 12.0)
          return double.Log(double.Abs(Gamma(x)));

        // Abramowitz and Stegun 6.1.41
        // Asymptotic series should be good to at least 11 or 12 figures
        // For error analysis, see Whittiker and Watson
        // A Course in Modern Analysis (1927), page 252

        var z = 1 / (x * x);

        var sum = -3617.0 / 122400.0;

        sum *= z;
        sum += 1.0 / 156.0;

        sum *= z;
        sum += -691.0 / 360360.0;

        sum *= z;
        sum += 1.0 / 1188.0;

        sum *= z;
        sum += -1.0 / 1680.0;

        sum *= z;
        sum += 1.0 / 1260.0;

        sum *= z;
        sum += -1.0 / 360.0;

        sum *= z;
        sum += 1.0 / 12.0;

        var series = sum / x;

        var halfLogTwoPi = 0.91893853320467274178032973640562;

        var logGamma = (x - 0.5) * double.Log(x) - x + halfLogTwoPi + series;

        return logGamma;
      }

      /// <summary>Compute the inverse of the normal (Gaussian) CDF. </summary>
      /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
      /// <see href="https://www.johndcook.com/blog/normal_cdf_inverse/"/>
      public static double NormalCdfInverse(double x)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(x);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(x, 1.0);

        // See article above for explanation of the following section.
        return (x < 0.5)
          ? -RationalApproximation(double.Sqrt(-2 * double.Log(x))) // F^-1(p) = - G^-1(p)
          : RationalApproximation(double.Sqrt(-2 * double.Log(1 - x))); // F^-1(p) = G^-1(1-p)
      }

      /// <summary>Implementation see reference.</summary>
      /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
      /// <see href="https://www.johndcook.com/blog/csharp_phi/"/>
      public static double Phi(double x)
      {
        var a1 = 0.254829592;
        var a2 = -0.284496736;
        var a3 = 1.421413741;
        var a4 = -1.453152027;
        var a5 = 1.061405429;

        var p = 0.3275911;

        var absx = double.Abs(x) / double.Sqrt(2);

        // A&S formula 7.1.26
        var t = 1 / (1 + p * absx);
        var y = 1 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * double.Exp(-absx * absx);

        return 0.5 * (1 + double.CopySign(y, x));
      }

      /// <summary>Implementation see reference.</summary>
      /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
      /// <see href="https://www.johndcook.com/blog/normal_cdf_inverse/"/>
      /// <seealso href="https://en.wikipedia.org/wiki/Horner%27s_method"/>
      public static double RationalApproximation(double x)
      {
        // Abramowitz and Stegun formula 26.2.23. The absolute value of the error should be less than 4.5 e-4.

        var c0 = 2.515517;
        var c1 = 0.802853;
        var c2 = 0.010328;

        var d0 = 1.432788;
        var d1 = 0.189269;
        var d2 = 0.001308;

        return x - ((c2 * x + c1) * x + c0) / (((d2 * x + d1) * x + d0) * x + 1);
      }

      #endregion
    }

    #region Special functions (helpers)

    #region LogFactorial (helpers)

    #region Lookup table

    private static readonly double[] m_logFactorialTable =
    {
      0.000000000000000,
      0.000000000000000,
      0.693147180559945,
      1.791759469228055,
      3.178053830347946,
      4.787491742782046,
      6.579251212010101,
      8.525161361065415,
      10.604602902745251,
      12.801827480081469,
      15.104412573075516,
      17.502307845873887,
      19.987214495661885,
      22.552163853123421,
      25.191221182738683,
      27.899271383840894,
      30.671860106080675,
      33.505073450136891,
      36.395445208033053,
      39.339884187199495,
      42.335616460753485,
      45.380138898476908,
      48.471181351835227,
      51.606675567764377,
      54.784729398112319,
      58.003605222980518,
      61.261701761002001,
      64.557538627006323,
      67.889743137181526,
      71.257038967168000,
      74.658236348830158,
      78.092223553315307,
      81.557959456115029,
      85.054467017581516,
      88.580827542197682,
      92.136175603687079,
      95.719694542143202,
      99.330612454787428,
      102.968198614513810,
      106.631760260643450,
      110.320639714757390,
      114.034211781461690,
      117.771881399745060,
      121.533081515438640,
      125.317271149356880,
      129.123933639127240,
      132.952575035616290,
      136.802722637326350,
      140.673923648234250,
      144.565743946344900,
      148.477766951773020,
      152.409592584497350,
      156.360836303078800,
      160.331128216630930,
      164.320112263195170,
      168.327445448427650,
      172.352797139162820,
      176.395848406997370,
      180.456291417543780,
      184.533828861449510,
      188.628173423671600,
      192.739047287844900,
      196.866181672889980,
      201.009316399281570,
      205.168199482641200,
      209.342586752536820,
      213.532241494563270,
      217.736934113954250,
      221.956441819130360,
      226.190548323727570,
      230.439043565776930,
      234.701723442818260,
      238.978389561834350,
      243.268849002982730,
      247.572914096186910,
      251.890402209723190,
      256.221135550009480,
      260.564940971863220,
      264.921649798552780,
      269.291097651019810,
      273.673124285693690,
      278.067573440366120,
      282.474292687630400,
      286.893133295426990,
      291.323950094270290,
      295.766601350760600,
      300.220948647014100,
      304.686856765668720,
      309.164193580146900,
      313.652829949878990,
      318.152639620209300,
      322.663499126726210,
      327.185287703775200,
      331.717887196928470,
      336.261181979198450,
      340.815058870798960,
      345.379407062266860,
      349.954118040770250,
      354.539085519440790,
      359.134205369575340,
      363.739375555563470,
      368.354496072404690,
      372.979468885689020,
      377.614197873918670,
      382.258588773060010,
      386.912549123217560,
      391.575988217329610,
      396.248817051791490,
      400.930948278915760,
      405.622296161144900,
      410.322776526937280,
      415.032306728249580,
      419.750805599544780,
      424.478193418257090,
      429.214391866651570,
      433.959323995014870,
      438.712914186121170,
      443.475088120918940,
      448.245772745384610,
      453.024896238496130,
      457.812387981278110,
      462.608178526874890,
      467.412199571608080,
      472.224383926980520,
      477.044665492585580,
      481.872979229887900,
      486.709261136839360,
      491.553448223298010,
      496.405478487217580,
      501.265290891579240,
      506.132825342034830,
      511.008022665236070,
      515.890824587822520,
      520.781173716044240,
      525.679013515995050,
      530.584288294433580,
      535.496943180169520,
      540.416924105997740,
      545.344177791154950,
      550.278651724285620,
      555.220294146894960,
      560.169054037273100,
      565.124881094874350,
      570.087725725134190,
      575.057539024710200,
      580.034272767130800,
      585.017879388839220,
      590.008311975617860,
      595.005524249382010,
      600.009470555327430,
      605.020105849423770,
      610.037385686238740,
      615.061266207084940,
      620.091704128477430,
      625.128656730891070,
      630.172081847810200,
      635.221937855059760,
      640.278183660408100,
      645.340778693435030,
      650.409682895655240,
      655.484856710889060,
      660.566261075873510,
      665.653857411105950,
      670.747607611912710,
      675.847474039736880,
      680.953419513637530,
      686.065407301994010,
      691.183401114410800,
      696.307365093814040,
      701.437263808737160,
      706.573062245787470,
      711.714725802289990,
      716.862220279103440,
      722.015511873601330,
      727.174567172815840,
      732.339353146739310,
      737.509837141777440,
      742.685986874351220,
      747.867770424643370,
      753.055156230484160,
      758.248113081374300,
      763.446610112640200,
      768.650616799717000,
      773.860102952558460,
      779.075038710167410,
      784.295394535245690,
      789.521141208958970,
      794.752249825813460,
      799.988691788643450,
      805.230438803703120,
      810.477462875863580,
      815.729736303910160,
      820.987231675937890,
      826.249921864842800,
      831.517780023906310,
      836.790779582469900,
      842.068894241700490,
      847.352097970438420,
      852.640365001133090,
      857.933669825857460,
      863.231987192405430,
      868.535292100464630,
      873.843559797865740,
      879.156765776907600,
      884.474885770751830,
      889.797895749890240,
      895.125771918679900,
      900.458490711945270,
      905.796028791646340,
      911.138363043611210,
      916.485470574328820,
      921.837328707804890,
      927.193914982476710,
      932.555207148186240,
      937.921183163208070,
      943.291821191335660,
      948.667099599019820,
      954.046996952560450,
      959.431492015349480,
      964.820563745165940,
      970.214191291518320,
      975.612353993036210,
      981.015031374908400,
      986.422203146368590,
      991.833849198223450,
      997.249949600427840,
      1002.670484599700300,
      1008.095434617181700,
      1013.524780246136200,
      1018.958502249690200,
      1024.396581558613400,
      1029.838999269135500,
      1035.285736640801600,
      1040.736775094367400,
      1046.192096209724900,
      1051.651681723869200,
      1057.115513528895000,
      1062.583573670030100,
      1068.055844343701400,
      1073.532307895632800,
      1079.012946818975000,
      1084.497743752465600,
      1089.986681478622400,
      1095.479742921962700,
      1100.976911147256000,
      1106.478169357800900,
      1111.983500893733000,
      1117.492889230361000,
      1123.006317976526100,
      1128.523770872990800,
      1134.045231790853000,
      1139.570684729984800,
      1145.100113817496100,
      1150.633503306223700,
      1156.170837573242400,
    };

    #endregion

    public static double LogFactorial<TInteger>(TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(n);

      if (n > TInteger.CreateChecked(254))
      {
        var x = double.CreateChecked(n + TInteger.One);

        return (x - 0.5) * double.Log(x) - x + 0.5 * double.Log(double.Tau) + 1.0 / (12.0 * x);
      }
      else
      {
        return m_logFactorialTable[int.CreateChecked(n)];
      }
    }

    #endregion

    #region SpougesGammaApproximation.. (helpers)

    private static readonly System.Collections.Generic.List<double> m_coefficients13 = SpougesGammaApproximationCoefficients(13);

    /// <summary>
    /// <para>Spouge's coefficients for computing approximation of the gamma function .</para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<double> SpougesGammaApproximationCoefficients(int count)
    {
      var c = new System.Collections.Generic.List<double>(count);

      var k1_factrl = 1.0;

      c.Add(double.Sqrt(double.Tau));

      for (var k = 1; k < count; k++)
      {
        c.Add(double.Exp(count - k) * double.Pow(count - k, k - 0.5) / k1_factrl);

        k1_factrl *= -(double)k;
      }

      return c;
    }

    #endregion

    ///// <summary>
    ///// <para>Coefficients for Lanczos approximation when (g=7, n=9).</para>
    ///// </summary>
    //private static readonly double[] p = {
    //  0.99999999999980993,
    //  676.5203681218851,
    //  -1259.1392167224028,
    //  771.32342877765313,
    //  -176.61502916214059,
    //  12.507343278686905,
    //  -0.13857109526572012,
    //  9.9843695780195716e-6,
    //  1.5056327351493116e-7
    //};

    ///// <summary>
    ///// <para>The Gamma function. The (g, n) values are currently hardcoded for (g=7, n=9).</para>
    ///// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
    ///// <para><see href="https://en.wikipedia.org/wiki/Lanczos_approximation"/></para>
    ///// </summary>
    ///// <param name="z"></param>
    ///// <returns></returns>
    //public static double GammaLanczos(this double z)
    //{
    //  var y = 0.0;

    //  if (z < 0.5)
    //    y = double.Pi / (double.SinPi(z) * GammaLanczos(1 - z)); // Reflection formula.
    //  else // Lanczos approximation (invalid for above condition).
    //  {
    //    z -= 1;

    //    var x = p[0];

    //    for (var i = 1; i < 9; i++) // Changed to: 9 from (g + 2)
    //      x += p[i] / (z + i);

    //    var t = z + 7 + 0.5; // Changed to: 7 from (g)

    //    y = double.Sqrt(2 * double.Pi) * double.Pow(t, z + 0.5) * double.Exp(-t) * x;
    //  }

    //  return y;
    //}

    //public static TSelf GammaLanczos<TSelf>(this TSelf z)
    //  where TSelf : System.Numerics.IFloatingPointIeee754<TSelf> // System.Numerics.IFloatingPointConstants<TSelf>, System.Numerics.IExponentialFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    //{
    //  var y = TSelf.Zero;

    //  var halfOf1 = TSelf.CreateChecked(0.5);

    //  if (z < halfOf1)
    //    y = TSelf.Pi / (TSelf.SinPi(z) * GammaLanczos(TSelf.One - z)); // Reflection formula.
    //  else // Lanczos approximation (invalid for above condition).
    //  {
    //    z -= TSelf.One;

    //    var x = TSelf.CreateChecked(p[0]);

    //    for (var i = 1; i < 9; i++) // Changed to: 9 from (g + 2)
    //      x += TSelf.CreateChecked(p[i]) / (z + TSelf.CreateChecked(i));

    //    var t = z + TSelf.CreateChecked(7) + halfOf1; // Changed to: 7 from (g)

    //    y = TSelf.Sqrt(TSelf.Tau) * TSelf.Pow(t, z + halfOf1) * TSelf.Exp(-t) * x;
    //  }

    //  return y;
    //}

    #endregion
  }
}
