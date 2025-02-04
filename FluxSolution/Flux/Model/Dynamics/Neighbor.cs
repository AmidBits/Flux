//using System.Collections;
//using System.Linq;

//namespace Flux.Model.Dynamics.ForceGenerators
//{
//  public class Neighbor
//  {
//  }

//  public class Neighborhood
//    : RigidBodyList
//  {
//    public System.Numerics.Vector3 AverageDifference { get; set; }
//    public System.Numerics.Vector3 AverageLinearVelocity { get; set; }
//    public System.Numerics.Vector3 AveragePosition { get; set; }

//    public RigidBody ClosestNeighbor { get; set; }
//    public float ClosestNeighborDistance { get; set; } = float.NaN;
//    public System.Numerics.Vector3 ClosestNeighborVector { get; set; }

//    public bool Enabled { get; set; }

//    public System.Numerics.Vector3 PositionDifference { get; set; }
//    public System.Numerics.Vector3 PositionSum { get; set; }
//    public System.Numerics.Vector3 VelocitySum { get; set; }

//    public float PerimeterRadius { get; set; } = 21.5f;

//    public void FindNeighbors(RigidBody body, RigidBodyList bodies)
//    {
//      if (!Enabled)
//        return;

//      Clear();

//      AverageLinearVelocity = default;
//      AveragePosition = default;

//      ClosestNeighbor = null;
//      ClosestNeighborDistance = float.MaxValue;
//      ClosestNeighborVector = default;

//      foreach (RigidBody boid in bodies)
//      {
//        if (!boid.Equals(body))
//        {
//          var point = boid.Position - body.Position;
//          var distance = point.Length();

//          if (distance < PerimeterRadius)
//          {
//            Add(boid);

//            PositionDifference -= point;
//            PositionSum += boid.Position;
//            VelocitySum += boid.LinearVelocity;
//          }

//          if (distance < ClosestNeighborDistance)
//          {
//            ClosestNeighbor = boid;
//            ClosestNeighborDistance = distance;
//            ClosestNeighborVector = point;
//          }
//        }
//      }

//      if (Count == 0)
//        Add(ClosestNeighbor);

//      AveragePosition = PositionSum / Count;
//      AverageLinearVelocity = VelocitySum / Count;
//    }

//    //public static Neighborhood Parse(System.Collections.Generic.IEnumerable<RigidBody> bodies)
//    //{

//    //}
//  }
//}
