//#if NET7_0_OR_GREATER
//namespace Flux.Geometry
//{
//  /// <summary>A matrix of 16 elements (4 rows by 4 columns).</summary>
//  /// <remarks>All angles in radians.</remarks>
//  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Matrix4x4.cs"/>
//  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//  public readonly record struct Matrix4
//    : IMatrix4
//  {
//    /// <summary>Returns an empty matrix.</summary>
//    public static readonly Matrix4 Empty;

//    /// <summary>Returns the multiplicative identity matrix.</summary>
//    public static Matrix4 Identity
//      => new(
//        1, 0, 0, 0,
//        0, 1, 0, 0,
//        0, 0, 1, 0,
//        0, 0, 0, 1
//      );

//    public static Matrix4 ChangeOfBasisMatrix
//      => new(
//        1, 0, 0, 0,
//        0, 1, 0, 0,
//        0, 0, 1, 0,
//        0, 0, 0, 1
//      );

//    private readonly double m_11; // Row 1, 4 columns.
//    private readonly double m_12;
//    private readonly double m_13;
//    private readonly double m_14;

//    private readonly double m_21; // Row 2, 4 columns.
//    private readonly double m_22;
//    private readonly double m_23;
//    private readonly double m_24;

//    private readonly double m_31; // Row 3, 4 columns.
//    private readonly double m_32;
//    private readonly double m_33;
//    private readonly double m_34;

//    private readonly double m_41; // Row 4, 4 columns.
//    private readonly double m_42;
//    private readonly double m_43;
//    private readonly double m_44;

//    /// <summary>Constructs a Matrix4x4 from the given components.</summary>
//    public Matrix4(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
//    {
//      m_11 = m11;
//      m_12 = m12;
//      m_13 = m13;
//      m_14 = m14;

//      m_21 = m21;
//      m_22 = m22;
//      m_23 = m23;
//      m_24 = m24;

//      m_31 = m31;
//      m_32 = m32;
//      m_33 = m33;
//      m_34 = m34;

//      m_41 = m41;
//      m_42 = m42;
//      m_43 = m43;
//      m_44 = m44;
//    }

//    /// <summary>Value at row 1, column 1 of the matrix.</summary>
//    public double M11 => m_11;
//    /// <summary>Value at row 1, column 2 of the matrix.</summary>
//    public double M12 => m_12;
//    /// <summary>Value at row 1, column 3 of the matrix.</summary>
//    public double M13 => m_13;
//    /// <summary>Value at row 1, column 4 of the matrix.</summary>
//    public double M14 => m_14;
//    /// <summary>Value at row 2, column 1 of the matrix.</summary>
//    public double M21 => m_21;
//    /// <summary>Value at row 2, column 2 of the matrix.</summary>
//    public double M22 => m_22;
//    /// <summary>Value at row 2, column 3 of the matrix.</summary>
//    public double M23 => m_23;
//    /// <summary>Value at row 2, column 4 of the matrix.</summary>
//    public double M24 => m_24;
//    /// <summary>Value at row 3, column 1 of the matrix.</summary>
//    public double M31 => m_31;
//    /// <summary>Value at row 3, column 2 of the matrix.</summary>
//    public double M32 => m_32;
//    /// <summary>Value at row 3, column 3 of the matrix.</summary>
//    public double M33 => m_33;
//    /// <summary>Value at row 3, column 4 of the matrix.</summary>
//    public double M34 => m_34;
//    /// <summary>Value at row 4, column 1 of the matrix.</summary>
//    public double M41 => m_41;
//    /// <summary>Value at row 4, column 2 of the matrix.</summary>
//    public double M42 => m_42;
//    /// <summary>Value at row 4, column 3 of the matrix.</summary>
//    public double M43 => m_43;
//    /// <summary>Value at row 4, column 4 of the matrix.</summary>
//    public double M44 => m_44;

//    /// <summary>Returns whether the matrix is empty.</summary>
//    public bool IsEmpty => Equals(Empty);

//    /// <summary>Returns whether the matrix is the identity matrix.</summary>
//    public bool IsIdentity => Equals(Identity);

//    #region Static methods
//    ///// <summary>Creates a spherical billboard that rotates around a specified object position.</summary>
//    ///// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
//    ///// <param name="cameraPosition">Position of the camera.</param>
//    ///// <param name="cameraUpVector">The up vector of the camera.</param>
//    ///// <param name="cameraForwardVector">The forward vector of the camera.</param>
//    ///// <returns>The created billboard matrix</returns>
//    //public static Matrix4<double> CreateBillboard(CoordinateSystems.CartesianCoordinate4 objectPosition, CoordinateSystems.CartesianCoordinate4 cameraPosition, CoordinateSystems.CartesianCoordinate4 cameraUpVector, CoordinateSystems.CartesianCoordinate4 cameraForwardVector)
//    //{
//    //  const double epsilon = 1e-4f;

//    //  var zaxis = new CoordinateSystems.CartesianCoordinate4(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);

//    //  zaxis = CoordinateSystems.CartesianCoordinate4.EuclideanLengthSquared(zaxis) is var znorm && znorm < epsilon ? -cameraForwardVector : zaxis * (1 / System.Math.Sqrt(znorm));

//    //  var xaxis = CoordinateSystems.CartesianCoordinate4.Normalize(CoordinateSystems.CartesianCoordinate4.Cross(cameraUpVector, zaxis));
//    //  var yaxis = CoordinateSystems.CartesianCoordinate4.Cross(zaxis, xaxis);

//    //  return new(xaxis.X, xaxis.Y, xaxis.Z, 0, yaxis.X, yaxis.Y, yaxis.Z, 0, zaxis.X, zaxis.Y, zaxis.Z, 0, objectPosition.X, objectPosition.Y, objectPosition.Z, 1);
//    //}
//    ///// <summary>
//    ///// Creates a cylindrical billboard that rotates around a specified axis.
//    ///// </summary>
//    ///// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
//    ///// <param name="cameraPosition">Position of the camera.</param>
//    ///// <param name="rotateAxis">Axis to rotate the billboard around.</param>
//    ///// <param name="cameraForwardVector">Forward vector of the camera.</param>
//    ///// <param name="objectForwardVector">Forward vector of the object.</param>
//    ///// <returns>The created billboard matrix.</returns>
//    //public static Matrix4<double> CreateConstrainedBillboard(CoordinateSystems.CartesianCoordinate4 objectPosition, CoordinateSystems.CartesianCoordinate4 cameraPosition, CoordinateSystems.CartesianCoordinate4 rotateAxis, CoordinateSystems.CartesianCoordinate4 cameraForwardVector, CoordinateSystems.CartesianCoordinate4 objectForwardVector)
//    //{
//    //  const double epsilon = 1e-4f;
//    //  const double minAngle = 1 - (0.1f * GenericMath.PiOver180); // 0.1 degrees

//    //  // Treat the case when object and camera positions are too close.
//    //  var faceDir = new CoordinateSystems.CartesianCoordinate4(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);

//    //  faceDir = CoordinateSystems.CartesianCoordinate4.EuclideanLengthSquared(faceDir) is var norm && norm < epsilon ? -cameraForwardVector : faceDir * (1 / System.Math.Sqrt(norm));

//    //  CoordinateSystems.CartesianCoordinate4 yaxis = rotateAxis;
//    //  CoordinateSystems.CartesianCoordinate4 xaxis;
//    //  CoordinateSystems.CartesianCoordinate4 zaxis;

//    //  // Treat the case when angle between faceDir and rotateAxis is too close to 0.
//    //  var dot = CoordinateSystems.CartesianCoordinate4.Dot(rotateAxis, faceDir);

//    //  if (System.Math.Abs(dot) > minAngle)
//    //  {
//    //    zaxis = objectForwardVector;

//    //    // Make sure passed values are useful for compute.
//    //    dot = CoordinateSystems.CartesianCoordinate4.Dot(rotateAxis, zaxis);

//    //    if (System.Math.Abs(dot) > minAngle)
//    //    {
//    //      zaxis = (System.Math.Abs(rotateAxis.Z) > minAngle) ? new CoordinateSystems.CartesianCoordinate4(1, 0, 0) : new CoordinateSystems.CartesianCoordinate4(0, 0, -1);
//    //    }

//    //    xaxis = CoordinateSystems.CartesianCoordinate4.Normalize(CoordinateSystems.CartesianCoordinate4.Cross(rotateAxis, zaxis));
//    //    zaxis = CoordinateSystems.CartesianCoordinate4.Normalize(CoordinateSystems.CartesianCoordinate4.Cross(xaxis, rotateAxis));
//    //  }
//    //  else
//    //  {
//    //    xaxis = CoordinateSystems.CartesianCoordinate4.Normalize(CoordinateSystems.CartesianCoordinate4.Cross(rotateAxis, faceDir));
//    //    zaxis = CoordinateSystems.CartesianCoordinate4.Normalize(CoordinateSystems.CartesianCoordinate4.Cross(xaxis, yaxis));
//    //  }

//    //  return new(
//    //    double.CreateChecked(xaxis.X), double.CreateChecked(xaxis.Y), double.CreateChecked(xaxis.Z), 0,
//    //    double.CreateChecked(yaxis.X), double.CreateChecked(yaxis.Y), double.CreateChecked(yaxis.Z), 0,
//    //    double.CreateChecked(zaxis.X), double.CreateChecked(zaxis.Y), double.CreateChecked(zaxis.Z), 0,
//    //    double.CreateChecked(objectPosition.X), double.CreateChecked(objectPosition.Y), double.CreateChecked(objectPosition.Z), 1
//    //  );
//    //}
//    ///// <summary>
//    ///// Creates a matrix that rotates around an arbitrary vector.
//    ///// </summary>
//    ///// <param name="axis">The axis to rotate around.</param>
//    ///// <param name="angle">The angle to rotate around the given axis, in radians.</param>
//    ///// <returns>The rotation matrix.</returns>
//    //public static Matrix4<double> CreateFromAxisAngle(CoordinateSystems.CartesianCoordinate4 axis, double angle)
//    //{
//    //  // a: angle
//    //  // x, y, z: unit vector for axis.
//    //  //
//    //  // Rotation matrix M can compute by using below equation.
//    //  //
//    //  //        T               T
//    //  //  M = uu + (cos a)( I-uu ) + (sin a)S
//    //  //
//    //  // Where:
//    //  //
//    //  //  u = ( x, y, z )
//    //  //
//    //  //      [  0 -z  y ]
//    //  //  S = [  z  0 -x ]
//    //  //      [ -y  x  0 ]
//    //  //
//    //  //      [ 1 0 0 ]
//    //  //  I = [ 0 1 0 ]
//    //  //      [ 0 0 1 ]
//    //  //
//    //  //
//    //  //     [  xx+cosa*(1-xx)   yx-cosa*yx-sina*z zx-cosa*xz+sina*y ]
//    //  // M = [ xy-cosa*yx+sina*z    yy+cosa(1-yy)  yz-cosa*yz-sina*x ]
//    //  //     [ zx-cosa*zx-sina*y zy-cosa*zy+sina*x   zz+cosa*(1-zz)  ]
//    //  //
//    //  double x = double.CreateChecked(axis.X), y = double.CreateChecked(axis.Y), z = double.CreateChecked(axis.Z);
//    //  double sa = double.Sin(angle), ca = double.Cos(angle);
//    //  double xx = x * x, yy = y * y, zz = z * z;
//    //  double xy = x * y, xz = x * z, yz = y * z;

//    //  return new(
//    //    xx + ca * (1 - xx), xy - ca * xy + sa * z, xz - ca * xz - sa * y, 0,
//    //    xy - ca * xy - sa * z, yy + ca * (1 - yy), yz - ca * yz + sa * x, 0,
//    //    xz - ca * xz + sa * y, yz - ca * yz - sa * x, zz + ca * (1 - zz), 0,
//    //    0, 0, 0, 1
//    //  );
//    //}
//    ///// <summary>
//    ///// Creates a rotation matrix from the given Quaternion rotation value.
//    ///// </summary>
//    ///// <param name="quaternion">The source Quaternion.</param>
//    ///// <returns>The rotation matrix.</returns>
//    //public static Matrix4<double> CreateFromQuaternion(Quaternion<double> quaternion)
//    //{
//    //  var xx = quaternion.X * quaternion.X;
//    //  var yy = quaternion.Y * quaternion.Y;
//    //  var zz = quaternion.Z * quaternion.Z;

//    //  var xy = quaternion.X * quaternion.Y;
//    //  var wz = quaternion.Z * quaternion.W;
//    //  var xz = quaternion.Z * quaternion.X;
//    //  var wy = quaternion.Y * quaternion.W;
//    //  var yz = quaternion.Y * quaternion.Z;
//    //  var wx = quaternion.X * quaternion.W;

//    //  return new(
//    //    1 - (yy + zz).Multiply(2), (xy + wz).Multiply(2), (xz - wy).Multiply(2), 0,
//    //    (xy - wz).Multiply(2), 1 - (zz + xx).Multiply(2), (yz + wx).Multiply(2), 0,
//    //    (xz + wy).Multiply(2), (yz - wx).Multiply(2), 1 - (yy + xx).Multiply(2), 0,
//    //    0, 0, 0, 1
//    //  );
//    //}
//    ///// <summary>
//    ///// Creates a rotation matrix from the specified yaw, pitch, and roll.
//    ///// </summary>
//    ///// <param name="yaw">Angle of rotation, in radians, around the Y-axis.</param>
//    ///// <param name="pitch">Angle of rotation, in radians, around the X-axis.</param>
//    ///// <param name="roll">Angle of rotation, in radians, around the Z-axis.</param>
//    ///// <returns>The rotation matrix.</returns>
//    //public static Matrix4<double> CreateFromYawPitchRoll(double yaw, double pitch, double roll)
//    //  => Quaternion<double>.CreateFromYawPitchRoll(yaw, pitch, roll).ToMatrix4();
//    ///// <summary>
//    ///// Creates a view matrix.
//    ///// </summary>
//    ///// <param name="cameraPosition">The position of the camera.</param>
//    ///// <param name="cameraTarget">The target towards which the camera is pointing.</param>
//    ///// <param name="cameraUpVector">The direction that is "up" from the camera's point of view.</param>
//    ///// <returns>The view matrix.</returns>
//    //public static Matrix4<double> CreateLookAt(CoordinateSystems.CartesianCoordinate4 cameraPosition, CoordinateSystems.CartesianCoordinate4 cameraTarget, CoordinateSystems.CartesianCoordinate4 cameraUpVector)
//    //{
//    //  var zaxis = double.CreateChecked(CoordinateSystems.CartesianCoordinate4.Normalize(cameraPosition - cameraTarget));
//    //  var xaxis = double.CreateChecked(CoordinateSystems.CartesianCoordinate4.Normalize(CoordinateSystems.CartesianCoordinate4.Cross(cameraUpVector, zaxis)));
//    //  var yaxis = double.CreateChecked(CoordinateSystems.CartesianCoordinate4.Cross(zaxis, xaxis));

//    //  return new(
//    //    xaxis.X, yaxis.X, zaxis.X, 0,
//    //    xaxis.Y, yaxis.Y, zaxis.Y, 0,
//    //    xaxis.Z, yaxis.Z, zaxis.Z, 0,
//    //    double.CreateChecked(-CoordinateSystems.CartesianCoordinate4.Dot(xaxis, cameraPosition)), double.CreateChecked(-CoordinateSystems.CartesianCoordinate4.Dot(yaxis, cameraPosition)), double.CreateChecked(-CoordinateSystems.CartesianCoordinate4.Dot(zaxis, cameraPosition)), 1
//    //  );
//    //}
//    /// <summary>
//    /// Creates an orthographic perspective matrix from the given view volume dimensions.
//    /// </summary>
//    /// <param name="width">Width of the view volume.</param>
//    /// <param name="height">Height of the view volume.</param>
//    /// <param name="zNearPlane">Minimum Z-value of the view volume.</param>
//    /// <param name="zFarPlane">Maximum Z-value of the view volume.</param>
//    /// <returns>The orthographic projection matrix.</returns>
//    public static Matrix4 CreateOrthographic(double width, double height, double zNearPlane, double zFarPlane)
//      => new(
//        2 / width, 0, 0, 0, 0,
//        2 / height, 0, 0, 0, 0,
//        1 / (zNearPlane - zFarPlane), 0,
//        0, 0, zNearPlane / (zNearPlane - zFarPlane), 1
//      );
//    ///// <summary>
//    ///// Builds a customized, orthographic projection matrix.
//    ///// </summary>
//    ///// <param name="left">Minimum X-value of the view volume.</param>
//    ///// <param name="right">Maximum X-value of the view volume.</param>
//    ///// <param name="bottom">Minimum Y-value of the view volume.</param>
//    ///// <param name="top">Maximum Y-value of the view volume.</param>
//    ///// <param name="zNearPlane">Minimum Z-value of the view volume.</param>
//    ///// <param name="zFarPlane">Maximum Z-value of the view volume.</param>
//    ///// <returns>The orthographic projection matrix.</returns>
//    //public static Matrix4<double> CreateOrthographicOffCenter(double left, double right, double bottom, double top, double zNearPlane, double zFarPlane)
//    //  => new(2 / (right - left), 0, 0, 0, 0, 2 / (top - bottom), 0, 0, 0, 0, 1 / (zNearPlane - zFarPlane), 0, (left + right) / (left - right), (top + bottom) / (bottom - top), zNearPlane / (zNearPlane - zFarPlane), 1);
//    ///// <summary>
//    ///// Creates a perspective projection matrix based on a field of view, aspect ratio, and near and far view plane distances. 
//    ///// </summary>
//    ///// <param name="fieldOfView">Field of view in the y direction, in radians.</param>
//    ///// <param name="aspectRatio">Aspect ratio, defined as view space width divided by height.</param>
//    ///// <param name="nearPlaneDistance">Distance to the near view plane.</param>
//    ///// <param name="farPlaneDistance">Distance to the far view plane.</param>
//    ///// <returns>The perspective projection matrix.</returns>
//    //public static Matrix4<double> CreatePerspectiveFieldOfView(double fieldOfView, double aspectRatio, double nearPlaneDistance, double farPlaneDistance)
//    //{
//    //  if (fieldOfView <= 0 || fieldOfView >= System.Math.PI) throw new System.ArgumentOutOfRangeException(nameof(fieldOfView));
//    //  if (nearPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));
//    //  if (farPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(farPlaneDistance));
//    //  if (nearPlaneDistance >= farPlaneDistance) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));

//    //  var yScale = 1 / System.Math.Tan(fieldOfView * 0.5);
//    //  var xScale = yScale / aspectRatio;

//    //  return new(xScale, 0, 0, 0, 0, yScale, 0, 0, 0, 0, farPlaneDistance / (nearPlaneDistance - farPlaneDistance), -1, 0, 0, nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance), 0);
//    //}
//    ///// <summary>
//    ///// Creates a perspective projection matrix from the given view volume dimensions.
//    ///// </summary>
//    ///// <param name="width">Width of the view volume at the near view plane.</param>
//    ///// <param name="height">Height of the view volume at the near view plane.</param>
//    ///// <param name="nearPlaneDistance">Distance to the near view plane.</param>
//    ///// <param name="farPlaneDistance">Distance to the far view plane.</param>
//    ///// <returns>The perspective projection matrix.</returns>
//    //public static Matrix4<double> CreatePerspective(double width, double height, double nearPlaneDistance, double farPlaneDistance)
//    //{
//    //  if (nearPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));
//    //  if (farPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(farPlaneDistance));
//    //  if (nearPlaneDistance >= farPlaneDistance) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));

//    //  return new(2 * nearPlaneDistance / width, 0, 0, 0, 0, 2 * nearPlaneDistance / height, 0, 0, 0, 0, farPlaneDistance / (nearPlaneDistance - farPlaneDistance), -1, 0, 0, nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance), 0);
//    //}
//    ///// <summary>
//    ///// Creates a customized, perspective projection matrix.
//    ///// </summary>
//    ///// <param name="left">Minimum x-value of the view volume at the near view plane.</param>
//    ///// <param name="right">Maximum x-value of the view volume at the near view plane.</param>
//    ///// <param name="bottom">Minimum y-value of the view volume at the near view plane.</param>
//    ///// <param name="top">Maximum y-value of the view volume at the near view plane.</param>
//    ///// <param name="nearPlaneDistance">Distance to the near view plane.</param>
//    ///// <param name="farPlaneDistance">Distance to of the far view plane.</param>
//    ///// <returns>The perspective projection matrix.</returns>
//    //public static Matrix4<double> CreatePerspectiveOffCenter(double left, double right, double bottom, double top, double nearPlaneDistance, double farPlaneDistance)
//    //{
//    //  if (nearPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));
//    //  if (farPlaneDistance <= 0) throw new System.ArgumentOutOfRangeException(nameof(farPlaneDistance));
//    //  if (nearPlaneDistance >= farPlaneDistance) throw new System.ArgumentOutOfRangeException(nameof(nearPlaneDistance));

//    //  return new(2 * nearPlaneDistance / (right - left), 0, 0, 0, 0, 2 * nearPlaneDistance / (top - bottom), 0, 0, (left + right) / (right - left), (top + bottom) / (top - bottom), farPlaneDistance / (nearPlaneDistance - farPlaneDistance), -1, 0, 0, nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance), 0);
//    //}
//    /// <summary>
//    /// Creates a Matrix that reflects the coordinate system about a specified Plane.
//    /// </summary>
//    /// <param name="value">The Plane about which to create a reflection.</param>
//    /// <returns>A new matrix expressing the reflection.</returns>
//    public static Matrix4 CreateReflection(Geometry.Plane value)
//    {
//      value = value.Normalize();

//      var a = value.X;
//      var b = value.Y;
//      var c = value.Z;

//      var fa = a * -2;
//      var fb = b * -2;
//      var fc = c * -2;

//      var vd = value.Distance;

//      return new(
//        fa * a + 1, fb * a, fc * a, 0,
//        fa * b, fb * b + 1, fc * b, 0,
//        fa * c, fb * c, fc * c + 1, 0,
//        fa * vd, fb * vd, fc * vd, 1
//      );
//    }
//    /// <summary>
//    /// Creates a matrix for rotating points around the X-axis.
//    /// </summary>
//    /// <param name="radians">The amount, in radians, by which to rotate around the X-axis.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4 CreateRotationX(double radians)
//    {
//      // [  1  0  0  0 ]
//      // [  0  c  s  0 ]
//      // [  0 -s  c  0 ]
//      // [  0  0  0  1 ]

//      var (s, c) = double.SinCos(radians);

//      return new(1, 0, 0, 0, 0, c, s, 0, 0, -s, c, 0, 0, 0, 0, 1);
//    }
//    ///// <summary>
//    ///// Creates a matrix for rotating points around the X-axis, from a center point.
//    ///// </summary>
//    ///// <param name="radians">The amount, in radians, by which to rotate around the X-axis.</param>
//    ///// <param name="centerPoint">The center point.</param>
//    ///// <returns>The rotation matrix.</returns>
//    //public static Matrix4<double> CreateRotationX(double radians, CoordinateSystems.CartesianCoordinate4 centerPoint)
//    //{
//    //  // [  1  0  0  0 ]
//    //  // [  0  c  s  0 ]
//    //  // [  0 -s  c  0 ]
//    //  // [  0  y  z  1 ]

//    //  var c = double.Cos(radians);
//    //  var s = double.Sin(radians);

//    //  var y = centerPoint.Y * (1 - c) + centerPoint.Z * s;
//    //  var z = centerPoint.Z * (1 - c) - centerPoint.Y * s;

//    //  return new(1, 0, 0, 0, 0, c, s, 0, 0, -s, c, 0, 0, y, z, 1);
//    //}
//    /// <summary>
//    /// Creates a matrix for rotating points around the Y-axis.
//    /// </summary>
//    /// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4 CreateRotationY(double radians)
//    {
//      // [  c  0 -s  0 ]
//      // [  0  1  0  0 ]
//      // [  s  0  c  0 ]
//      // [  0  0  0  1 ]

//      var (s, c) = double.SinCos(radians);

//      return new(c, 0, -s, 0, 0, 1, 0, 0, s, 0, c, 0, 0, 0, 0, 1);
//    }
//    ///// <summary>
//    ///// Creates a matrix for rotating points around the Y-axis, from a center point.
//    ///// </summary>
//    ///// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
//    ///// <param name="centerPoint">The center point.</param>
//    ///// <returns>The rotation matrix.</returns>
//    //public static Matrix4<double> CreateRotationY(double radians, CoordinateSystems.CartesianCoordinate4 centerPoint)
//    //{
//    //  // [  c  0 -s  0 ]
//    //  // [  0  1  0  0 ]
//    //  // [  s  0  c  0 ]
//    //  // [  x  0  z  1 ]

//    //  var c = double.Cos(radians);
//    //  var s = double.Sin(radians);

//    //  var x = centerPoint.X * (1 - c) - centerPoint.Z * s;
//    //  var z = centerPoint.Z * (1 - c) + centerPoint.X * s;

//    //  return new(c, 0, -s, 0, 0, 1, 0, 0, s, 0, c, 0, x, 0, z, 1);
//    //}
//    /// <summary>
//    /// Creates a matrix for rotating points around the Z-axis.
//    /// </summary>
//    /// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
//    /// <returns>The rotation matrix.</returns>
//    public static Matrix4 CreateRotationZ(double radians)
//    {
//      // [  c  s  0  0 ]
//      // [ -s  c  0  0 ]
//      // [  0  0  1  0 ]
//      // [  0  0  0  1 ]

//      var (s, c) = double.SinCos(radians);

//      return new(c, s, 0, 0, -s, c, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
//    }
//    ///// <summary>
//    ///// Creates a matrix for rotating points around the Z-axis, from a center point.
//    ///// </summary>
//    ///// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
//    ///// <param name="centerPoint">The center point.</param>
//    ///// <returns>The rotation matrix.</returns>
//    //public static Matrix4<double> CreateRotationZ(double radians, CoordinateSystems.CartesianCoordinate4 centerPoint)
//    //{
//    //  // [  c  s  0  0 ]
//    //  // [ -s  c  0  0 ]
//    //  // [  0  0  1  0 ]
//    //  // [  x  y  0  1 ]

//    //  var c = double.Cos(radians);
//    //  var s = double.Sin(radians);

//    //  var x = centerPoint.X * (1 - c) + centerPoint.Y * s;
//    //  var y = centerPoint.Y * (1 - c) - centerPoint.X * s;

//    //  return new(c, s, 0, 0, -s, c, 0, 0, 0, 0, 1, 0, x, y, 0, 1);
//    //}
//    /// <summary>
//    /// Creates a scaling matrix.
//    /// </summary>
//    /// <param name="xScale">Value to scale by on the X-axis.</param>
//    /// <param name="yScale">Value to scale by on the Y-axis.</param>
//    /// <param name="zScale">Value to scale by on the Z-axis.</param>
//    /// <returns>The scaling matrix.</returns>
//    public static Matrix4 CreateScale(double xScale, double yScale, double zScale)
//      => new(xScale, 0, 0, 0, 0, yScale, 0, 0, 0, 0, zScale, 0, 0, 0, 0, 1);
//    ///// <summary>
//    ///// Creates a scaling matrix with a center point.
//    ///// </summary>
//    ///// <param name="xScale">Value to scale by on the X-axis.</param>
//    ///// <param name="yScale">Value to scale by on the Y-axis.</param>
//    ///// <param name="zScale">Value to scale by on the Z-axis.</param>
//    ///// <param name="centerPoint">The center point.</param>
//    ///// <returns>The scaling matrix.</returns>
//    //public static Matrix4<double> CreateScale(double xScale, double yScale, double zScale, CoordinateSystems.CartesianCoordinate4 centerPoint)
//    //{
//    //  var tx = centerPoint.X * (1 - xScale);
//    //  var ty = centerPoint.Y * (1 - yScale);
//    //  var tz = centerPoint.Z * (1 - zScale);

//    //  return new(xScale, 0, 0, 0, 0, yScale, 0, 0, 0, 0, zScale, 0, tx, ty, tz, 1);
//    //}
//    ///// <summary>Creates a scaling matrix.</summary>
//    ///// <param name="scales">The vector containing the amount to scale by on each axis.</param>
//    ///// <returns>The scaling matrix.</returns>
//    //public static Matrix4<double> CreateScale(CoordinateSystems.CartesianCoordinate4 scales)
//    //  => new(scales.X, 0, 0, 0, 0, scales.Y, 0, 0, 0, 0, scales.Z, 0, 0, 0, 0, 1);
//    ///// <summary>
//    ///// Creates a scaling matrix with a center point.
//    ///// </summary>
//    ///// <param name="scales">The vector containing the amount to scale by on each axis.</param>
//    ///// <param name="centerPoint">The center point.</param>
//    ///// <returns>The scaling matrix.</returns>
//    //public static Matrix4 CreateScale(CoordinateSystems.CartesianCoordinate4 scales, CoordinateSystems.CartesianCoordinate4 centerPoint)
//    //{
//    //  var tx = centerPoint.X * (1 - scales.X);
//    //  var ty = centerPoint.Y * (1 - scales.Y);
//    //  var tz = centerPoint.Z * (1 - scales.Z);

//    //  return new(scales.X, 0, 0, 0, 0, scales.Y, 0, 0, 0, 0, scales.Z, 0, tx, ty, tz, 1);
//    //}
//    /// <summary>
//    /// Creates a uniform scaling matrix that scales equally on each axis.
//    /// </summary>
//    /// <param name="scale">The uniform scaling factor.</param>
//    /// <returns>The scaling matrix.</returns>
//    public static Matrix4 CreateScale(double scale)
//      => new(scale, 0, 0, 0, 0, scale, 0, 0, 0, 0, scale, 0, 0, 0, 0, 1);
//    ///// <summary>
//    ///// Creates a uniform scaling matrix that scales equally on each axis with a center point.
//    ///// </summary>
//    ///// <param name="scale">The uniform scaling factor.</param>
//    ///// <param name="centerPoint">The center point.</param>
//    ///// <returns>The scaling matrix.</returns>
//    //public static Matrix4<double> CreateScale(double scale, CoordinateSystems.CartesianCoordinate4 centerPoint)
//    //{
//    //  var tx = centerPoint.X * (1 - scale);
//    //  var ty = centerPoint.Y * (1 - scale);
//    //  var tz = centerPoint.Z * (1 - scale);

//    //  return new(scale, 0, 0, 0, 0, scale, 0, 0, 0, 0, scale, 0, tx, ty, tz, 1);
//    //}
//    ///// <summary>
//    ///// Creates a Matrix that flattens geometry into a specified Plane as if casting a shadow from a specified light source.
//    ///// </summary>
//    ///// <param name="lightDirection">The direction from which the light that will cast the shadow is coming.</param>
//    ///// <param name="plane">The Plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
//    ///// <returns>A new Matrix that can be used to flatten geometry onto the specified plane from the specified direction.</returns>
//    //public static Matrix4<double> CreateShadow(CoordinateSystems.CartesianCoordinate4 lightDirection, Plane plane)
//    //{
//    //  var p = Plane.Normalize(plane);

//    //  double dot = p.X * lightDirection.X + p.Y * lightDirection.Y + p.Z * lightDirection.Z;

//    //  double a = -p.X;
//    //  double b = -p.Y;
//    //  double c = -p.Z;
//    //  double d = -p.Distance;

//    //  return new
//    //  (
//    //    a * lightDirection.X + dot,
//    //    a * lightDirection.Y,
//    //    a * lightDirection.Z,
//    //    0,

//    //    b * lightDirection.X,
//    //    b * lightDirection.Y + dot,
//    //    b * lightDirection.Z,
//    //    0,

//    //    c * lightDirection.X,
//    //    c * lightDirection.Y,
//    //    c * lightDirection.Z + dot,
//    //    0,

//    //    d * lightDirection.X,
//    //    d * lightDirection.Y,
//    //    d * lightDirection.Z,
//    //    dot
//    //  );
//    //}
//    ///// <summary>
//    ///// Creates a translation matrix.
//    ///// </summary>
//    ///// <param name="position">The amount to translate in each axis.</param>
//    ///// <returns>The translation matrix.</returns>
//    //public static Matrix4<double> CreateTranslation(CoordinateSystems.CartesianCoordinate4 position)
//    //  => new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, position.X, position.Y, position.Z, 1);
//    /// <summary>
//    /// Creates a translation matrix.
//    /// </summary>
//    /// <param name="xPosition">The amount to translate on the X-axis.</param>
//    /// <param name="yPosition">The amount to translate on the Y-axis.</param>
//    /// <param name="zPosition">The amount to translate on the Z-axis.</param>
//    /// <returns>The translation matrix.</returns>
//    public static Matrix4 CreateTranslation(double xPosition, double yPosition, double zPosition)
//      => new(
//        1, 0, 0, 0,
//        0, 1, 0, 0,
//        0, 0, 1, 0,
//        xPosition, yPosition, zPosition, 1
//      );
//    ///// <summary>
//    ///// Creates a world matrix with the specified parameters.
//    ///// </summary>
//    ///// <param name="position">The position of the object, used in translation operations.</param>
//    ///// <param name="forward">Forward direction of the object.</param>
//    ///// <param name="up">Upward direction of the object, usually [0, 1, 0].</param>
//    ///// <returns>The world matrix.</returns>
//    //public static Matrix4<double> CreateWorld(CoordinateSystems.CartesianCoordinate4 position, CoordinateSystems.CartesianCoordinate4 forward, CoordinateSystems.CartesianCoordinate4 up)
//    //{
//    //  var zaxis = CoordinateSystems.CartesianCoordinate4.Normalize(-forward);
//    //  var xaxis = CoordinateSystems.CartesianCoordinate4.Normalize(CoordinateSystems.CartesianCoordinate4.Cross(up, zaxis));
//    //  var yaxis = CoordinateSystems.CartesianCoordinate4.Cross(zaxis, xaxis);

//    //  return new Matrix4(xaxis.X, xaxis.Y, xaxis.Z, 0, yaxis.X, yaxis.Y, yaxis.Z, 0, zaxis.X, zaxis.Y, zaxis.Z, 0, position.X, position.Y, position.Z, 1);
//    //}

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
//    #endregion Static methods

//    #region Operator overloads

//    /// <summary>
//    /// Returns a new matrix with the negated elements of the given matrix.
//    /// </summary>
//    /// <param name="value">The source matrix.</param>
//    /// <returns>The negated matrix.</returns>
//    public static Matrix4 operator -(Matrix4 value)
//      => new
//      (
//        -value.m_11, -value.m_12, -value.m_13, -value.m_14,
//        -value.m_21, -value.m_22, -value.m_23, -value.m_24,
//        -value.m_31, -value.m_32, -value.m_33, -value.m_34,
//        -value.m_41, -value.m_42, -value.m_43, -value.m_44
//      );

//    /// <summary>
//    /// Adds two matrices together.
//    /// </summary>
//    /// <param name="value1">The first source matrix.</param>
//    /// <param name="value2">The second source matrix.</param>
//    /// <returns>The resulting matrix.</returns>
//    public static Matrix4 operator +(Matrix4 value1, Matrix4 value2)
//      => new
//      (
//        value1.m_11 + value2.m_11, value1.m_12 + value2.m_12, value1.m_13 + value2.m_13, value1.m_14 + value2.m_14,
//        value1.m_21 + value2.m_21, value1.m_22 + value2.m_22, value1.m_23 + value2.m_23, value1.m_24 + value2.m_24,
//        value1.m_31 + value2.m_31, value1.m_32 + value2.m_32, value1.m_33 + value2.m_33, value1.m_34 + value2.m_34,
//        value1.m_41 + value2.m_41, value1.m_42 + value2.m_42, value1.m_43 + value2.m_43, value1.m_44 + value2.m_44
//      );

//    /// <summary>
//    /// Subtracts the second matrix from the first.
//    /// </summary>
//    /// <param name="value1">The first source matrix.</param>
//    /// <param name="value2">The second source matrix.</param>
//    /// <returns>The result of the subtraction.</returns>
//    public static Matrix4 operator -(Matrix4 value1, Matrix4 value2)
//      => new
//      (
//        value1.m_11 - value2.m_11, value1.m_12 - value2.m_12, value1.m_13 - value2.m_13, value1.m_14 - value2.m_14,
//        value1.m_21 - value2.m_21, value1.m_22 - value2.m_22, value1.m_23 - value2.m_23, value1.m_24 - value2.m_24,
//        value1.m_31 - value2.m_31, value1.m_32 - value2.m_32, value1.m_33 - value2.m_33, value1.m_34 - value2.m_34,
//        value1.m_41 - value2.m_41, value1.m_42 - value2.m_42, value1.m_43 - value2.m_43, value1.m_44 - value2.m_44
//      );

//    /// <summary>
//    /// Multiplies a matrix by another matrix.
//    /// </summary>
//    /// <param name="value1">The first source matrix.</param>
//    /// <param name="value2">The second source matrix.</param>
//    /// <returns>The result of the multiplication.</returns>
//    public static Matrix4 operator *(Matrix4 value1, Matrix4 value2)
//      => new
//      (
//        // First row
//        value1.m_11 * value2.m_11 + value1.m_12 * value2.m_21 + value1.m_13 * value2.m_31 + value1.m_14 * value2.m_41,
//        value1.m_11 * value2.m_12 + value1.m_12 * value2.m_22 + value1.m_13 * value2.m_32 + value1.m_14 * value2.m_42,
//        value1.m_11 * value2.m_13 + value1.m_12 * value2.m_23 + value1.m_13 * value2.m_33 + value1.m_14 * value2.m_43,
//        value1.m_11 * value2.m_14 + value1.m_12 * value2.m_24 + value1.m_13 * value2.m_34 + value1.m_14 * value2.m_44,
//        // Second row
//        value1.m_21 * value2.m_11 + value1.m_22 * value2.m_21 + value1.m_23 * value2.m_31 + value1.m_24 * value2.m_41,
//        value1.m_21 * value2.m_12 + value1.m_22 * value2.m_22 + value1.m_23 * value2.m_32 + value1.m_24 * value2.m_42,
//        value1.m_21 * value2.m_13 + value1.m_22 * value2.m_23 + value1.m_23 * value2.m_33 + value1.m_24 * value2.m_43,
//        value1.m_21 * value2.m_14 + value1.m_22 * value2.m_24 + value1.m_23 * value2.m_34 + value1.m_24 * value2.m_44,
//        // Third row
//        value1.m_31 * value2.m_11 + value1.m_32 * value2.m_21 + value1.m_33 * value2.m_31 + value1.m_34 * value2.m_41,
//        value1.m_31 * value2.m_12 + value1.m_32 * value2.m_22 + value1.m_33 * value2.m_32 + value1.m_34 * value2.m_42,
//        value1.m_31 * value2.m_13 + value1.m_32 * value2.m_23 + value1.m_33 * value2.m_33 + value1.m_34 * value2.m_43,
//        value1.m_31 * value2.m_14 + value1.m_32 * value2.m_24 + value1.m_33 * value2.m_34 + value1.m_34 * value2.m_44,
//        // Fourth row
//        value1.m_41 * value2.m_11 + value1.m_42 * value2.m_21 + value1.m_43 * value2.m_31 + value1.m_44 * value2.m_41,
//        value1.m_41 * value2.m_12 + value1.m_42 * value2.m_22 + value1.m_43 * value2.m_32 + value1.m_44 * value2.m_42,
//        value1.m_41 * value2.m_13 + value1.m_42 * value2.m_23 + value1.m_43 * value2.m_33 + value1.m_44 * value2.m_43,
//        value1.m_41 * value2.m_14 + value1.m_42 * value2.m_24 + value1.m_43 * value2.m_34 + value1.m_44 * value2.m_44
//      );

//    /// <summary>
//    /// Multiplies a matrix by a scalar value.
//    /// </summary>
//    /// <param name="value1">The source matrix.</param>
//    /// <param name="value2">The scaling factor.</param>
//    /// <returns>The scaled matrix.</returns>
//    public static Matrix4 operator *(Matrix4 value1, double value2)
//      => new
//      (
//        value1.m_11 * value2, value1.m_12 * value2, value1.m_13 * value2, value1.m_14 * value2,
//        value1.m_21 * value2, value1.m_22 * value2, value1.m_23 * value2, value1.m_24 * value2,
//        value1.m_31 * value2, value1.m_32 * value2, value1.m_33 * value2, value1.m_34 * value2,
//        value1.m_41 * value2, value1.m_42 * value2, value1.m_43 * value2, value1.m_44 * value2
//      );

//    #endregion Operator overloads
//  }
//}
//#endif