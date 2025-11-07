//namespace Flux
//{
//  public static partial class XtensionIFloatingPoint
//  {
//    public static TFloat Halfs<TFloat>()
//      where TFloat : System.Numerics.IFloatingPoint<TFloat>
//      => TFloat.One / (TFloat.One + TFloat.One);

//    extension<TFloat>(TFloat)
//      where TFloat : System.Numerics.IFloatingPoint<TFloat>
//    {
//      /// <summary>
//      /// <para>Represents 0.5, or zero-point-five.</para>
//      /// </summary>
//      public static TFloat Half => TFloat.One / (TFloat.One + TFloat.One);

//      public static TFloat Two => TFloat.One + TFloat.One;
//    }
//  }
//}
