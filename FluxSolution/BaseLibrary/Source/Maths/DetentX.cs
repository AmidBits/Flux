

using System.Linq;

namespace Flux
{
  public static partial class Maths
  {

    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Numerics.BigInteger DetentIntervalX(System.Numerics.BigInteger value, System.Numerics.BigInteger interval, System.Numerics.BigInteger distance)
        => (value / interval) * interval is var nearestInterval && System.Numerics.BigInteger.Abs(nearestInterval - value) < distance ? nearestInterval : value;
    
    /// <summary>Determines whether a value is within a specified positive distance of the position, and if so, snaps the value to the position.</summary>
    public static System.Numerics.BigInteger DetentPositionX(System.Numerics.BigInteger value, System.Numerics.BigInteger position, System.Numerics.BigInteger distance)
        => System.Numerics.BigInteger.Abs(position - value) > System.Numerics.BigInteger.Abs(distance) ? value : position;
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static System.Numerics.BigInteger DetentZeroX(System.Numerics.BigInteger value, System.Numerics.BigInteger distance)
      => value < -distance || value > distance ? value : 0;

    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Decimal DetentIntervalX(System.Decimal value, System.Decimal interval, System.Decimal distance)
        => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;
    
    /// <summary>Determines whether a value is within a specified positive distance of the position, and if so, snaps the value to the position.</summary>
    public static System.Decimal DetentPositionX(System.Decimal value, System.Decimal position, System.Decimal distance)
        => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static System.Decimal DetentZeroX(System.Decimal value, System.Decimal distance)
      => value < -distance || value > distance ? value : 0;

    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Double DetentIntervalX(System.Double value, System.Double interval, System.Double distance)
        => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;
    
    /// <summary>Determines whether a value is within a specified positive distance of the position, and if so, snaps the value to the position.</summary>
    public static System.Double DetentPositionX(System.Double value, System.Double position, System.Double distance)
        => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static System.Double DetentZeroX(System.Double value, System.Double distance)
      => value < -distance || value > distance ? value : 0;

    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Single DetentIntervalX(System.Single value, System.Single interval, System.Single distance)
        => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;
    
    /// <summary>Determines whether a value is within a specified positive distance of the position, and if so, snaps the value to the position.</summary>
    public static System.Single DetentPositionX(System.Single value, System.Single position, System.Single distance)
        => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static System.Single DetentZeroX(System.Single value, System.Single distance)
      => value < -distance || value > distance ? value : 0;

    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Int32 DetentIntervalX(System.Int32 value, System.Int32 interval, System.Int32 distance)
        => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;
    
    /// <summary>Determines whether a value is within a specified positive distance of the position, and if so, snaps the value to the position.</summary>
    public static System.Int32 DetentPositionX(System.Int32 value, System.Int32 position, System.Int32 distance)
        => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static System.Int32 DetentZeroX(System.Int32 value, System.Int32 distance)
      => value < -distance || value > distance ? value : 0;

    
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Int64 DetentIntervalX(System.Int64 value, System.Int64 interval, System.Int64 distance)
        => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;
    
    /// <summary>Determines whether a value is within a specified positive distance of the position, and if so, snaps the value to the position.</summary>
    public static System.Int64 DetentPositionX(System.Int64 value, System.Int64 position, System.Int64 distance)
        => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static System.Int64 DetentZeroX(System.Int64 value, System.Int64 distance)
      => value < -distance || value > distance ? value : 0;

    
  }
}
