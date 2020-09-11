//using Flux.Random;
//using System.Collections.Generic;
//using System.Linq;

//namespace Flux.Model.Dynamics.ForceGenerators.FlockBehaviors
//{
//  //public class Neighborhood
//  //  : RigidBody
//  //{
//  //  public System.Numerics.Vector2 AverageDifference = new System.Numerics.Vector2();
//  //  public System.Numerics.Vector2 AverageVelocity = new System.Numerics.Vector2();
//  //  public System.Numerics.Vector2 AveragePosition = new System.Numerics.Vector2();

//  //  public RigidBody2D ClosestNeighbor = null;
//  //  public float ClosestNeighborDistance = float.NaN;
//  //  public System.Numerics.Vector2 ClosestNeighborVector = default(System.Numerics.Vector2);

//  //  public bool Enabled { get; set; }

//  //  public System.Numerics.Vector2 PositionDifference = new System.Numerics.Vector2();
//  //  public System.Numerics.Vector2 PositionSum = new System.Numerics.Vector2();
//  //  public System.Numerics.Vector2 VelocitySum = new System.Numerics.Vector2();

//  //  public float PerimeterRadius = 21.5f;

//  //  public void FindNeighbors(RigidBody2D body, RigidBodies2D bodies)
//  //  {
//  //    if (!Enabled)
//  //      return;

//  //    Clear();

//  //    AverageVelocity = default(System.Numerics.Vector2);
//  //    AveragePosition = default(System.Numerics.Vector2);

//  //    ClosestNeighbor = null;
//  //    ClosestNeighborDistance = float.MaxValue;
//  //    ClosestNeighborVector = default(System.Numerics.Vector2);

//  //    foreach (RigidBody2D boid in bodies)
//  //    {
//  //      if (!boid.Equals(body))
//  //      {
//  //        System.Numerics.Vector2 point = boid.Position - body.Position;
//  //        float distance = point.Length();

//  //        if (distance < PerimeterRadius)
//  //        {
//  //          Add(boid);
//  //          PositionDifference -= point;
//  //          PositionSum += boid.Velocity;
//  //          VelocitySum += boid.Velocity;
//  //        }

//  //        if (distance < ClosestNeighborDistance)
//  //        {
//  //          ClosestNeighbor = boid;
//  //          ClosestNeighborDistance = distance;
//  //          ClosestNeighborVector = point;
//  //        }
//  //      }
//  //    }

//  //    if (Count == 0)
//  //      Add(ClosestNeighbor);

//  //    AveragePosition = PositionSum / Count;
//  //    AverageVelocity = VelocitySum / Count;
//  //  }
//  //}
//  public class FlockBehavior
//    : IForceGenerator
//  {
//    public bool Disabled { get; set; }

//    public float PerimeterRadius = 21.5f;

//    public float CohesionWeight { get; set; }

//    public void ApplyForce(RigidBody body, IEnumerable<RigidBody> bodies)
//    {
//      var neighbors = bodies;
//      var centroidOfNeighbors = Media.Geometry.Shapes.Polygon.SurfaceCentroid(neighbors.Select(rb => rb.Position));

//      // Flock cohesion (centering).
//      body.Force += ((centroidOfNeighbors - body.Position) * (CohesionWeight * body.Mass));
//      //body.Force += ((body.Neighborhood.AveragePosition - body.Position) * CohesionWeight);

//      // Flock separation (distancing).
//      body.Force += neighbors.AverageDifference * (SeparationWeight * body.Mass);
//    }
//  }

//  //public class FlockCentering
//  //  : IForceGenerator
//  //{
//  //  public bool Disabled { get; set; }

//  //  public float CohesionWeight { get; set; }

//  //  public FlockCentering(float cohesionWeight = 0.01f) { CohesionWeight = cohesionWeight; }

//  //  public void ApplyForce(RigidBody body, IEnumerable<RigidBody> bodies)
//  //  {
//  //    body.Force += ((bodies.Centroid - body.Position) * (CohesionWeight * body.Mass));
//  //    //body.Force += ((body.Neighborhood.AveragePosition - body.Position) * CohesionWeight);
//  //  }
//  //}

//  public class NeighborhoodDistancing
//    : IForceGenerator
//  {
//    public bool Disabled { get; set; }
//    public float SeparationWeight { get; set; }

//    public NeighborhoodDistancing(float separationWeight = 15f) { SeparationWeight = separationWeight; }

//    public void ApplyForce(RigidBody body, IEnumerable<RigidBody> bodies)
//    {
//      if (!Disabled)
//        body.Force += (body.Neighborhood.AverageDifference * (SeparationWeight * body.Mass));
//    }
//  }

//  public class NeighborhoodVelocityAlignment
//      : IForceGenerator
//  {
//    public float AlignmentWeight { get; set; }
//    public bool Disabled { get; set; }

//    public NeighborhoodVelocityAlignment(float alignmentWeigth = 0.00125f) { AlignmentWeight = alignmentWeigth; }

//    public void ApplyForce(RigidBody body, IEnumerable<RigidBody> bodies)
//    {
//      if (!Disabled)
//        body.Force += body.Neighborhood.AverageVelocity * AlignmentWeight;
//    }
//  }

//  public class PointOfInterest
//    : IForceGenerator
//  {
//    public bool Disabled { get; set; }
//    public System.Numerics.Vector3 POI { get; set; }
//    public float Ratio { get; set; }

//    public PointOfInterest(float ratio = 0.017f)
//    {
//      Ratio = ratio;
//    }

//    public void ApplyForce(RigidBody body, IEnumerable<RigidBody> bodies)
//    {
//      if (!Disabled)
//        body.Force += ((POI - body.Position) * (Ratio * body.Mass));
//    }
//  }

//  public class RandomWandering
//   : IForceGenerator
//  {
//    public bool Disabled { get; set; }
//    public float WanderingWeight { get; set; }

//    public RandomWandering(float wanderingWeight = 1f) { WanderingWeight = wanderingWeight; }

//    public void ApplyForce(RigidBody body, IEnumerable<RigidBody> bodies)
//    {
//      if (!Disabled)
//        body.Force += (WanderingWeight * body.Mass) * new System.Numerics.Vector3((float)NumberGenerator.Crypto.NextDouble() - 0.5f, (float)Flux.Random.NumberGenerator.Crypto.NextDouble() - 0.5f, (float)Flux.Random.NumberGenerator.Crypto.NextDouble() - 0.5f);
//    }
//  }
//}
