namespace Flux
{
  /// <summary>A structure encapsulating a 3D Plane.</summary>
  /// <see cref="https://github.com/mono/mono/blob/bd278dd00dd24b3e8c735a4220afa6cb3ba317ee/netcore/System.Private.CoreLib/shared/System/Numerics/Plane.cs"/>
  public interface IPlane<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    /// <summary>The normal vector X of the Plane.</summary>
    public TSelf X { get; init; }
    /// <summary>The normal vector Y of the Plane.</summary>
    public TSelf Y { get; init; }
    /// <summary>The normal vector Z of the Plane.</summary>
    public TSelf Z { get; init; }

    /// <summary>The distance of the Plane along its normal from the origin.</summary>
    public TSelf Distance { get; init; }
  }
}
