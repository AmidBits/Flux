//namespace Flux.Numerics
//{
//  /// <summary>
//  /// 
//  /// </summary>
//  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Matrix4x4.cs"/>
//  public struct Matrix4x4
//    : System.IEquatable<Matrix4x4>
//  {
//    /// <summary>Returns an empty matrix.</summary>
//    public static Matrix4x4 Empty
//      => new Matrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

//    /// <summary>Returns the multiplicative identity matrix.</summary>
//    public static Matrix4x4 Identity
//      => new Matrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

//    /// <summary>Value at row 1, column 1 of the matrix.</summary>
//    public double M11 { get; set; }
//    /// <summary>Value at row 1, column 2 of the matrix.</summary>
//    public double M12 { get; set; }
//    /// <summary>Value at row 1, column 3 of the matrix.</summary>
//    public double M13 { get; set; }
//    /// <summary>Value at row 1, column 4 of the matrix.</summary>
//    public double M14 { get; set; }

//    /// <summary>Value at row 2, column 1 of the matrix.</summary>
//    public double M21 { get; set; }
//    /// <summary>Value at row 2, column 2 of the matrix.</summary>
//    public double M22 { get; set; }
//    /// <summary>Value at row 2, column 3 of the matrix.</summary>
//    public double M23 { get; set; }
//    /// <summary>Value at row 2, column 4 of the matrix.</summary>
//    public double M24 { get; set; }

//    /// <summary>Value at row 3, column 1 of the matrix.</summary>
//    public double M31 { get; set; }
//    /// <summary>Value at row 3, column 2 of the matrix.</summary>
//    public double M32 { get; set; }
//    /// <summary>Value at row 3, column 3 of the matrix.</summary>
//    public double M33 { get; set; }
//    /// <summary>Value at row 3, column 4 of the matrix.</summary>
//    public double M34 { get; set; }

//    /// <summary>Value at row 4, column 1 of the matrix.</summary>
//    public double M41 { get; set; }
//    /// <summary>Value at row 4, column 2 of the matrix.</summary>
//    public double M42 { get; set; }
//    /// <summary>Value at row 4, column 3 of the matrix.</summary>
//    public double M43 { get; set; }
//    /// <summary>Value at row 4, column 4 of the matrix.</summary>
//    public double M44 { get; set; }

//    /// <summary>
//    /// Constructs a Matrix4x4 from the given components.
//    /// </summary>
//    public Matrix4x4(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
//    {
//      M11 = m11;
//      M12 = m12;
//      M13 = m13;
//      M14 = m14;

//      M21 = m21;
//      M22 = m22;
//      M23 = m23;
//      M24 = m24;

//      M31 = m31;
//      M32 = m32;
//      M33 = m33;
//      M34 = m34;

//      M41 = m41;
//      M42 = m42;
//      M43 = m43;
//      M44 = m44;
//    }

//    /// <summary>Returns whether the matrix is empty.</summary>
//    public bool IsEmpty
//      => Equals(Empty);

//    /// <summary>Returns whether the matrix is the identity matrix.</summary>
//    public bool IsIdentity
//      => Equals(Identity);

//    /// <summary>Calculates the determinant of the matrix.</summary>
//    public double GetDeterminant()
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

//      double a = M11, b = M12, c = M13, d = M14;
//      double e = M21, f = M22, g = M23, h = M24;
//      double i = M31, j = M32, k = M33, l = M34;
//      double m = M41, n = M42, o = M43, p = M44;

//      double kp_lo = k * p - l * o;
//      double jp_ln = j * p - l * n;
//      double jo_kn = j * o - k * n;
//      double ip_lm = i * p - l * m;
//      double io_km = i * o - k * m;
//      double in_jm = i * n - j * m;

//      return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
//             b * (e * kp_lo - g * ip_lm + h * io_km) +
//             c * (e * jp_ln - f * ip_lm + h * in_jm) -
//             d * (e * jo_kn - f * io_km + g * in_jm);
//    }

//    /// <summary>Gets or sets the translation component of this matrix.</summary>
//    public Vector4 Translation
//    {
//      get => new Vector4(M41, M42, M43, 1);
//      set
//      {
//        M41 = value.X;
//        M42 = value.Y;
//        M43 = value.Z;
//      }
//    }

//    #region Static methods
//    /// <summary>Creates a spherical billboard that rotates around a specified object position.</summary>
//    /// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
//    /// <param name="cameraPosition">Position of the camera.</param>
//    /// <param name="cameraUpVector">The up vector of the camera.</param>
//    /// <param name="cameraForwardVector">The forward vector of the camera.</param>
//    /// <returns>The created billboard matrix</returns>
//    public static Matrix4x4 CreateBillboard(Vector4 objectPosition, Vector4 cameraPosition, Vector4 cameraUpVector, Vector4 cameraForwardVector)
//    {
//      const double epsilon = 1e-4f;

//      var zaxis = new Vector4(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);

//      zaxis = zaxis.LengthSquared() is var znorm && znorm < epsilon ? -cameraForwardVector : Vector4.Multiply(zaxis, 1 / System.Math.Sqrt(znorm));

//      var xaxis = Vector4.Normalize(Vector4.Cross(cameraUpVector, zaxis));
//      var yaxis = Vector4.Cross(zaxis, xaxis);

//      return new Matrix4x4(xaxis.X, xaxis.Y, xaxis.Z, 0, yaxis.X, yaxis.Y, yaxis.Z, 0, zaxis.X, zaxis.Y, zaxis.Z, 0, objectPosition.X, objectPosition.Y, objectPosition.Z, 1);
//    }
//    /// <summary>
//    /// Creates a cylindrical billboard that rotates around a specified axis.
//    /// </summary>
//    /// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
//    /// <param name="cameraPosition">Position of the camera.</param>
//    /// <param name="rotateAxis">Axis to rotate the billboard around.</param>
//    /// <param name="cameraForwardVector">Forward vector of the camera.</param>
//    /// <param name="objectForwardVector">Forward vector of the object.</param>
//    /// <returns>The created billboard matrix.</returns>
//    public static Matrix4x4 CreateConstrainedBillboard(Vector4 objectPosition, Vector4 cameraPosition, Vector4 rotateAxis, Vector4 cameraForwardVector, Vector4 objectForwardVector)
//    {
//      const double epsilon = 1e-4f;
//      const double minAngle = 1 - (0.1f * Maths.PiOver180); // 0.1 degrees

//      // Treat the case when object and camera positions are too close.
//      var faceDir = new Vector4(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);

//      faceDir = faceDir.LengthSquared() is var norm && norm < epsilon ? -cameraForwardVector : Vector4.Multiply(faceDir, 1 / System.Math.Sqrt(norm));

//      Vector4 yaxis = rotateAxis;
//      Vector4 xaxis;
//      Vector4 zaxis;

//      // Treat the case when angle between faceDir and rotateAxis is too close to 0.
//      var dot = Vector4.Dot(rotateAxis, faceDir);

//      if (System.Math.Abs(dot) > minAngle)
//      {
//        zaxis = objectForwardVector;

//        // Make sure passed values are useful for compute.
//        dot = Vector4.Dot(rotateAxis, zaxis);

//        if (System.Math.Abs(dot) > minAngle)
//        {
//          zaxis = (System.Math.Abs(rotateAxis.Z) > minAngle) ? new Vector4(1, 0, 0) : new Vector4(0, 0, -1);
//        }

//        xaxis = Vector4.Normalize(Vector4.Cross(rotateAxis, zaxis));
//        zaxis = Vector4.Normalize(Vector4.Cross(xaxis, rotateAxis));
//      }
//      else
//      {
//        xaxis = Vector4.Normalize(Vector4.Cross(rotateAxis, faceDir));
//        zaxis = Vector4.Normalize(Vector4.Cross(xaxis, yaxis));
//      }

//      return new Matrix4x4(xaxis.X, xaxis.Y, xaxis.Z, 0, yaxis.X, yaxis.Y, yaxis.Z, 0, zaxis.X, zaxis.Y, zaxis.Z, 0, objectPosition.X, objectPosition.Y, objectPosition.Z, 1);
//    }
//    /// <summary>
//    /// Creates a matrix that rotates around an arbitrary vector.
//    /// </summary>
//    /// <param name="axis">The axis to rotate around.</param>
//    /// <param name="angle">The angle to rotate around the given axis, in radians.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4x4 CreateFromAxisAngle(Vector4 axis, double angle)
//    {
//      // a: angle
//      // x, y, z: unit vector for axis.
//      //
//      // Rotation matrix M can compute by using below equation.
//      //
//      //        T               T
//      //  M = uu + (cos a)( I-uu ) + (sin a)S
//      //
//      // Where:
//      //
//      //  u = ( x, y, z )
//      //
//      //      [  0 -z  y ]
//      //  S = [  z  0 -x ]
//      //      [ -y  x  0 ]
//      //
//      //      [ 1 0 0 ]
//      //  I = [ 0 1 0 ]
//      //      [ 0 0 1 ]
//      //
//      //
//      //     [  xx+cosa*(1-xx)   yx-cosa*yx-sina*z zx-cosa*xz+sina*y ]
//      // M = [ xy-cosa*yx+sina*z    yy+cosa(1-yy)  yz-cosa*yz-sina*x ]
//      //     [ zx-cosa*zx-sina*y zy-cosa*zy+sina*x   zz+cosa*(1-zz)  ]
//      //
//      double x = axis.X, y = axis.Y, z = axis.Z;
//      double sa = System.Math.Sin(angle), ca = System.Math.Cos(angle);
//      double xx = x * x, yy = y * y, zz = z * z;
//      double xy = x * y, xz = x * z, yz = y * z;

//      return new Matrix4x4(xx + ca * (1 - xx), xy - ca * xy + sa * z, xz - ca * xz - sa * y, 0, xy - ca * xy - sa * z, yy + ca * (1 - yy), yz - ca * yz + sa * x, 0, xz - ca * xz + sa * y, yz - ca * yz - sa * x, zz + ca * (1 - zz), 0, 0, 0, 0, 1);
//    }
//    /// <summary>
//    /// Creates a rotation matrix from the given Quaternion rotation value.
//    /// </summary>
//    /// <param name="quaternion">The source Quaternion.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4x4 CreateFromQuaternion(Quaternion quaternion)
//    {
//      var xx = quaternion.X * quaternion.X;
//      var yy = quaternion.Y * quaternion.Y;
//      var zz = quaternion.Z * quaternion.Z;

//      var xy = quaternion.X * quaternion.Y;
//      var wz = quaternion.Z * quaternion.W;
//      var xz = quaternion.Z * quaternion.X;
//      var wy = quaternion.Y * quaternion.W;
//      var yz = quaternion.Y * quaternion.Z;
//      var wx = quaternion.X * quaternion.W;

//      return new Matrix4x4(1 - 2 * (yy + zz), 2 * (xy + wz), 2 * (xz - wy), 0, 2 * (xy - wz), 1 - 2 * (zz + xx), 2 * (yz + wx), 0, 2 * (xz + wy), 2 * (yz - wx), 1 - 2 * (yy + xx), 0, 0, 0, 0, 1);
//    }
//    /// <summary>
//    /// Creates a rotation matrix from the specified yaw, pitch, and roll.
//    /// </summary>
//    /// <param name="yaw">Angle of rotation, in radians, around the Y-axis.</param>
//    /// <param name="pitch">Angle of rotation, in radians, around the X-axis.</param>
//    /// <param name="roll">Angle of rotation, in radians, around the Z-axis.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4x4 CreateFromYawPitchRoll(double yaw, double pitch, double roll)
//      => CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));
//    /// <summary>
//    /// Creates a view matrix.
//    /// </summary>
//    /// <param name="cameraPosition">The position of the camera.</param>
//    /// <param name="cameraTarget">The target towards which the camera is pointing.</param>
//    /// <param name="cameraUpVector">The direction that is "up" from the camera's point of view.</param>
//    /// <returns>The view matrix.</returns>
//    public static Matrix4x4 CreateLookAt(Vector4 cameraPosition, Vector4 cameraTarget, Vector4 cameraUpVector)
//    {
//      Vector4 zaxis = Vector4.Normalize(cameraPosition - cameraTarget);
//      Vector4 xaxis = Vector4.Normalize(Vector4.Cross(cameraUpVector, zaxis));
//      Vector4 yaxis = Vector4.Cross(zaxis, xaxis);

//      return new Matrix4x4(xaxis.X, yaxis.X, zaxis.X, 0, xaxis.Y, yaxis.Y, zaxis.Y, 0, xaxis.Z, yaxis.Z, zaxis.Z, 0, -Vector4.Dot(xaxis, cameraPosition), -Vector4.Dot(yaxis, cameraPosition), -Vector4.Dot(zaxis, cameraPosition), 1);
//    }
//    /// <summary>
//    /// Creates an orthographic perspective matrix from the given view volume dimensions.
//    /// </summary>
//    /// <param name="width">Width of the view volume.</param>
//    /// <param name="height">Height of the view volume.</param>
//    /// <param name="zNearPlane">Minimum Z-value of the view volume.</param>
//    /// <param name="zFarPlane">Maximum Z-value of the view volume.</param>
//    /// <returns>The orthographic projection matrix.</returns>
//    public static Matrix4x4 CreateOrthographic(double width, double height, double zNearPlane, double zFarPlane)
//      => new Matrix4x4(2 / width, 0, 0, 0, 0, 2 / height, 0, 0, 0, 0, 1 / (zNearPlane - zFarPlane), 0, 0, 0, zNearPlane / (zNearPlane - zFarPlane), 1);
//    /// <summary>
//    /// Builds a customized, orthographic projection matrix.
//    /// </summary>
//    /// <param name="left">Minimum X-value of the view volume.</param>
//    /// <param name="right">Maximum X-value of the view volume.</param>
//    /// <param name="bottom">Minimum Y-value of the view volume.</param>
//    /// <param name="top">Maximum Y-value of the view volume.</param>
//    /// <param name="zNearPlane">Minimum Z-value of the view volume.</param>
//    /// <param name="zFarPlane">Maximum Z-value of the view volume.</param>
//    /// <returns>The orthographic projection matrix.</returns>
//    public static Matrix4x4 CreateOrthographicOffCenter(double left, double right, double bottom, double top, double zNearPlane, double zFarPlane)
//      => new Matrix4x4(2 / (right - left), 0, 0, 0, 0, 2 / (top - bottom), 0, 0, 0, 0, 1 / (zNearPlane - zFarPlane), 0, (left + right) / (left - right), (top + bottom) / (bottom - top), zNearPlane / (zNearPlane - zFarPlane), 1);
//    /// <summary>
//    /// Creates a perspective projection matrix based on a field of view, aspect ratio, and near and far view plane distances. 
//    /// </summary>
//    /// <param name="fieldOfView">Field of view in the y direction, in radians.</param>
//    /// <param name="aspectRatio">Aspect ratio, defined as view space width divided by height.</param>
//    /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
//    /// <param name="farPlaneDistance">Distance to the far view plane.</param>
//    /// <returns>The perspective projection matrix.</returns>
//    public static Matrix4x4 CreatePerspectiveFieldOfView(double fieldOfView, double aspectRatio, double nearPlaneDistance, double farPlaneDistance)
//    {
//      if (fieldOfView <= 0 || fieldOfView >= System.Math.PI) throw new System.ArgumentOutOfRangeException(nameof(fieldOfView));
//      if (nearPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));
//      if (farPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(farPlaneDistance));
//      if (nearPlaneDistance >= farPlaneDistance) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));

//      var yScale = 1 / System.Math.Tan(fieldOfView * 0.5);
//      var xScale = yScale / aspectRatio;

//      return new Matrix4x4(xScale, 0, 0, 0, 0, yScale, 0, 0, 0, 0, farPlaneDistance / (nearPlaneDistance - farPlaneDistance), -1, 0, 0, nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance), 0);
//    }
//    /// <summary>
//    /// Creates a perspective projection matrix from the given view volume dimensions.
//    /// </summary>
//    /// <param name="width">Width of the view volume at the near view plane.</param>
//    /// <param name="height">Height of the view volume at the near view plane.</param>
//    /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
//    /// <param name="farPlaneDistance">Distance to the far view plane.</param>
//    /// <returns>The perspective projection matrix.</returns>
//    public static Matrix4x4 CreatePerspective(double width, double height, double nearPlaneDistance, double farPlaneDistance)
//    {
//      if (nearPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));
//      if (farPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(farPlaneDistance));
//      if (nearPlaneDistance >= farPlaneDistance) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));

//      return new Matrix4x4(2 * nearPlaneDistance / width, 0, 0, 0, 0, 2 * nearPlaneDistance / height, 0, 0, 0, 0, farPlaneDistance / (nearPlaneDistance - farPlaneDistance), -1, 0, 0, nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance), 0);
//    }
//    /// <summary>
//    /// Creates a customized, perspective projection matrix.
//    /// </summary>
//    /// <param name="left">Minimum x-value of the view volume at the near view plane.</param>
//    /// <param name="right">Maximum x-value of the view volume at the near view plane.</param>
//    /// <param name="bottom">Minimum y-value of the view volume at the near view plane.</param>
//    /// <param name="top">Maximum y-value of the view volume at the near view plane.</param>
//    /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
//    /// <param name="farPlaneDistance">Distance to of the far view plane.</param>
//    /// <returns>The perspective projection matrix.</returns>
//    public static Matrix4x4 CreatePerspectiveOffCenter(double left, double right, double bottom, double top, double nearPlaneDistance, double farPlaneDistance)
//    {
//      if (nearPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));
//      if (farPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(farPlaneDistance));
//      if (nearPlaneDistance >= farPlaneDistance) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));

//      return new Matrix4x4(2 * nearPlaneDistance / (right - left), 0, 0, 0, 0, 2 * nearPlaneDistance / (top - bottom), 0, 0, (left + right) / (right - left), (top + bottom) / (top - bottom), farPlaneDistance / (nearPlaneDistance - farPlaneDistance), -1, 0, 0, nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance), 0);
//    }
//    /// <summary>
//    /// Creates a Matrix that reflects the coordinate system about a specified Plane.
//    /// </summary>
//    /// <param name="value">The Plane about which to create a reflection.</param>
//    /// <returns>A new matrix expressing the reflection.</returns>
//    public static Matrix4x4 CreateReflection(Plane value)
//    {
//      value = Plane.Normalize(value);

//      var a = value.Normal.X;
//      var b = value.Normal.Y;
//      var c = value.Normal.Z;

//      var fa = -2 * a;
//      var fb = -2 * b;
//      var fc = -2 * c;

//      return new Matrix4x4(fa * a + 1, fb * a, fc * a, 0, fa * b, fb * b + 1, fc * b, 0, fa * c, fb * c, fc * c + 1, 0, fa * value.D, fb * value.D, fc * value.D, 1);
//    }
//    /// <summary>
//    /// Creates a matrix for rotating points around the X-axis.
//    /// </summary>
//    /// <param name="radians">The amount, in radians, by which to rotate around the X-axis.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4x4 CreateRotationX(double radians)
//    {
//      // [  1  0  0  0 ]
//      // [  0  c  s  0 ]
//      // [  0 -s  c  0 ]
//      // [  0  0  0  1 ]

//      var c = System.Math.Cos(radians);
//      var s = System.Math.Sin(radians);

//      return new Matrix4x4(1, 0, 0, 0, 0, c, s, 0, 0, -s, c, 0, 0, 0, 0, 1);
//    }
//    /// <summary>
//    /// Creates a matrix for rotating points around the X-axis, from a center point.
//    /// </summary>
//    /// <param name="radians">The amount, in radians, by which to rotate around the X-axis.</param>
//    /// <param name="centerPoint">The center point.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4x4 CreateRotationX(double radians, Vector4 centerPoint)
//    {
//      // [  1  0  0  0 ]
//      // [  0  c  s  0 ]
//      // [  0 -s  c  0 ]
//      // [  0  y  z  1 ]

//      var c = System.Math.Cos(radians);
//      var s = System.Math.Sin(radians);

//      var y = centerPoint.Y * (1 - c) + centerPoint.Z * s;
//      var z = centerPoint.Z * (1 - c) - centerPoint.Y * s;

//      return new Matrix4x4(1, 0, 0, 0, 0, c, s, 0, 0, -s, c, 0, 0, y, z, 1);
//    }
//    /// <summary>
//    /// Creates a matrix for rotating points around the Y-axis.
//    /// </summary>
//    /// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4x4 CreateRotationY(double radians)
//    {
//      // [  c  0 -s  0 ]
//      // [  0  1  0  0 ]
//      // [  s  0  c  0 ]
//      // [  0  0  0  1 ]

//      var c = System.Math.Cos(radians);
//      var s = System.Math.Sin(radians);

//      return new Matrix4x4(c, 0, -s, 0, 0, 1, 0, 0, s, 0, c, 0, 0, 0, 0, 1);
//    }
//    /// <summary>
//    /// Creates a matrix for rotating points around the Y-axis, from a center point.
//    /// </summary>
//    /// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
//    /// <param name="centerPoint">The center point.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4x4 CreateRotationY(double radians, Vector4 centerPoint)
//    {
//      // [  c  0 -s  0 ]
//      // [  0  1  0  0 ]
//      // [  s  0  c  0 ]
//      // [  x  0  z  1 ]

//      var c = System.Math.Cos(radians);
//      var s = System.Math.Sin(radians);

//      var x = centerPoint.X * (1 - c) - centerPoint.Z * s;
//      var z = centerPoint.Z * (1 - c) + centerPoint.X * s;

//      return new Matrix4x4(c, 0, -s, 0, 0, 1, 0, 0, s, 0, c, 0, x, 0, z, 1);
//    }
//    /// <summary>
//    /// Creates a matrix for rotating points around the Z-axis.
//    /// </summary>
//    /// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4x4 CreateRotationZ(double radians)
//    {
//      // [  c  s  0  0 ]
//      // [ -s  c  0  0 ]
//      // [  0  0  1  0 ]
//      // [  0  0  0  1 ]

//      var c = System.Math.Cos(radians);
//      var s = System.Math.Sin(radians);

//      return new Matrix4x4(c, s, 0, 0, -s, c, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
//    }
//    /// <summary>
//    /// Creates a matrix for rotating points around the Z-axis, from a center point.
//    /// </summary>
//    /// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
//    /// <param name="centerPoint">The center point.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4x4 CreateRotationZ(double radians, Vector4 centerPoint)
//    {
//      // [  c  s  0  0 ]
//      // [ -s  c  0  0 ]
//      // [  0  0  1  0 ]
//      // [  x  y  0  1 ]

//      var c = System.Math.Cos(radians);
//      var s = System.Math.Sin(radians);

//      var x = centerPoint.X * (1 - c) + centerPoint.Y * s;
//      var y = centerPoint.Y * (1 - c) - centerPoint.X * s;

//      return new Matrix4x4(c, s, 0, 0, -s, c, 0, 0, 0, 0, 1, 0, x, y, 0, 1);
//    }
//    /// <summary>
//    /// Creates a scaling matrix.
//    /// </summary>
//    /// <param name="xScale">Value to scale by on the X-axis.</param>
//    /// <param name="yScale">Value to scale by on the Y-axis.</param>
//    /// <param name="zScale">Value to scale by on the Z-axis.</param>
//    /// <returns>The scaling matrix.</returns>
//    public static Matrix4x4 CreateScale(double xScale, double yScale, double zScale)
//      => new Matrix4x4(xScale, 0, 0, 0, 0, yScale, 0, 0, 0, 0, zScale, 0, 0, 0, 0, 1);
//    /// <summary>
//    /// Creates a scaling matrix with a center point.
//    /// </summary>
//    /// <param name="xScale">Value to scale by on the X-axis.</param>
//    /// <param name="yScale">Value to scale by on the Y-axis.</param>
//    /// <param name="zScale">Value to scale by on the Z-axis.</param>
//    /// <param name="centerPoint">The center point.</param>
//    /// <returns>The scaling matrix.</returns>
//    public static Matrix4x4 CreateScale(double xScale, double yScale, double zScale, Vector4 centerPoint)
//    {
//      var tx = centerPoint.X * (1 - xScale);
//      var ty = centerPoint.Y * (1 - yScale);
//      var tz = centerPoint.Z * (1 - zScale);

//      return new Matrix4x4(xScale, 0, 0, 0, 0, yScale, 0, 0, 0, 0, zScale, 0, tx, ty, tz, 1);
//    }
//    /// <summary>Creates a scaling matrix.</summary>
//    /// <param name="scales">The vector containing the amount to scale by on each axis.</param>
//    /// <returns>The scaling matrix.</returns>
//    public static Matrix4x4 CreateScale(Vector4 scales)
//      => new Matrix4x4(scales.X, 0, 0, 0, 0, scales.Y, 0, 0, 0, 0, scales.Z, 0, 0, 0, 0, 1);
//    /// <summary>
//    /// Creates a scaling matrix with a center point.
//    /// </summary>
//    /// <param name="scales">The vector containing the amount to scale by on each axis.</param>
//    /// <param name="centerPoint">The center point.</param>
//    /// <returns>The scaling matrix.</returns>
//    public static Matrix4x4 CreateScale(Vector4 scales, Vector4 centerPoint)
//    {
//      var tx = centerPoint.X * (1 - scales.X);
//      var ty = centerPoint.Y * (1 - scales.Y);
//      var tz = centerPoint.Z * (1 - scales.Z);

//      return new Matrix4x4(scales.X, 0, 0, 0, 0, scales.Y, 0, 0, 0, 0, scales.Z, 0, tx, ty, tz, 1);
//    }
//    /// <summary>
//    /// Creates a uniform scaling matrix that scales equally on each axis.
//    /// </summary>
//    /// <param name="scale">The uniform scaling factor.</param>
//    /// <returns>The scaling matrix.</returns>
//    public static Matrix4x4 CreateScale(double scale)
//      => new Matrix4x4(scale, 0, 0, 0, 0, scale, 0, 0, 0, 0, scale, 0, 0, 0, 0, 1);
//    /// <summary>
//    /// Creates a uniform scaling matrix that scales equally on each axis with a center point.
//    /// </summary>
//    /// <param name="scale">The uniform scaling factor.</param>
//    /// <param name="centerPoint">The center point.</param>
//    /// <returns>The scaling matrix.</returns>
//    public static Matrix4x4 CreateScale(double scale, Vector4 centerPoint)
//    {
//      var tx = centerPoint.X * (1 - scale);
//      var ty = centerPoint.Y * (1 - scale);
//      var tz = centerPoint.Z * (1 - scale);

//      return new Matrix4x4(scale, 0, 0, 0, 0, scale, 0, 0, 0, 0, scale, 0, tx, ty, tz, 1);
//    }
//    /// <summary>
//    /// Creates a Matrix that flattens geometry into a specified Plane as if casting a shadow from a specified light source.
//    /// </summary>
//    /// <param name="lightDirection">The direction from which the light that will cast the shadow is coming.</param>
//    /// <param name="plane">The Plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
//    /// <returns>A new Matrix that can be used to flatten geometry onto the specified plane from the specified direction.</returns>
//    public static Matrix4x4 CreateShadow(Vector4 lightDirection, Plane plane)
//    {
//      Plane p = Plane.Normalize(plane);

//      double dot = p.Normal.X * lightDirection.X + p.Normal.Y * lightDirection.Y + p.Normal.Z * lightDirection.Z;
//      double a = -p.Normal.X;
//      double b = -p.Normal.Y;
//      double c = -p.Normal.Z;
//      double d = -p.D;

//      Matrix4x4 m = new Matrix4x4();

//      m.M11 = a * lightDirection.X + dot;
//      m.M21 = b * lightDirection.X;
//      m.M31 = c * lightDirection.X;
//      m.M41 = d * lightDirection.X;

//      m.M12 = a * lightDirection.Y;
//      m.M22 = b * lightDirection.Y + dot;
//      m.M32 = c * lightDirection.Y;
//      m.M42 = d * lightDirection.Y;

//      m.M13 = a * lightDirection.Z;
//      m.M23 = b * lightDirection.Z;
//      m.M33 = c * lightDirection.Z + dot;
//      m.M43 = d * lightDirection.Z;

//      m.M14 = 0;
//      m.M24 = 0;
//      m.M34 = 0;
//      m.M44 = dot;

//      return m;
//    }
//    /// <summary>
//    /// Creates a translation matrix.
//    /// </summary>
//    /// <param name="position">The amount to translate in each axis.</param>
//    /// <returns>The translation matrix.</returns>
//    public static Matrix4x4 CreateTranslation(Vector4 position)
//      => new Matrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, position.X, position.Y, position.Z, 1);
//    /// <summary>
//    /// Creates a translation matrix.
//    /// </summary>
//    /// <param name="xPosition">The amount to translate on the X-axis.</param>
//    /// <param name="yPosition">The amount to translate on the Y-axis.</param>
//    /// <param name="zPosition">The amount to translate on the Z-axis.</param>
//    /// <returns>The translation matrix.</returns>
//    public static Matrix4x4 CreateTranslation(double xPosition, double yPosition, double zPosition)
//      => new Matrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, xPosition, yPosition, zPosition, 1);
//    /// <summary>
//    /// Creates a world matrix with the specified parameters.
//    /// </summary>
//    /// <param name="position">The position of the object, used in translation operations.</param>
//    /// <param name="forward">Forward direction of the object.</param>
//    /// <param name="up">Upward direction of the object, usually [0, 1, 0].</param>
//    /// <returns>The world matrix.</returns>
//    public static Matrix4x4 CreateWorld(Vector4 position, Vector4 forward, Vector4 up)
//    {
//      var zaxis = Vector4.Normalize(-forward);
//      var xaxis = Vector4.Normalize(Vector4.Cross(up, zaxis));
//      var yaxis = Vector4.Cross(zaxis, xaxis);

//      return new Matrix4x4(xaxis.X, xaxis.Y, xaxis.Z, 0, yaxis.X, yaxis.Y, yaxis.Z, 0, zaxis.X, zaxis.Y, zaxis.Z, 0, position.X, position.Y, position.Z, 1);
//    }

//    //struct CanonicalBasis
//    //{
//    //  public Vector4 Row0;
//    //  public Vector4 Row1;
//    //  public Vector4 Row2;
//    //};

//    //[System.Security.SecuritySafeCritical]
//    //struct VectorBasis
//    //{
//    //  public unsafe Vector4* Element0;
//    //  public unsafe Vector4* Element1;
//    //  public unsafe Vector4* Element2;
//    //}

//    /// <summary>
//    /// Adds two matrices together.
//    /// </summary>
//    /// <param name="m1">The first source matrix.</param>
//    /// <param name="m2">The second source matrix.</param>
//    /// <returns>The resulting matrix.</returns>
//    public static Matrix4x4 Add(Matrix4x4 m1, Matrix4x4 m2)
//      => new Matrix4x4(m1.M11 + m2.M11, m1.M12 + m2.M12, m1.M13 + m2.M13, m1.M14 + m2.M14, m1.M21 + m2.M21, m1.M22 + m2.M22, m1.M23 + m2.M23, m1.M24 + m2.M24, m1.M31 + m2.M31, m1.M32 + m2.M32, m1.M33 + m2.M33, m1.M34 + m2.M34, m1.M41 + m2.M41, m1.M42 + m2.M42, m1.M43 + m2.M43, m1.M44 + m2.M44);
//    /// <summary>
//    /// Attempts to extract the scale, translation, and rotation components from the given scale/rotation/translation matrix.
//    /// If successful, the out parameters will contained the extracted values.
//    /// </summary>
//    /// <param name="matrix">The source matrix.</param>
//    /// <param name="scale">The scaling component of the transformation matrix.</param>
//    /// <param name="rotation">The rotation component of the transformation matrix.</param>
//    /// <param name="translation">The translation component of the transformation matrix</param>
//    /// <returns>True if the source matrix was successfully decomposed; False otherwise.</returns>
//    //[System.Security.SecuritySafeCritical]
//    //public static bool Decompose(Matrix4x4 matrix, out Vector4 scale, out Quaternion rotation, out Vector4 translation)
//    //{
//    //  bool result = true;

//    //  unsafe
//    //  {
//    //    fixed (Vector4* scaleBase = &scale)
//    //    {
//    //      double* pfScales = (double*)scaleBase;
//    //      const double EPSILON = 0.0001f;
//    //      double det;

//    //      VectorBasis vectorBasis;
//    //      Vector4** pVectorBasis = (Vector4**)&vectorBasis;

//    //      Matrix4x4 matTemp = Matrix4x4.Identity;
//    //      CanonicalBasis canonicalBasis = new CanonicalBasis();
//    //      Vector4* pCanonicalBasis = &canonicalBasis.Row0;

//    //      canonicalBasis.Row0 = new Vector4(1, 0, 0);
//    //      canonicalBasis.Row1 = new Vector4(0, 1, 0);
//    //      canonicalBasis.Row2 = new Vector4(0, 0, 1);

//    //      translation = new Vector4(
//    //          matrix.M41,
//    //          matrix.M42,
//    //          matrix.M43);

//    //      pVectorBasis[0] = (Vector4*)&matTemp.M11;
//    //      pVectorBasis[1] = (Vector4*)&matTemp.M21;
//    //      pVectorBasis[2] = (Vector4*)&matTemp.M31;

//    //      *(pVectorBasis[0]) = new Vector4(matrix.M11, matrix.M12, matrix.M13);
//    //      *(pVectorBasis[1]) = new Vector4(matrix.M21, matrix.M22, matrix.M23);
//    //      *(pVectorBasis[2]) = new Vector4(matrix.M31, matrix.M32, matrix.M33);

//    //      scale.X = pVectorBasis[0]->Length();
//    //      scale.Y = pVectorBasis[1]->Length();
//    //      scale.Z = pVectorBasis[2]->Length();

//    //      uint a, b, c;
//    //      #region Ranking
//    //      double x = pfScales[0], y = pfScales[1], z = pfScales[2];
//    //      if (x < y)
//    //      {
//    //        if (y < z)
//    //        {
//    //          a = 2;
//    //          b = 1;
//    //          c = 0;
//    //        }
//    //        else
//    //        {
//    //          a = 1;

//    //          if (x < z)
//    //          {
//    //            b = 2;
//    //            c = 0;
//    //          }
//    //          else
//    //          {
//    //            b = 0;
//    //            c = 2;
//    //          }
//    //        }
//    //      }
//    //      else
//    //      {
//    //        if (x < z)
//    //        {
//    //          a = 2;
//    //          b = 0;
//    //          c = 1;
//    //        }
//    //        else
//    //        {
//    //          a = 0;

//    //          if (y < z)
//    //          {
//    //            b = 2;
//    //            c = 1;
//    //          }
//    //          else
//    //          {
//    //            b = 1;
//    //            c = 2;
//    //          }
//    //        }
//    //      }
//    //      #endregion

//    //      if (pfScales[a] < EPSILON)
//    //      {
//    //        *(pVectorBasis[a]) = pCanonicalBasis[a];
//    //      }

//    //      *pVectorBasis[a] = Vector4.Normalize(*pVectorBasis[a]);

//    //      if (pfScales[b] < EPSILON)
//    //      {
//    //        uint cc;
//    //        double fAbsX, fAbsY, fAbsZ;

//    //        fAbsX = System.Math.Abs(pVectorBasis[a]->X);
//    //        fAbsY = System.Math.Abs(pVectorBasis[a]->Y);
//    //        fAbsZ = System.Math.Abs(pVectorBasis[a]->Z);

//    //        #region Ranking
//    //        if (fAbsX < fAbsY)
//    //        {
//    //          if (fAbsY < fAbsZ)
//    //          {
//    //            cc = 0;
//    //          }
//    //          else
//    //          {
//    //            if (fAbsX < fAbsZ)
//    //            {
//    //              cc = 0;
//    //            }
//    //            else
//    //            {
//    //              cc = 2;
//    //            }
//    //          }
//    //        }
//    //        else
//    //        {
//    //          if (fAbsX < fAbsZ)
//    //          {
//    //            cc = 1;
//    //          }
//    //          else
//    //          {
//    //            if (fAbsY < fAbsZ)
//    //            {
//    //              cc = 1;
//    //            }
//    //            else
//    //            {
//    //              cc = 2;
//    //            }
//    //          }
//    //        }
//    //        #endregion

//    //        *pVectorBasis[b] = Vector4.Cross(*pVectorBasis[a], *(pCanonicalBasis + cc));
//    //      }

//    //      *pVectorBasis[b] = Vector4.Normalize(*pVectorBasis[b]);

//    //      if (pfScales[c] < EPSILON)
//    //      {
//    //        *pVectorBasis[c] = Vector4.Cross(*pVectorBasis[a], *pVectorBasis[b]);
//    //      }

//    //      *pVectorBasis[c] = Vector4.Normalize(*pVectorBasis[c]);

//    //      det = matTemp.GetDeterminant();

//    //      // use Kramer's rule to check for handedness of coordinate system
//    //      if (det < 0)
//    //      {
//    //        // switch coordinate system by negating the scale and inverting the basis vector on the x-axis
//    //        pfScales[a] = -pfScales[a];
//    //        *pVectorBasis[a] = -(*pVectorBasis[a]);

//    //        det = -det;
//    //      }

//    //      det -= 1;
//    //      det *= det;

//    //      if ((EPSILON < det))
//    //      {
//    //        // Non-SRT matrix encountered
//    //        rotation = Quaternion.Identity;
//    //        result = false;
//    //      }
//    //      else
//    //      {
//    //        // generate the quaternion from the matrix
//    //        rotation = Quaternion.CreateFromRotationMatrix(matTemp);
//    //      }
//    //    }
//    //  }

//    //  return result;
//    //}
//    /// <summary>
//    /// Attempts to calculate the inverse of the given matrix. If successful, result will contain the inverted matrix.
//    /// </summary>
//    /// <param name="matrix">The source matrix to invert.</param>
//    /// <param name="result">If successful, contains the inverted matrix.</param>
//    /// <returns>True if the source matrix could be inverted; False otherwise.</returns>
//    public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
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
//      double a = matrix.M11, b = matrix.M12, c = matrix.M13, d = matrix.M14;
//      double e = matrix.M21, f = matrix.M22, g = matrix.M23, h = matrix.M24;
//      double i = matrix.M31, j = matrix.M32, k = matrix.M33, l = matrix.M34;
//      double m = matrix.M41, n = matrix.M42, o = matrix.M43, p = matrix.M44;

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

//      if (System.Math.Abs(det) < double.Epsilon)
//      {
//        result = new Matrix4x4(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);

//        return false;
//      }

//      result = new Matrix4x4();

//      double invDet = 1 / det;

//      result.M11 = a11 * invDet;
//      result.M21 = a12 * invDet;
//      result.M31 = a13 * invDet;
//      result.M41 = a14 * invDet;

//      result.M12 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
//      result.M22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
//      result.M32 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
//      result.M42 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

//      var gp_ho = g * p - h * o;
//      var fp_hn = f * p - h * n;
//      var fo_gn = f * o - g * n;
//      var ep_hm = e * p - h * m;
//      var eo_gm = e * o - g * m;
//      var en_fm = e * n - f * m;

//      result.M13 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
//      result.M23 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
//      result.M33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
//      result.M43 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

//      var gl_hk = g * l - h * k;
//      var fl_hj = f * l - h * j;
//      var fk_gj = f * k - g * j;
//      var el_hi = e * l - h * i;
//      var ek_gi = e * k - g * i;
//      var ej_fi = e * j - f * i;

//      result.M14 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
//      result.M24 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
//      result.M34 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
//      result.M44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

//      return true;
//    }
//    /// <summary>Linearly interpolates between the corresponding values of two matrices.</summary>
//    /// <param name="amount">The relative weight of the second source matrix.</param>
//    public static Matrix4x4 Lerp(Matrix4x4 m1, Matrix4x4 m2, double amount) => new Matrix4x4(
//      // First row
//      m1.M11 + (m2.M11 - m1.M11) * amount,
//      m1.M12 + (m2.M12 - m1.M12) * amount,
//      m1.M13 + (m2.M13 - m1.M13) * amount,
//      m1.M14 + (m2.M14 - m1.M14) * amount,
//      // Second row
//      m1.M21 + (m2.M21 - m1.M21) * amount,
//      m1.M22 + (m2.M22 - m1.M22) * amount,
//      m1.M23 + (m2.M23 - m1.M23) * amount,
//      m1.M24 + (m2.M24 - m1.M24) * amount,
//      // Third row
//      m1.M31 + (m2.M31 - m1.M31) * amount,
//      m1.M32 + (m2.M32 - m1.M32) * amount,
//      m1.M33 + (m2.M33 - m1.M33) * amount,
//      m1.M34 + (m2.M34 - m1.M34) * amount,
//      // Fourth row
//      m1.M41 + (m2.M41 - m1.M41) * amount,
//      m1.M42 + (m2.M42 - m1.M42) * amount,
//      m1.M43 + (m2.M43 - m1.M43) * amount,
//      m1.M44 + (m2.M44 - m1.M44) * amount
//    );
//    /// <summary>
//    /// Multiplies a matrix by another matrix.
//    /// </summary>
//    /// <param name="m1">The first source matrix.</param>
//    /// <param name="m2">The second source matrix.</param>
//    /// <returns>The result of the multiplication.</returns>
//    public static Matrix4x4 Multiply(Matrix4x4 m1, Matrix4x4 m2) => new Matrix4x4(
//      // First row
//      m1.M11 * m2.M11 + m1.M12 * m2.M21 + m1.M13 * m2.M31 + m1.M14 * m2.M41,
//      m1.M11 * m2.M12 + m1.M12 * m2.M22 + m1.M13 * m2.M32 + m1.M14 * m2.M42,
//      m1.M11 * m2.M13 + m1.M12 * m2.M23 + m1.M13 * m2.M33 + m1.M14 * m2.M43,
//      m1.M11 * m2.M14 + m1.M12 * m2.M24 + m1.M13 * m2.M34 + m1.M14 * m2.M44,
//      // Second row
//      m1.M21 * m2.M11 + m1.M22 * m2.M21 + m1.M23 * m2.M31 + m1.M24 * m2.M41,
//      m1.M21 * m2.M12 + m1.M22 * m2.M22 + m1.M23 * m2.M32 + m1.M24 * m2.M42,
//      m1.M21 * m2.M13 + m1.M22 * m2.M23 + m1.M23 * m2.M33 + m1.M24 * m2.M43,
//      m1.M21 * m2.M14 + m1.M22 * m2.M24 + m1.M23 * m2.M34 + m1.M24 * m2.M44,
//      // Third row
//      m1.M31 * m2.M11 + m1.M32 * m2.M21 + m1.M33 * m2.M31 + m1.M34 * m2.M41,
//      m1.M31 * m2.M12 + m1.M32 * m2.M22 + m1.M33 * m2.M32 + m1.M34 * m2.M42,
//      m1.M31 * m2.M13 + m1.M32 * m2.M23 + m1.M33 * m2.M33 + m1.M34 * m2.M43,
//      m1.M31 * m2.M14 + m1.M32 * m2.M24 + m1.M33 * m2.M34 + m1.M34 * m2.M44,
//      // Fourth row
//      m1.M41 * m2.M11 + m1.M42 * m2.M21 + m1.M43 * m2.M31 + m1.M44 * m2.M41,
//      m1.M41 * m2.M12 + m1.M42 * m2.M22 + m1.M43 * m2.M32 + m1.M44 * m2.M42,
//      m1.M41 * m2.M13 + m1.M42 * m2.M23 + m1.M43 * m2.M33 + m1.M44 * m2.M43,
//      m1.M41 * m2.M14 + m1.M42 * m2.M24 + m1.M43 * m2.M34 + m1.M44 * m2.M44
//    );
//    /// <summary>Multiplies a matrix by a scalar value.</summary>
//    public static Matrix4x4 Multiply(Matrix4x4 m, double scalar)
//      => new Matrix4x4(m.M11 * scalar, m.M12 * scalar, m.M13 * scalar, m.M14 * scalar, m.M21 * scalar, m.M22 * scalar, m.M23 * scalar, m.M24 * scalar, m.M31 * scalar, m.M32 * scalar, m.M33 * scalar, m.M34 * scalar, m.M41 * scalar, m.M42 * scalar, m.M43 * scalar, m.M44 * scalar);
//    /// <summary>Returns a new matrix with the negated elements of the given matrix.</summary>
//    public static Matrix4x4 Negate(Matrix4x4 m)
//      => new Matrix4x4(-m.M11, -m.M12, -m.M13, -m.M14, -m.M21, -m.M22, -m.M23, -m.M24, -m.M31, -m.M32, -m.M33, -m.M34, -m.M41, -m.M42, -m.M43, -m.M44);
//    /// <summary>Subtracts the second matrix from the first.</summary>
//    public static Matrix4x4 Subtract(Matrix4x4 m1, Matrix4x4 m2)
//      => new Matrix4x4(m1.M11 - m2.M11, m1.M12 - m2.M12, m1.M13 - m2.M13, m1.M14 - m2.M14, m1.M21 - m2.M21, m1.M22 - m2.M22, m1.M23 - m2.M23, m1.M24 - m2.M24, m1.M31 - m2.M31, m1.M32 - m2.M32, m1.M33 - m2.M33, m1.M34 - m2.M34, m1.M41 - m2.M41, m1.M42 - m2.M42, m1.M43 - m2.M43, m1.M44 - m2.M44);
//    /// <summary>Transforms the given matrix by applying the given Quaternion rotation.</summary>
//    /// <param name="value">The source matrix to transform.</param>
//    /// <param name="rotation">The rotation to apply.</param>
//    public static Matrix4x4 Transform(Matrix4x4 value, Quaternion rotation)
//    {
//      // Compute rotation matrix.
//      double x2 = rotation.X + rotation.X;
//      double y2 = rotation.Y + rotation.Y;
//      double z2 = rotation.Z + rotation.Z;

//      double wx2 = rotation.W * x2;
//      double wy2 = rotation.W * y2;
//      double wz2 = rotation.W * z2;
//      double xx2 = rotation.X * x2;
//      double xy2 = rotation.X * y2;
//      double xz2 = rotation.X * z2;
//      double yy2 = rotation.Y * y2;
//      double yz2 = rotation.Y * z2;
//      double zz2 = rotation.Z * z2;

//      double q11 = 1 - yy2 - zz2;
//      double q21 = xy2 - wz2;
//      double q31 = xz2 + wy2;

//      double q12 = xy2 + wz2;
//      double q22 = 1 - xx2 - zz2;
//      double q32 = yz2 - wx2;

//      double q13 = xz2 - wy2;
//      double q23 = yz2 + wx2;
//      double q33 = 1 - xx2 - yy2;

//      Matrix4x4 result = new Matrix4x4();

//      // First row
//      result.M11 = value.M11 * q11 + value.M12 * q21 + value.M13 * q31;
//      result.M12 = value.M11 * q12 + value.M12 * q22 + value.M13 * q32;
//      result.M13 = value.M11 * q13 + value.M12 * q23 + value.M13 * q33;
//      result.M14 = value.M14;

//      // Second row
//      result.M21 = value.M21 * q11 + value.M22 * q21 + value.M23 * q31;
//      result.M22 = value.M21 * q12 + value.M22 * q22 + value.M23 * q32;
//      result.M23 = value.M21 * q13 + value.M22 * q23 + value.M23 * q33;
//      result.M24 = value.M24;

//      // Third row
//      result.M31 = value.M31 * q11 + value.M32 * q21 + value.M33 * q31;
//      result.M32 = value.M31 * q12 + value.M32 * q22 + value.M33 * q32;
//      result.M33 = value.M31 * q13 + value.M32 * q23 + value.M33 * q33;
//      result.M34 = value.M34;

//      // Fourth row
//      result.M41 = value.M41 * q11 + value.M42 * q21 + value.M43 * q31;
//      result.M42 = value.M41 * q12 + value.M42 * q22 + value.M43 * q32;
//      result.M43 = value.M41 * q13 + value.M42 * q23 + value.M43 * q33;
//      result.M44 = value.M44;

//      return result;
//    }
//    /// <summary>Transposes the rows and columns of a matrix.</summary>
//    /// <param name="matrix">The source matrix.</param>
//    /// <returns>The transposed matrix.</returns>
//    public static Matrix4x4 Transpose(Matrix4x4 matrix)
//      => new Matrix4x4(matrix.M11, matrix.M21, matrix.M31, matrix.M41, matrix.M12, matrix.M22, matrix.M32, matrix.M42, matrix.M13, matrix.M23, matrix.M33, matrix.M43, matrix.M14, matrix.M24, matrix.M34, matrix.M44);
//    #endregion Static methods

//    #region Operator overloads
//    /// <summary>
//    /// Returns a boolean indicating whether the given two matrices are equal.
//    /// </summary>
//    /// <param name="value1">The first matrix to compare.</param>
//    /// <param name="value2">The second matrix to compare.</param>
//    /// <returns>True if the given matrices are equal; False otherwise.</returns>
//    public static bool operator ==(Matrix4x4 value1, Matrix4x4 value2)
//    {
//      return (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && // Check diagonal element first for early out.
//              value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 &&
//              value1.M21 == value2.M21 && value1.M23 == value2.M23 && value1.M24 == value2.M24 &&
//              value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M34 == value2.M34 &&
//              value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43);
//    }

//    /// <summary>
//    /// Returns a boolean indicating whether the given two matrices are not equal.
//    /// </summary>
//    /// <param name="value1">The first matrix to compare.</param>
//    /// <param name="value2">The second matrix to compare.</param>
//    /// <returns>True if the given matrices are not equal; False if they are equal.</returns>
//    public static bool operator !=(Matrix4x4 value1, Matrix4x4 value2)
//    {
//      return (value1.M11 != value2.M11 || value1.M12 != value2.M12 || value1.M13 != value2.M13 || value1.M14 != value2.M14 ||
//              value1.M21 != value2.M21 || value1.M22 != value2.M22 || value1.M23 != value2.M23 || value1.M24 != value2.M24 ||
//              value1.M31 != value2.M31 || value1.M32 != value2.M32 || value1.M33 != value2.M33 || value1.M34 != value2.M34 ||
//              value1.M41 != value2.M41 || value1.M42 != value2.M42 || value1.M43 != value2.M43 || value1.M44 != value2.M44);
//    }

//    /// <summary>
//    /// Returns a new matrix with the negated elements of the given matrix.
//    /// </summary>
//    /// <param name="value">The source matrix.</param>
//    /// <returns>The negated matrix.</returns>
//    public static Matrix4x4 operator -(Matrix4x4 value)
//    {
//      Matrix4x4 m = new Matrix4x4();

//      m.M11 = -value.M11;
//      m.M12 = -value.M12;
//      m.M13 = -value.M13;
//      m.M14 = -value.M14;
//      m.M21 = -value.M21;
//      m.M22 = -value.M22;
//      m.M23 = -value.M23;
//      m.M24 = -value.M24;
//      m.M31 = -value.M31;
//      m.M32 = -value.M32;
//      m.M33 = -value.M33;
//      m.M34 = -value.M34;
//      m.M41 = -value.M41;
//      m.M42 = -value.M42;
//      m.M43 = -value.M43;
//      m.M44 = -value.M44;

//      return m;
//    }
//    /// <summary>
//    /// Adds two matrices together.
//    /// </summary>
//    /// <param name="value1">The first source matrix.</param>
//    /// <param name="value2">The second source matrix.</param>
//    /// <returns>The resulting matrix.</returns>
//    public static Matrix4x4 operator +(Matrix4x4 value1, Matrix4x4 value2)
//    {
//      Matrix4x4 m = new Matrix4x4();

//      m.M11 = value1.M11 + value2.M11;
//      m.M12 = value1.M12 + value2.M12;
//      m.M13 = value1.M13 + value2.M13;
//      m.M14 = value1.M14 + value2.M14;
//      m.M21 = value1.M21 + value2.M21;
//      m.M22 = value1.M22 + value2.M22;
//      m.M23 = value1.M23 + value2.M23;
//      m.M24 = value1.M24 + value2.M24;
//      m.M31 = value1.M31 + value2.M31;
//      m.M32 = value1.M32 + value2.M32;
//      m.M33 = value1.M33 + value2.M33;
//      m.M34 = value1.M34 + value2.M34;
//      m.M41 = value1.M41 + value2.M41;
//      m.M42 = value1.M42 + value2.M42;
//      m.M43 = value1.M43 + value2.M43;
//      m.M44 = value1.M44 + value2.M44;

//      return m;
//    }
//    /// <summary>
//    /// Subtracts the second matrix from the first.
//    /// </summary>
//    /// <param name="value1">The first source matrix.</param>
//    /// <param name="value2">The second source matrix.</param>
//    /// <returns>The result of the subtraction.</returns>
//    public static Matrix4x4 operator -(Matrix4x4 value1, Matrix4x4 value2)
//    {
//      Matrix4x4 m = new Matrix4x4();

//      m.M11 = value1.M11 - value2.M11;
//      m.M12 = value1.M12 - value2.M12;
//      m.M13 = value1.M13 - value2.M13;
//      m.M14 = value1.M14 - value2.M14;
//      m.M21 = value1.M21 - value2.M21;
//      m.M22 = value1.M22 - value2.M22;
//      m.M23 = value1.M23 - value2.M23;
//      m.M24 = value1.M24 - value2.M24;
//      m.M31 = value1.M31 - value2.M31;
//      m.M32 = value1.M32 - value2.M32;
//      m.M33 = value1.M33 - value2.M33;
//      m.M34 = value1.M34 - value2.M34;
//      m.M41 = value1.M41 - value2.M41;
//      m.M42 = value1.M42 - value2.M42;
//      m.M43 = value1.M43 - value2.M43;
//      m.M44 = value1.M44 - value2.M44;

//      return m;
//    }
//    /// <summary>
//    /// Multiplies a matrix by another matrix.
//    /// </summary>
//    /// <param name="value1">The first source matrix.</param>
//    /// <param name="value2">The second source matrix.</param>
//    /// <returns>The result of the multiplication.</returns>
//    public static Matrix4x4 operator *(Matrix4x4 value1, Matrix4x4 value2)
//    {
//      Matrix4x4 m = new Matrix4x4();

//      // First row
//      m.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
//      m.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
//      m.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
//      m.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;

//      // Second row
//      m.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
//      m.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
//      m.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
//      m.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;

//      // Third row
//      m.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
//      m.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
//      m.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
//      m.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;

//      // Fourth row
//      m.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
//      m.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
//      m.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
//      m.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;

//      return m;
//    }
//    /// <summary>
//    /// Multiplies a matrix by a scalar value.
//    /// </summary>
//    /// <param name="value1">The source matrix.</param>
//    /// <param name="value2">The scaling factor.</param>
//    /// <returns>The scaled matrix.</returns>
//    public static Matrix4x4 operator *(Matrix4x4 value1, double value2)
//    {
//      Matrix4x4 m = new Matrix4x4();

//      m.M11 = value1.M11 * value2;
//      m.M12 = value1.M12 * value2;
//      m.M13 = value1.M13 * value2;
//      m.M14 = value1.M14 * value2;
//      m.M21 = value1.M21 * value2;
//      m.M22 = value1.M22 * value2;
//      m.M23 = value1.M23 * value2;
//      m.M24 = value1.M24 * value2;
//      m.M31 = value1.M31 * value2;
//      m.M32 = value1.M32 * value2;
//      m.M33 = value1.M33 * value2;
//      m.M34 = value1.M34 * value2;
//      m.M41 = value1.M41 * value2;
//      m.M42 = value1.M42 * value2;
//      m.M43 = value1.M43 * value2;
//      m.M44 = value1.M44 * value2;
//      return m;
//    }
//    #endregion Operator overloads

//    #region Implemented interfaces
//    // IEquatable
//    /// <summary>Returns a boolean indicating whether this matrix instance is equal to the specified matrix.</summary>
//    public bool Equals(Matrix4x4 m)
//      => M11 == m.M11 && M22 == m.M22 && M33 == m.M33 && M44 == m.M44 && // Check diagonal elements first for early out.
//      M12 == m.M12 && M13 == m.M13 && M14 == m.M14 &&
//      M21 == m.M21 && M23 == m.M23 && M24 == m.M24 &&
//      M31 == m.M31 && M32 == m.M32 && M34 == m.M34 &&
//      M41 == m.M41 && M42 == m.M42 && M43 == m.M43;
//    #endregion Implemented interfaces

//    #region Object overrides
//    /// <summary>Returns a boolean indicating whether the given Object is equal to this matrix instance.</summary>
//    public override bool Equals(object? obj)
//      => obj is Matrix4x4 o && Equals(o);
//    /// <summary>Returns the hash code for this instance.</summary>
//    public override int GetHashCode()
//    {
//      var hc = new System.HashCode();
//      hc.Add(M11);
//      hc.Add(M12);
//      hc.Add(M13);
//      hc.Add(M14);
//      hc.Add(M21);
//      hc.Add(M22);
//      hc.Add(M23);
//      hc.Add(M24);
//      hc.Add(M31);
//      hc.Add(M32);
//      hc.Add(M33);
//      hc.Add(M34);
//      hc.Add(M41);
//      hc.Add(M42);
//      hc.Add(M43);
//      hc.Add(M44);
//      return hc.ToHashCode();
//    }
//    /// <summary>Returns a String representing this matrix instance.</summary>
//    public override string ToString()
//      => $"<{GetType().Name}: M11={M11} M12={M12} M13={M13} M14={M14}, M21={M21} M22={M22} M23={M23} M24={M24}, M31={M31} M32={M32} M33={M33} M34={M34}, M41={M41} M42={M42} M43={M43} M44={M44}>";
//    #endregion Object overrides
//  }
//}
