//#if NET7_0_OR_GREATER
//namespace Flux
//{
//  #region ExtensionMethods
//  public static partial class ExtensionMethodsCoordinateSystems
//  {
//    /// <summary>Computes the determinant (generally) of the matrix.</summary>
//    public static double GetDeterminantGeneral(this Geometry.IMatrix4 source)
//      => source.M14 * source.M23 * source.M32 * source.M41 - source.M13 * source.M24 * source.M32 * source.M41 - source.M14 * source.M22 * source.M33 * source.M41 + source.M12 * source.M24 * source.M33 * source.M41 + source.M13 * source.M22 * source.M34 * source.M41 - source.M12 * source.M23 * source.M34 * source.M41 - source.M14 * source.M23 * source.M31 * source.M42 + source.M13 * source.M24 * source.M31 * source.M42 + source.M14 * source.M21 * source.M33 * source.M42 - source.M11 * source.M24 * source.M33 * source.M42 - source.M13 * source.M21 * source.M34 * source.M42 + source.M11 * source.M23 * source.M34 * source.M42 + source.M14 * source.M22 * source.M31 * source.M43 - source.M12 * source.M24 * source.M31 * source.M43 - source.M14 * source.M21 * source.M32 * source.M43 + source.M11 * source.M24 * source.M32 * source.M43 + source.M12 * source.M21 * source.M34 * source.M43 - source.M11 * source.M22 * source.M34 * source.M43 - source.M13 * source.M22 * source.M31 * source.M44 + source.M12 * source.M23 * source.M31 * source.M44 + source.M13 * source.M21 * source.M32 * source.M44 - source.M11 * source.M23 * source.M32 * source.M44 - source.M12 * source.M21 * source.M33 * source.M44 + source.M11 * source.M22 * source.M33 * source.M44;

//    /// <summary>Computes the determinant (optimized) of the matrix.</summary>
//    public static double GetDeterminantOptimized(this Geometry.IMatrix4 source)
//    {
//      // | a b c d |     | f g h |     | e g h |     | e f h |     | e f g |
//      // | e f g h | = a | j k l | - b | i k l | + c | i j l | - d | i j k |
//      // | i j k l |     | n o p |     | m o p |     | m n p |     | m n o |
//      // | m n o p |
//      //
//      //   | f g h |
//      // a | j k l | = a ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
//      //   | n o p |
//      //
//      //   | e g h |     
//      // b | i k l | = b ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
//      //   | m o p |     
//      //
//      //   | e f h |
//      // c | i j l | = c ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
//      //   | m n p |
//      //
//      //   | e f g |
//      // d | i j k | = d ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
//      //   | m n o |
//      //
//      // Cost of operation
//      // 17 adds and 28 muls.
//      //
//      // add: 6 + 8 + 3 = 17
//      // mul: 12 + 16 = 28

//      double a = source.M11, b = source.M12, c = source.M13, d = source.M14;
//      double e = source.M21, f = source.M22, g = source.M23, h = source.M24;
//      double i = source.M31, j = source.M32, k = source.M33, l = source.M34;
//      double m = source.M41, n = source.M42, o = source.M43, p = source.M44;

//      var kp_lo = k * p - l * o;
//      var jp_ln = j * p - l * n;
//      var jo_kn = j * o - k * n;
//      var ip_lm = i * p - l * m;
//      var io_km = i * o - k * m;
//      var in_jm = i * n - j * m;

//      return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
//             b * (e * kp_lo - g * ip_lm + h * io_km) +
//             c * (e * jp_ln - f * ip_lm + h * in_jm) -
//             d * (e * jo_kn - f * io_km + g * in_jm);
//    }

//    /// <summary>Computes the general inverse of the matrix.</summary>
//    public static Geometry.Matrix4 GetInverseGeneral<TSelf>(this Geometry.IMatrix4 source)
//    {
//      var det = 1 / source.GetDeterminantGeneral();

//      return new(
//        (source.M23 * source.M34 * source.M42 - source.M24 * source.M33 * source.M42 + source.M24 * source.M32 * source.M43 - source.M22 * source.M34 * source.M43 - source.M23 * source.M32 * source.M44 + source.M22 * source.M33 * source.M44) * det,
//        (source.M14 * source.M33 * source.M42 - source.M13 * source.M34 * source.M42 - source.M14 * source.M32 * source.M43 + source.M12 * source.M34 * source.M43 + source.M13 * source.M32 * source.M44 - source.M12 * source.M33 * source.M44) * det,
//        (source.M13 * source.M24 * source.M42 - source.M14 * source.M23 * source.M42 + source.M14 * source.M22 * source.M43 - source.M12 * source.M24 * source.M43 - source.M13 * source.M22 * source.M44 + source.M12 * source.M23 * source.M44) * det,
//        (source.M14 * source.M23 * source.M32 - source.M13 * source.M24 * source.M32 - source.M14 * source.M22 * source.M33 + source.M12 * source.M24 * source.M33 + source.M13 * source.M22 * source.M34 - source.M12 * source.M23 * source.M34) * det,

//        (source.M24 * source.M33 * source.M41 - source.M23 * source.M34 * source.M41 - source.M24 * source.M31 * source.M43 + source.M21 * source.M34 * source.M43 + source.M23 * source.M31 * source.M44 - source.M21 * source.M33 * source.M44) * det,
//        (source.M13 * source.M34 * source.M41 - source.M14 * source.M33 * source.M41 + source.M14 * source.M31 * source.M43 - source.M11 * source.M34 * source.M43 - source.M13 * source.M31 * source.M44 + source.M11 * source.M33 * source.M44) * det,
//        (source.M14 * source.M23 * source.M41 - source.M13 * source.M24 * source.M41 - source.M14 * source.M21 * source.M43 + source.M11 * source.M24 * source.M43 + source.M13 * source.M21 * source.M44 - source.M11 * source.M23 * source.M44) * det,
//        (source.M13 * source.M24 * source.M31 - source.M14 * source.M23 * source.M31 + source.M14 * source.M21 * source.M33 - source.M11 * source.M24 * source.M33 - source.M13 * source.M21 * source.M34 + source.M11 * source.M23 * source.M34) * det,

//        (source.M22 * source.M34 * source.M41 - source.M24 * source.M32 * source.M41 + source.M24 * source.M31 * source.M42 - source.M21 * source.M34 * source.M42 - source.M22 * source.M31 * source.M44 + source.M21 * source.M32 * source.M44) * det,
//        (source.M14 * source.M32 * source.M41 - source.M12 * source.M34 * source.M41 - source.M14 * source.M31 * source.M42 + source.M11 * source.M34 * source.M42 + source.M12 * source.M31 * source.M44 - source.M11 * source.M32 * source.M44) * det,
//        (source.M12 * source.M24 * source.M41 - source.M14 * source.M22 * source.M41 + source.M14 * source.M21 * source.M42 - source.M11 * source.M24 * source.M42 - source.M12 * source.M21 * source.M44 + source.M11 * source.M22 * source.M44) * det,
//        (source.M14 * source.M22 * source.M31 - source.M12 * source.M24 * source.M31 - source.M14 * source.M21 * source.M32 + source.M11 * source.M24 * source.M32 + source.M12 * source.M21 * source.M34 - source.M11 * source.M22 * source.M34) * det,

//        (source.M23 * source.M32 * source.M41 - source.M22 * source.M33 * source.M41 - source.M23 * source.M31 * source.M42 + source.M21 * source.M33 * source.M42 + source.M22 * source.M31 * source.M43 - source.M21 * source.M32 * source.M43) * det,
//        (source.M12 * source.M33 * source.M41 - source.M13 * source.M32 * source.M41 + source.M13 * source.M31 * source.M42 - source.M11 * source.M33 * source.M42 - source.M12 * source.M31 * source.M43 + source.M11 * source.M32 * source.M43) * det,
//        (source.M13 * source.M22 * source.M41 - source.M12 * source.M23 * source.M41 - source.M13 * source.M21 * source.M42 + source.M11 * source.M23 * source.M42 + source.M12 * source.M21 * source.M43 - source.M11 * source.M22 * source.M43) * det,
//        (source.M12 * source.M23 * source.M31 - source.M13 * source.M22 * source.M31 + source.M13 * source.M21 * source.M32 - source.M11 * source.M23 * source.M32 - source.M12 * source.M21 * source.M33 + source.M11 * source.M22 * source.M33) * det
//      );
//    }

//    /// <summary>Linearly interpolates between the corresponding values of two matrices.</summary>
//    /// <param name="amount">The relative weight of the second source matrix.</param>
//    public static Geometry.Matrix4 Lerp(this Geometry.IMatrix4 m1, Geometry.IMatrix4 m2, double amount)
//      => new(
//        // First row
//        m1.M11 + (m2.M11 - m1.M11) * amount,
//        m1.M12 + (m2.M12 - m1.M12) * amount,
//        m1.M13 + (m2.M13 - m1.M13) * amount,
//        m1.M14 + (m2.M14 - m1.M14) * amount,
//        // Second row
//        m1.M21 + (m2.M21 - m1.M21) * amount,
//        m1.M22 + (m2.M22 - m1.M22) * amount,
//        m1.M23 + (m2.M23 - m1.M23) * amount,
//        m1.M24 + (m2.M24 - m1.M24) * amount,
//        // Third row
//        m1.M31 + (m2.M31 - m1.M31) * amount,
//        m1.M32 + (m2.M32 - m1.M32) * amount,
//        m1.M33 + (m2.M33 - m1.M33) * amount,
//        m1.M34 + (m2.M34 - m1.M34) * amount,
//        // Fourth row
//        m1.M41 + (m2.M41 - m1.M41) * amount,
//        m1.M42 + (m2.M42 - m1.M42) * amount,
//        m1.M43 + (m2.M43 - m1.M43) * amount,
//        m1.M44 + (m2.M44 - m1.M44) * amount
//      );

//    /// <summary>Creates a new matrix with the elements negated.</summary>
//    public static Geometry.Matrix4 Negate(this Geometry.IMatrix4 source)
//      => new(-source.M11, -source.M12, -source.M13, -source.M14, -source.M21, -source.M22, -source.M23, -source.M24, -source.M31, -source.M32, -source.M33, -source.M34, -source.M41, -source.M42, -source.M43, -source.M44);

//    /// <summary>Calculates the determinant of the matrix.</summary>
//    //public static double OptimizedDeterminant(Matrix4 matrix)
//    //{
//    //  // | a b c d |     | f g h |     | e g h |     | e f h |     | e f g |
//    //  // | e f g h | = a | j k l | - b | i k l | + c | i j l | - d | i j k |
//    //  // | i j k l |     | n o p |     | m o p |     | m n p |     | m n o |
//    //  // | m n o p |
//    //  //
//    //  //   | f g h |
//    //  // a | j k l | = a ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
//    //  //   | n o p |
//    //  //
//    //  //   | e g h |     
//    //  // b | i k l | = b ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
//    //  //   | m o p |     
//    //  //
//    //  //   | e f h |
//    //  // c | i j l | = c ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
//    //  //   | m n p |
//    //  //
//    //  //   | e f g |
//    //  // d | i j k | = d ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
//    //  //   | m n o |
//    //  //
//    //  // Cost of operation
//    //  // 17 adds and 28 muls.
//    //  //
//    //  // add: 6 + 8 + 3 = 17
//    //  // mul: 12 + 16 = 28

//    //  double a = matrix.m_11, b = matrix.m_12, c = matrix.m_13, d = matrix.m_14;
//    //  double e = matrix.m_21, f = matrix.m_22, g = matrix.m_23, h = matrix.m_24;
//    //  double i = matrix.m_31, j = matrix.m_32, k = matrix.m_33, l = matrix.m_34;
//    //  double m = matrix.m_41, n = matrix.m_42, o = matrix.m_43, p = matrix.m_44;

//    //  double kp_lo = k * p - l * o;
//    //  double jp_ln = j * p - l * n;
//    //  double jo_kn = j * o - k * n;
//    //  double ip_lm = i * p - l * m;
//    //  double io_km = i * o - k * m;
//    //  double in_jm = i * n - j * m;

//    //  return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
//    //         b * (e * kp_lo - g * ip_lm + h * io_km) +
//    //         c * (e * jp_ln - f * ip_lm + h * in_jm) -
//    //         d * (e * jo_kn - f * io_km + g * in_jm);
//    //}
//    /// <summary>
//    /// Attempts to calculate the inverse of the given matrix. If successful, result will contain the inverted matrix.
//    /// </summary>
//    /// <param name="matrix">The source matrix to invert.</param>
//    /// <param name="result">If successful, contains the inverted matrix.</param>
//    /// <returns>True if the source matrix could be inverted; False otherwise.</returns>
//    //public static bool OptimizedInverse(Matrix4 matrix, out Matrix4 result)
//    //{
//    //  //                                       -1
//    //  // If you have matrix M, inverse Matrix M   can compute
//    //  //
//    //  //     -1       1      
//    //  //    M   = --------- A
//    //  //            det(M)
//    //  //
//    //  // A is adjugate (adjoint) of M, where,
//    //  //
//    //  //      T
//    //  // A = C
//    //  //
//    //  // C is Cofactor matrix of M, where,
//    //  //           i + j
//    //  // C   = (-1)      * det(M  )
//    //  //  ij                    ij
//    //  //
//    //  //     [ a b c d ]
//    //  // M = [ e f g h ]
//    //  //     [ i j k l ]
//    //  //     [ m n o p ]
//    //  //
//    //  // First Row
//    //  //           2 | f g h |
//    //  // C   = (-1)  | j k l | = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
//    //  //  11         | n o p |
//    //  //
//    //  //           3 | e g h |
//    //  // C   = (-1)  | i k l | = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
//    //  //  12         | m o p |
//    //  //
//    //  //           4 | e f h |
//    //  // C   = (-1)  | i j l | = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
//    //  //  13         | m n p |
//    //  //
//    //  //           5 | e f g |
//    //  // C   = (-1)  | i j k | = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
//    //  //  14         | m n o |
//    //  //
//    //  // Second Row
//    //  //           3 | b c d |
//    //  // C   = (-1)  | j k l | = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
//    //  //  21         | n o p |
//    //  //
//    //  //           4 | a c d |
//    //  // C   = (-1)  | i k l | = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
//    //  //  22         | m o p |
//    //  //
//    //  //           5 | a b d |
//    //  // C   = (-1)  | i j l | = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
//    //  //  23         | m n p |
//    //  //
//    //  //           6 | a b c |
//    //  // C   = (-1)  | i j k | = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
//    //  //  24         | m n o |
//    //  //
//    //  // Third Row
//    //  //           4 | b c d |
//    //  // C   = (-1)  | f g h | = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
//    //  //  31         | n o p |
//    //  //
//    //  //           5 | a c d |
//    //  // C   = (-1)  | e g h | = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
//    //  //  32         | m o p |
//    //  //
//    //  //           6 | a b d |
//    //  // C   = (-1)  | e f h | = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
//    //  //  33         | m n p |
//    //  //
//    //  //           7 | a b c |
//    //  // C   = (-1)  | e f g | = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
//    //  //  34         | m n o |
//    //  //
//    //  // Fourth Row
//    //  //           5 | b c d |
//    //  // C   = (-1)  | f g h | = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
//    //  //  41         | j k l |
//    //  //
//    //  //           6 | a c d |
//    //  // C   = (-1)  | e g h | = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
//    //  //  42         | i k l |
//    //  //
//    //  //           7 | a b d |
//    //  // C   = (-1)  | e f h | = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
//    //  //  43         | i j l |
//    //  //
//    //  //           8 | a b c |
//    //  // C   = (-1)  | e f g | = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
//    //  //  44         | i j k |
//    //  //
//    //  // Cost of operation
//    //  // 53 adds, 104 muls, and 1 div.
//    //  double a = matrix.m_11, b = matrix.m_12, c = matrix.m_13, d = matrix.m_14;
//    //  double e = matrix.m_21, f = matrix.m_22, g = matrix.m_23, h = matrix.m_24;
//    //  double i = matrix.m_31, j = matrix.m_32, k = matrix.m_33, l = matrix.m_34;
//    //  double m = matrix.m_41, n = matrix.m_42, o = matrix.m_43, p = matrix.m_44;

//    //  var kp_lo = k * p - l * o;
//    //  var jp_ln = j * p - l * n;
//    //  var jo_kn = j * o - k * n;
//    //  var ip_lm = i * p - l * m;
//    //  var io_km = i * o - k * m;
//    //  var in_jm = i * n - j * m;

//    //  var a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
//    //  var a12 = -(e * kp_lo - g * ip_lm + h * io_km);
//    //  var a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
//    //  var a14 = -(e * jo_kn - f * io_km + g * in_jm);

//    //  var det = a * a11 + b * a12 + c * a13 + d * a14;

//    //  if (System.Math.Abs(det) < double.Epsilon)
//    //  {
//    //    result = new Matrix4(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);

//    //    return false;
//    //  }

//    //  result = new Matrix4();

//    //  double invDet = 1 / det;

//    //  result.m_11 = a11 * invDet;
//    //  result.m_21 = a12 * invDet;
//    //  result.m_31 = a13 * invDet;
//    //  result.m_41 = a14 * invDet;

//    //  result.m_12 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
//    //  result.m_22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
//    //  result.m_32 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
//    //  result.m_42 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

//    //  var gp_ho = g * p - h * o;
//    //  var fp_hn = f * p - h * n;
//    //  var fo_gn = f * o - g * n;
//    //  var ep_hm = e * p - h * m;
//    //  var eo_gm = e * o - g * m;
//    //  var en_fm = e * n - f * m;

//    //  result.m_13 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
//    //  result.m_23 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
//    //  result.m_33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
//    //  result.m_43 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

//    //  var gl_hk = g * l - h * k;
//    //  var fl_hj = f * l - h * j;
//    //  var fk_gj = f * k - g * j;
//    //  var el_hi = e * l - h * i;
//    //  var ek_gi = e * k - g * i;
//    //  var ej_fi = e * j - f * i;

//    //  result.m_14 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
//    //  result.m_24 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
//    //  result.m_34 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
//    //  result.m_44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

//    //  return true;
//    //}

//    public static double[,] ToArray(this Geometry.IMatrix4 source)
//      => new double[,]
//      {
//        { source.M11, source.M12, source.M13, source.M14 },
//        { source.M21, source.M22, source.M23, source.M24 },
//        { source.M31, source.M32, source.M33, source.M34 },
//        { source.M41, source.M42, source.M43, source.M44 }
//      };

//    public static Geometry.EulerAngles ToEulerAnglesTaitBryanZYX(this Geometry.IMatrix4 source)
//      => new(
//        System.Math.Atan2(source.M11, source.M21),
//        System.Math.Atan2(System.Math.Sqrt(1 - source.M31 * source.M31), -source.M31),
//        System.Math.Atan2(source.M33, source.M32)
//      );

//    public static Geometry.EulerAngles ToEulerAnglesProperEulerZXZ(this Geometry.IMatrix4 source)
//      => new(
//        System.Math.Atan2(-source.M23, source.M13),
//        System.Math.Atan2(source.M33, System.Math.Sqrt(1 - source.M33 * source.M33)),
//        System.Math.Atan2(source.M32, source.M31)
//      );

//    public static System.Numerics.Matrix4x4 ToMatrix4x4(this Geometry.IMatrix4 source)
//      => new(
//        (float)source.M11, (float)source.M12, (float)source.M13, (float)source.M14,
//        (float)source.M21, (float)source.M22, (float)source.M23, (float)source.M24,
//        (float)source.M31, (float)source.M32, (float)source.M33, (float)source.M34,
//        (float)source.M41, (float)source.M42, (float)source.M43, (float)source.M44
//      );

//    public static Geometry.CartesianCoordinate3<double> Transform(this Geometry.IMatrix4 source, Geometry.ICartesianCoordinate3<double> vector)
//      => new(
//        vector.X * source.M11 + vector.Y * source.M12 + vector.Z * source.M13,
//        vector.X * source.M21 + vector.Y * source.M22 + vector.Z * source.M23,
//        vector.X * source.M31 + vector.Y * source.M32 + vector.Z * source.M33
//      );

//    /// <summary>Transforms the given matrix by applying the given Quaternion rotation.</summary>
//    /// <param name="source">The source matrix to transform.</param>
//    /// <param name="rotation">The rotation to apply.</param>
//    public static Geometry.Matrix4 Transform(this Geometry.IMatrix4 source, Geometry.IQuaternion rotation)
//    {
//      // Compute rotation matrix.
//      var x2 = rotation.X + rotation.X;
//      var y2 = rotation.Y + rotation.Y;
//      var z2 = rotation.Z + rotation.Z;

//      var wx2 = rotation.W * x2;
//      var wy2 = rotation.W * y2;
//      var wz2 = rotation.W * z2;
//      var xx2 = rotation.X * x2;
//      var xy2 = rotation.X * y2;
//      var xz2 = rotation.X * z2;
//      var yy2 = rotation.Y * y2;
//      var yz2 = rotation.Y * z2;
//      var zz2 = rotation.Z * z2;

//      var q11 = 1 - yy2 - zz2;
//      var q21 = xy2 - wz2;
//      var q31 = xz2 + wy2;

//      var q12 = xy2 + wz2;
//      var q22 = 1 - xx2 - zz2;
//      var q32 = yz2 - wx2;

//      var q13 = xz2 - wy2;
//      var q23 = yz2 + wx2;
//      var q33 = 1 - xx2 - yy2;

//      return new
//      (
//        // First row
//        source.M11 * q11 + source.M12 * q21 + source.M13 * q31,
//        source.M11 * q12 + source.M12 * q22 + source.M13 * q32,
//        source.M11 * q13 + source.M12 * q23 + source.M13 * q33,
//        source.M14,
//        // Second row
//        source.M21 * q11 + source.M22 * q21 + source.M23 * q31,
//        source.M21 * q12 + source.M22 * q22 + source.M23 * q32,
//        source.M21 * q13 + source.M22 * q23 + source.M23 * q33,
//        source.M24,
//        // Third row
//        source.M31 * q11 + source.M32 * q21 + source.M33 * q31,
//        source.M31 * q12 + source.M32 * q22 + source.M33 * q32,
//        source.M31 * q13 + source.M32 * q23 + source.M33 * q33,
//        source.M34,
//        // Fourth row
//        source.M41 * q11 + source.M42 * q21 + source.M43 * q31,
//        source.M41 * q12 + source.M42 * q22 + source.M43 * q32,
//        source.M41 * q13 + source.M42 * q23 + source.M43 * q33,
//        source.M44
//      );
//    }

//    /// <summary>Creates a new matrix with the rows and columns transposed.</summary>
//    public static Geometry.Matrix4 Transpose(this Geometry.IMatrix4 source)
//      => new(source.M11, source.M21, source.M31, source.M41, source.M12, source.M22, source.M32, source.M42, source.M13, source.M23, source.M33, source.M43, source.M14, source.M24, source.M34, source.M44);

//    /// <summary>Attempts to calculate the inverse of the given matrix. If successful, result will contain the inverted matrix.</summary>
//    /// <param name="matrix">The source matrix to invert.</param>
//    /// <param name="result">If successful, contains the inverted matrix.</param>
//    /// <returns>True if the source matrix could be inverted; False otherwise.</returns>
//    public static bool TryGetInverseOptimized(this Geometry.IMatrix4 source, out Geometry.Matrix4 result)
//    {
//      //                                       -1
//      // If you have matrix M, inverse Matrix M   can compute
//      //
//      //     -1       1      
//      //    M   = --------- A
//      //            det(M)
//      //
//      // A is adjugate (adjoint) of M, where,
//      //
//      //      T
//      // A = C
//      //
//      // C is Cofactor matrix of M, where,
//      //           i + j
//      // C   = (-1)      * det(M  )
//      //  ij                    ij
//      //
//      //     [ a b c d ]
//      // M = [ e f g h ]
//      //     [ i j k l ]
//      //     [ m n o p ]
//      //
//      // First Row
//      //           2 | f g h |
//      // C   = (-1)  | j k l | = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
//      //  11         | n o p |
//      //
//      //           3 | e g h |
//      // C   = (-1)  | i k l | = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
//      //  12         | m o p |
//      //
//      //           4 | e f h |
//      // C   = (-1)  | i j l | = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
//      //  13         | m n p |
//      //
//      //           5 | e f g |
//      // C   = (-1)  | i j k | = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
//      //  14         | m n o |
//      //
//      // Second Row
//      //           3 | b c d |
//      // C   = (-1)  | j k l | = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
//      //  21         | n o p |
//      //
//      //           4 | a c d |
//      // C   = (-1)  | i k l | = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
//      //  22         | m o p |
//      //
//      //           5 | a b d |
//      // C   = (-1)  | i j l | = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
//      //  23         | m n p |
//      //
//      //           6 | a b c |
//      // C   = (-1)  | i j k | = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
//      //  24         | m n o |
//      //
//      // Third Row
//      //           4 | b c d |
//      // C   = (-1)  | f g h | = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
//      //  31         | n o p |
//      //
//      //           5 | a c d |
//      // C   = (-1)  | e g h | = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
//      //  32         | m o p |
//      //
//      //           6 | a b d |
//      // C   = (-1)  | e f h | = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
//      //  33         | m n p |
//      //
//      //           7 | a b c |
//      // C   = (-1)  | e f g | = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
//      //  34         | m n o |
//      //
//      // Fourth Row
//      //           5 | b c d |
//      // C   = (-1)  | f g h | = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
//      //  41         | j k l |
//      //
//      //           6 | a c d |
//      // C   = (-1)  | e g h | = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
//      //  42         | i k l |
//      //
//      //           7 | a b d |
//      // C   = (-1)  | e f h | = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
//      //  43         | i j l |
//      //
//      //           8 | a b c |
//      // C   = (-1)  | e f g | = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
//      //  44         | i j k |
//      //
//      // Cost of operation
//      // 53 adds, 104 muls, and 1 div.
//      double a = source.M11, b = source.M12, c = source.M13, d = source.M14;
//      double e = source.M21, f = source.M22, g = source.M23, h = source.M24;
//      double i = source.M31, j = source.M32, k = source.M33, l = source.M34;
//      double m = source.M41, n = source.M42, o = source.M43, p = source.M44;

//      var kp_lo = k * p - l * o;
//      var jp_ln = j * p - l * n;
//      var jo_kn = j * o - k * n;
//      var ip_lm = i * p - l * m;
//      var io_km = i * o - k * m;
//      var in_jm = i * n - j * m;

//      var a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
//      var a12 = -(e * kp_lo - g * ip_lm + h * io_km);
//      var a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
//      var a14 = -(e * jo_kn - f * io_km + g * in_jm);

//      var det = a * a11 + b * a12 + c * a13 + d * a14;

//      if (double.Abs(det) < double.Epsilon)
//      {
//        result = new(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);

//        return false;
//      }

//      var invDet = 1 / det;

//      var m11 = a11 * invDet;
//      var m21 = a12 * invDet;
//      var m31 = a13 * invDet;
//      var m41 = a14 * invDet;

//      var m12 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
//      var m22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
//      var m32 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
//      var m42 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

//      var gp_ho = g * p - h * o;
//      var fp_hn = f * p - h * n;
//      var fo_gn = f * o - g * n;
//      var ep_hm = e * p - h * m;
//      var eo_gm = e * o - g * m;
//      var en_fm = e * n - f * m;

//      var m13 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
//      var m23 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
//      var m33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
//      var m43 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

//      var gl_hk = g * l - h * k;
//      var fl_hj = f * l - h * j;
//      var fk_gj = f * k - g * j;
//      var el_hi = e * l - h * i;
//      var ek_gi = e * k - g * i;
//      var ej_fi = e * j - f * i;

//      var m14 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
//      var m24 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
//      var m34 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
//      var m44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

//      result = new(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

//      return true;
//    }
//  }
//  #endregion ExtensionMethods

//  namespace Geometry
//  {
//    public interface IMatrix4
//    {
//      /// <summary>Value at row 1, column 1 of the matrix.</summary>
//      public double M11 { get; }
//      /// <summary>Value at row 1, column 2 of the matrix.</summary>
//      public double M12 { get; }
//      /// <summary>Value at row 1, column 3 of the matrix.</summary>
//      public double M13 { get; }
//      /// <summary>Value at row 1, column 4 of the matrix.</summary>
//      public double M14 { get; }
//      /// <summary>Value at row 2, column 1 of the matrix.</summary>
//      public double M21 { get; }
//      /// <summary>Value at row 2, column 2 of the matrix.</summary>
//      public double M22 { get; }
//      /// <summary>Value at row 2, column 3 of the matrix.</summary>
//      public double M23 { get; }
//      /// <summary>Value at row 2, column 4 of the matrix.</summary>
//      public double M24 { get; }
//      /// <summary>Value at row 3, column 1 of the matrix.</summary>
//      public double M31 { get; }
//      /// <summary>Value at row 3, column 2 of the matrix.</summary>
//      public double M32 { get; }
//      /// <summary>Value at row 3, column 3 of the matrix.</summary>
//      public double M33 { get; }
//      /// <summary>Value at row 3, column 4 of the matrix.</summary>
//      public double M34 { get; }
//      /// <summary>Value at row 4, column 1 of the matrix.</summary>
//      public double M41 { get; }
//      /// <summary>Value at row 4, column 2 of the matrix.</summary>
//      public double M42 { get; }
//      /// <summary>Value at row 4, column 3 of the matrix.</summary>
//      public double M43 { get; }
//      /// <summary>Value at row 4, column 4 of the matrix.</summary>
//      public double M44 { get; }
//    }
//  }
//}
//#endif
