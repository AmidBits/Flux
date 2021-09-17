namespace Flux.Model.Dynamics.ForceGenerators
{
  public class BoundaryBoxForce
    : IForceGenerator
  {
    public enum OutOfBoundsActionEnum
    {
      BounceBack,
      Unhindered,
      WrapAround
    }

    public OutOfBoundsActionEnum OutOfBoundsAction { get; set; } = OutOfBoundsActionEnum.Unhindered;

    public System.Numerics.Vector3 Low { get; set; } = new System.Numerics.Vector3(0, 1000, 0);
    public System.Numerics.Vector3 High { get; set; } = new System.Numerics.Vector3(1000, 0, 0);

    public void ApplyForce(RigidBody body)
    {
      if (body is null) throw new System.ArgumentNullException(nameof(body));

      var position = body.ObjectPosition;
      var linearVelocity = body.LinearVelocity;

      switch (OutOfBoundsAction)
      {
        case OutOfBoundsActionEnum.BounceBack:
          if (body.ObjectPosition.X < Low.X)
          {
            position.X = (Low.X + (Low.X - body.ObjectPosition.X));
            linearVelocity.X = System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
            System.Numerics.Quaternion.Inverse(body.ObjectRotation);
          }
          else if (body.ObjectPosition.X > High.X)
          {
            position.X = (High.X - (body.ObjectPosition.X - High.X));
            linearVelocity.X = -System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
            System.Numerics.Quaternion.Inverse(body.ObjectRotation);
          }
          if (body.ObjectPosition.Y < Low.Y)
          {
            position.Y = (Low.Y + (Low.Y - body.ObjectPosition.Y));
            linearVelocity.Y = System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
            System.Numerics.Quaternion.Inverse(body.ObjectRotation);
          }
          else if (body.ObjectPosition.Y > High.Y)
          {
            position.Y = (High.Y - (body.ObjectPosition.Y - High.Y));
            linearVelocity.Y = -System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
            System.Numerics.Quaternion.Inverse(body.ObjectRotation);
          }
          if (body.ObjectPosition.Z < Low.Z)
          {
            position.Z = (Low.Z + (Low.Z - body.ObjectPosition.Z));
            linearVelocity.Z = System.Math.Abs(body.LinearVelocity.Z * body.CoefficientOfRestitution);
            System.Numerics.Quaternion.Inverse(body.ObjectRotation);
          }
          else if (body.ObjectPosition.Z > High.Z)
          {
            position.Z = (High.Z - (body.ObjectPosition.Z - High.Z));
            linearVelocity.Z = -System.Math.Abs(body.LinearVelocity.Z * body.CoefficientOfRestitution);
            System.Numerics.Quaternion.Inverse(body.ObjectRotation);
          }
          break;
        case OutOfBoundsActionEnum.WrapAround:
          if (body.ObjectPosition.X < Low.X)
          {
            position.X = (High.X - (Low.X - body.ObjectPosition.X));
          }
          else if (body.ObjectPosition.X > High.X)
          {
            position.X = (Low.X + (body.ObjectPosition.X - High.X));
          }
          if (body.ObjectPosition.Y < Low.Y)
          {
            position.Y = (High.Y - (Low.Y - body.ObjectPosition.Y));
          }
          else if (body.ObjectPosition.Y > High.Y)
          {
            position.Y = (Low.Y + (body.ObjectPosition.Y - High.Y));
          }
          if (body.ObjectPosition.Z < Low.Z)
          {
            position.Z = (High.Z - (Low.Z - body.ObjectPosition.Z));
          }
          else if (body.ObjectPosition.Z > High.Z)
          {
            position.Z = (Low.Z + (body.ObjectPosition.Z - High.Z));
          }
          break;
        default:
          break;
      }

      //if (OutOfBoundsAction == OutOfBoundsActionEnum.BounceBack)
      //{
      //  if (body.Position.X < Boundary.Left)
      //  {
      //    body.Position.X = (float)(Boundary.Left + (Boundary.Left - body.Position.X));
      //    body.LinearVelocity.X = System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
      //    System.Numerics.Quaternion.Negate(body.Orientation);
      //    //body.AngularVelocity = -body.AngularVelocity * .5F;
      //    //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.X * 0.5F));
      //  }
      //  else if (body.Position.X > Boundary.Right)
      //  {
      //    body.Position.X = (float)(Boundary.Right - (body.Position.X - Boundary.Right));
      //    body.LinearVelocity.X = -System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
      //    System.Numerics.Quaternion.Negate(body.Orientation);
      //    //body.AngularVelocity = body.AngularVelocity * .5F;
      //    //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.X * 0.5F));
      //  }
      //  //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.X));

      //  if (body.Position.Y < Boundary.Top)
      //  {
      //    body.Position.Y = (float)(Boundary.Top + (Boundary.Top - body.Position.Y));
      //    body.LinearVelocity.Y = System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
      //    System.Numerics.Quaternion.Negate(body.Orientation);
      //    //body.AngularVelocity = -body.AngularVelocity * .5F;
      //    //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.Y * 0.5F));
      //  }
      //  else if (body.Position.Y > Boundary.Bottom)
      //  {
      //    body.Position.Y = (float)(Boundary.Bottom - (body.Position.Y - Boundary.Bottom));
      //    body.LinearVelocity.Y = -System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
      //    System.Numerics.Quaternion.Negate(body.Orientation);
      //    //body.AngularVelocity = body.AngularVelocity * .5F;
      //    //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.Y*0.5F));
      //  }
      //  //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.Y));
      //}
      //else if (OutOfBoundsAction == OutOfBoundsActionEnum.WrapAround)
      //{
      //  if (body.Position.X < Boundary.Left)
      //    body.Position.X = (float)(Boundary.Right - (Boundary.Left - body.Position.X));
      //  else if (body.Position.X > Boundary.Right)
      //    body.Position.X = (float)(Boundary.Left + (body.Position.X - Boundary.Right));

      //  if (body.Position.Y < Boundary.Top)
      //    body.Position.Y = (float)(Boundary.Bottom - (Boundary.Top - body.Position.Y));
      //  else if (body.Position.Y > Boundary.Bottom)
      //    body.Position.Y = (float)(Boundary.Top + (body.Position.Y - Boundary.Bottom));
      //}

      body.ObjectPosition = position;
      body.LinearVelocity = linearVelocity;
    }
  }
}
