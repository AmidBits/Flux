using System.Linq;

//namespace Flux.Model.Dynamics.ForceGenerators
//{
//  public interface IForceGenerator
//  {
//    bool Disabled { get; set; }

//    void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies);
//  }

//  public class ForceGenerator : IForceGenerator
//  {
//    public virtual bool Disabled { get; set; }

//    public virtual void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies)
//    {
//    }
//  }

//  public class BoundaryBoxForce : ForceGenerator
//  {
//    public enum OutOfBoundsActionEnum
//    {
//      BounceBack,
//      Unhindered,
//      WrapAround
//    }

//    public OutOfBoundsActionEnum OutOfBoundsAction { get; set; } = OutOfBoundsActionEnum.Unhindered;

//    public System.Numerics.Vector3 Low { get; set; } = new System.Numerics.Vector3(0, 1000, 0);
//    public System.Numerics.Vector3 High { get; set; } = new System.Numerics.Vector3(1000, 0, 0);

//    public override void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies)
//    {
//      if (body is null) throw new System.ArgumentNullException(nameof(body));

//      switch (OutOfBoundsAction)
//      {
//        case OutOfBoundsActionEnum.BounceBack:
//          if (body.Position.X < Low.X)
//          {
//            body.Position.X = (Low.X + (Low.X - body.Position.X));
//            body.LinearVelocity.X = System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
//            System.Numerics.Quaternion.Inverse(body.Orientation);
//          }
//          else if (body.Position.X > High.X)
//          {
//            body.Position.X = (High.X - (body.Position.X - High.X));
//            body.LinearVelocity.X = -System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
//            System.Numerics.Quaternion.Inverse(body.Orientation);
//          }
//          if (body.Position.Y < Low.Y)
//          {
//            body.Position.Y = (Low.Y + (Low.Y - body.Position.Y));
//            body.LinearVelocity.Y = System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
//            System.Numerics.Quaternion.Inverse(body.Orientation);
//          }
//          else if (body.Position.Y > High.Y)
//          {
//            body.Position.Y = (High.Y - (body.Position.Y - High.Y));
//            body.LinearVelocity.Y = -System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
//            System.Numerics.Quaternion.Inverse(body.Orientation);
//          }
//          if (body.Position.Z < Low.Z)
//          {
//            body.Position.Z = (Low.Z + (Low.Z - body.Position.Z));
//            body.LinearVelocity.Z = System.Math.Abs(body.LinearVelocity.Z * body.CoefficientOfRestitution);
//            System.Numerics.Quaternion.Inverse(body.Orientation);
//          }
//          else if (body.Position.Z > High.Z)
//          {
//            body.Position.Z = (High.Z - (body.Position.Z - High.Z));
//            body.LinearVelocity.Z = -System.Math.Abs(body.LinearVelocity.Z * body.CoefficientOfRestitution);
//            System.Numerics.Quaternion.Inverse(body.Orientation);
//          }
//          break;
//        case OutOfBoundsActionEnum.WrapAround:
//          if (body.Position.X < Low.X)
//          {
//            body.Position.X = (High.X - (Low.X - body.Position.X));
//          }
//          else if (body.Position.X > High.X)
//          {
//            body.Position.X = (Low.X + (body.Position.X - High.X));
//          }
//          if (body.Position.Y < Low.Y)
//          {
//            body.Position.Y = (High.Y - (Low.Y - body.Position.Y));
//          }
//          else if (body.Position.Y > High.Y)
//          {
//            body.Position.Y = (Low.Y + (body.Position.Y - High.Y));
//          }
//          if (body.Position.Z < Low.Z)
//          {
//            body.Position.Z = (High.Z - (Low.Z - body.Position.Z));
//          }
//          else if (body.Position.Z > High.Z)
//          {
//            body.Position.Z = (Low.Z + (body.Position.Z - High.Z));
//          }
//          break;
//        default:
//          break;
//      }

//      //if (OutOfBoundsAction == OutOfBoundsActionEnum.BounceBack)
//      //{
//      //  if (body.Position.X < Boundary.Left)
//      //  {
//      //    body.Position.X = (float)(Boundary.Left + (Boundary.Left - body.Position.X));
//      //    body.LinearVelocity.X = System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
//      //    System.Numerics.Quaternion.Negate(body.Orientation);
//      //    //body.AngularVelocity = -body.AngularVelocity * .5F;
//      //    //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.X * 0.5F));
//      //  }
//      //  else if (body.Position.X > Boundary.Right)
//      //  {
//      //    body.Position.X = (float)(Boundary.Right - (body.Position.X - Boundary.Right));
//      //    body.LinearVelocity.X = -System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
//      //    System.Numerics.Quaternion.Negate(body.Orientation);
//      //    //body.AngularVelocity = body.AngularVelocity * .5F;
//      //    //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.X * 0.5F));
//      //  }
//      //  //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.X));

//      //  if (body.Position.Y < Boundary.Top)
//      //  {
//      //    body.Position.Y = (float)(Boundary.Top + (Boundary.Top - body.Position.Y));
//      //    body.LinearVelocity.Y = System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
//      //    System.Numerics.Quaternion.Negate(body.Orientation);
//      //    //body.AngularVelocity = -body.AngularVelocity * .5F;
//      //    //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.Y * 0.5F));
//      //  }
//      //  else if (body.Position.Y > Boundary.Bottom)
//      //  {
//      //    body.Position.Y = (float)(Boundary.Bottom - (body.Position.Y - Boundary.Bottom));
//      //    body.LinearVelocity.Y = -System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
//      //    System.Numerics.Quaternion.Negate(body.Orientation);
//      //    //body.AngularVelocity = body.AngularVelocity * .5F;
//      //    //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.Y*0.5F));
//      //  }
//      //  //body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.Y));
//      //}
//      //else if (OutOfBoundsAction == OutOfBoundsActionEnum.WrapAround)
//      //{
//      //  if (body.Position.X < Boundary.Left)
//      //    body.Position.X = (float)(Boundary.Right - (Boundary.Left - body.Position.X));
//      //  else if (body.Position.X > Boundary.Right)
//      //    body.Position.X = (float)(Boundary.Left + (body.Position.X - Boundary.Right));

//      //  if (body.Position.Y < Boundary.Top)
//      //    body.Position.Y = (float)(Boundary.Bottom - (Boundary.Top - body.Position.Y));
//      //  else if (body.Position.Y > Boundary.Bottom)
//      //    body.Position.Y = (float)(Boundary.Top + (body.Position.Y - Boundary.Bottom));
//      //}
//    }
//  }

//  public class DragForce : ForceGenerator
//  {
//    public const float DensityOfAir = 1.29F;
//    public const float DensityOfWater = 1.94F;

//    public float DragDensity { get; set; } = DensityOfAir;

//    public override void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies)
//    {
//      if (body is null) throw new System.ArgumentNullException(nameof(body));

//      var dragForce = 0.5f * DragDensity * (float)System.Math.Pow(body.LinearVelocity.Length(), 2) * body.CoefficientOfDrag;

//      body.ApplyForce(System.Numerics.Vector3.Zero, System.Numerics.Vector3.Negate(body.LinearVelocity * dragForce));
//    }
//  }

//  /// <summary></summary>
//  /// <seealso cref="https://msdn.microsoft.com/en-us/library/microsoft.xna.framework.vector3.dot.aspx"/>
//  public class GravityForce : ForceGenerator
//  {
//    public const float GravityOnEarth = 9.780F;
//    public const float GravityOnMars = 3.711F;
//    public const float GravityOnTheMoon = 1.622F;

//    public System.Numerics.Vector3 GravitationalPull { get; set; } = new System.Numerics.Vector3(0, GravityOnEarth, 0);

//    public override void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies)
//    {
//      if (body is null) throw new System.ArgumentNullException(nameof(body));

//      // Dot product: >0 = <90, <0 = >90, =0 = 90, =1 = parallel same dir, =-1 = parallel opposite dir.
//      // if the gravitational pull and the body position is within 90 degrees then apply gravity.
//      //        if (System.Numerics.Vector3.Dot(System.Numerics.Vector3.Normalize(GravitationalPull), System.Numerics.Vector3.Normalize(body.Position)) > 0)
//      {
//        body.ApplyForce(System.Numerics.Vector3.Zero, GravitationalPull);
//      }
//    }
//  }

//  public class LambdaForce : ForceGenerator
//  {
//    public System.Func<RigidBody, System.Collections.Generic.IEnumerable<RigidBody>, System.Numerics.Vector3>? ForcePosition { get; set; }
//    public System.Func<RigidBody, System.Collections.Generic.IEnumerable<RigidBody>, System.Numerics.Vector3>? DirectionMagnitude { get; set; }

//    public override void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies)
//      => (body ?? throw new System.ArgumentNullException(nameof(body))).ApplyForce(ForcePosition?.Invoke(body, bodies) ?? System.Numerics.Vector3.Zero, DirectionMagnitude?.Invoke(body, bodies) ?? System.Numerics.Vector3.Zero);
//  }
//}

//namespace Flux.Model.Dynamics
//{
//  // http://www.enchantedage.com/node/68
//  public class RigidBody
//  {
//    public System.Collections.Generic.List<ForceGenerators.ForceGenerator> ForceGenerators { get; } = new System.Collections.Generic.List<ForceGenerators.ForceGenerator>();

//    public float CoefficientOfDrag { get; set; } = 0.35F;
//    public float CoefficientOfRestitution { get; set; } = 0.95F;

//    public float Mass { get; set; } = 7F;

//    public System.Numerics.Vector3 LinearVelocity;
//    public System.Numerics.Vector3 Force { get; set; }

//    public System.Numerics.Vector3 AngularVelocity { get; set; }
//    public System.Numerics.Vector3 Torque { get; set; }

//    public System.Numerics.Vector3 Position;

//    public System.Numerics.Quaternion Orientation { get; set; } = System.Numerics.Quaternion.Identity;

//    public System.Numerics.Vector3 Volume { get; set; } = new System.Numerics.Vector3(0.5F, 2F, 0.25F);

//    public void Integrate(float deltaTime)
//    {
//      foreach (var forceGenerator in ForceGenerators.Where(fg => !fg.Disabled))
//      {
//        forceGenerator.ApplyForce(this, System.Linq.Enumerable.Empty<RigidBody>());
//      }

//      var linearAcceleration = Force / Mass;
//      LinearVelocity += linearAcceleration * deltaTime;
//      Force = System.Numerics.Vector3.Zero;

//      var angularAcceleration = Torque / Mass;
//      AngularVelocity += angularAcceleration * deltaTime;
//      Torque = System.Numerics.Vector3.Zero;

//      Position += LinearVelocity * deltaTime;

//      Orientation += new System.Numerics.Quaternion((AngularVelocity * deltaTime), 0) * Orientation;
//      System.Numerics.Quaternion.Normalize(Orientation);
//    }

//    public void ApplyForce(System.Numerics.Vector3 forcePosition, System.Numerics.Vector3 directionMagnitude)
//    {
//      if (directionMagnitude.LengthSquared() is float lengthSquared && lengthSquared > float.Epsilon * 100)
//      {
//        Force += directionMagnitude;

//        if (forcePosition != default)
//        {
//          Torque += System.Numerics.Vector3.Cross(directionMagnitude, forcePosition - Position);
//        }
//      }
//    }

//    public System.Numerics.Vector3 PointVelocity(System.Numerics.Vector3 worldPoint) => System.Numerics.Vector3.Cross(AngularVelocity, worldPoint - Position) + LinearVelocity;

//    public override string ToString() => $"P:{Position}, O:{Orientation}, LV:{LinearVelocity}, AV:{AngularVelocity}";
//  }
//}

//namespace Flux.Model.Dynamics
//{
//	public interface IForceGenerator
//	{
//		void ComputeForce(RigidBody body);
//	}

//	#region ForceBoundary2D : IForceGenerator
//	//public class ForceBoundary2D : IForceGenerator
//	//{
//	//	public enum OutOfBoundsActionEnum
//	//	{
//	//		BounceBack,
//	//		Unhindered,
//	//		WrapAround
//	//	}

//	//	public Windows.Foundation.Rect Boundary = new Windows.Foundation.Rect(0, 0, 100, 100);

//	//	public OutOfBoundsActionEnum OutOfBoundsAction = OutOfBoundsActionEnum.Unhindered;

//	//	public void ComputeForce(RigidBody body)
//	//	{
//	//		if (OutOfBoundsAction == OutOfBoundsActionEnum.BounceBack)
//	//		{
//	//			if (body.Position.X < Boundary.Left)
//	//			{
//	//				body.Position.X = (float)(Boundary.Left + (Boundary.Left - body.Position.X));
//	//				body.LinearVelocity.X = System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
//	//				System.Numerics.Quaternion.Negate(body.Orientation);
//	//				//body.AngularVelocity = -body.AngularVelocity * .5F;
//	//				//body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.X * 0.5F));
//	//			}
//	//			else if (body.Position.X > Boundary.Right)
//	//			{
//	//				body.Position.X = (float)(Boundary.Right - (body.Position.X - Boundary.Right));
//	//				body.LinearVelocity.X = -System.Math.Abs(body.LinearVelocity.X * body.CoefficientOfRestitution);
//	//				System.Numerics.Quaternion.Negate(body.Orientation);
//	//				//body.AngularVelocity = body.AngularVelocity * .5F;
//	//				//body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.X * 0.5F));
//	//			}
//	//			//body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.X));

//	//			if (body.Position.Y < Boundary.Top)
//	//			{
//	//				body.Position.Y = (float)(Boundary.Top + (Boundary.Top - body.Position.Y));
//	//				body.LinearVelocity.Y = System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
//	//				System.Numerics.Quaternion.Negate(body.Orientation);
//	//				//body.AngularVelocity = -body.AngularVelocity * .5F;
//	//				//body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.Y * 0.5F));
//	//			}
//	//			else if (body.Position.Y > Boundary.Bottom)
//	//			{
//	//				body.Position.Y = (float)(Boundary.Bottom - (body.Position.Y - Boundary.Bottom));
//	//				body.LinearVelocity.Y = -System.Math.Abs(body.LinearVelocity.Y * body.CoefficientOfRestitution);
//	//				System.Numerics.Quaternion.Negate(body.Orientation);
//	//				//body.AngularVelocity = body.AngularVelocity * .5F;
//	//				//body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.Y*0.5F));
//	//			}
//	//			//body.ApplyForce(System.Numerics.Vector3.Zero, new System.Numerics.Vector3(0, 0, body.LinearVelocity.Y));
//	//		}
//	//		else if (OutOfBoundsAction == OutOfBoundsActionEnum.WrapAround)
//	//		{
//	//			if (body.Position.X < Boundary.Left)
//	//				body.Position.X = (float)(Boundary.Right - (Boundary.Left - body.Position.X));
//	//			else if (body.Position.X > Boundary.Right)
//	//				body.Position.X = (float)(Boundary.Left + (body.Position.X - Boundary.Right));

//	//			if (body.Position.Y < Boundary.Top)
//	//				body.Position.Y = (float)(Boundary.Bottom - (Boundary.Top - body.Position.Y));
//	//			else if (body.Position.Y > Boundary.Bottom)
//	//				body.Position.Y = (float)(Boundary.Top + (body.Position.Y - Boundary.Bottom));
//	//		}
//	//	}
//	//}
//	#endregion

//	#region ForceDrag : IForceGenerator
//	public class ForceDrag : IForceGenerator
//	{
//		public const float DensityOfAir = 1.29f;
//		public const float DensityOfWater = 1.94f;

//		public float DragDensity = DensityOfAir;

//		public void ComputeForce(RigidBody body)
//		{
//			float dragForce = 0.5f * body.CoefficientOfDrag * DragDensity;
//			//float dragForce = 0.5f * body.CoefficientOfDrag * DragDensity * (float)System.Math.Pow(body.LinearVelocity.Length(), 2);

//			body.ApplyForce(body.Position, System.Numerics.Vector3.Negate(body.LinearVelocity * dragForce));
//		}
//	}
//	#endregion

//	#region ForceGravity : IForceGenerator
//	public class ForceGravity : IForceGenerator
//	{
//		public const float EarthGravity = 9.780f;
//		public const float MarsGravity = 3.711f;
//		public const float MoonGravity = 1.622f;

//		public System.Numerics.Vector3 GravitationalPull = new System.Numerics.Vector3(0F, EarthGravity, 0F);

//		public void ComputeForce(RigidBody body)
//		{
//			var oneOverMass = (1 / body.Mass);

//			body.ApplyForce(body.Position, GravitationalPull / oneOverMass);
//		}
//	}
//	#endregion

//	public class RigidBody
//	{
//		public System.Collections.Generic.List<IForceGenerator> ForceGenerators = new System.Collections.Generic.List<IForceGenerator>();

//		public float CoefficientOfRestitution = 0.95F;
//		public float CoefficientOfDrag = 0.35F;

//		private float _inverseAngularMass = 1;
//		public float AngularMass
//		{
//			get { return 1F / _inverseAngularMass; }
//			set { _inverseAngularMass = 1F / value; }
//		}

//		private float _inverseMass = 1;
//		public float Mass
//		{
//			get { return 1F / _inverseMass; }
//			set { _inverseMass = 1F / value; }
//		}

//		public System.Numerics.Vector3 Position = System.Numerics.Vector3.Zero;
//		public System.Numerics.Vector3 LinearVelocity = System.Numerics.Vector3.Zero;
//		public System.Numerics.Vector3 Force = System.Numerics.Vector3.Zero;

//		public System.Numerics.Quaternion Orientation = System.Numerics.Quaternion.Identity;
//		public System.Numerics.Vector3 AngularVelocity = System.Numerics.Vector3.Zero;
//		public System.Numerics.Vector3 Torque = System.Numerics.Vector3.Zero;

//		public float PhysicalScale = 10F;

//		/// <summary>Applies a force to Force/Torque via ForcePosition and ForceDirectionAndMagnitude.</summary>
//		public void ApplyForce(System.Numerics.Vector3 forcePosition, System.Numerics.Vector3 forceDirectionAndMagnitude)
//		{
//			if(forceDirectionAndMagnitude == System.Numerics.Vector3.Zero || forceDirectionAndMagnitude.LengthSquared() < float.Epsilon)
//				return;

//			Force += forceDirectionAndMagnitude;

//			System.Numerics.Vector3 distance = (forcePosition - Position);
//			Torque += System.Numerics.Vector3.Cross(forceDirectionAndMagnitude, distance);
//		}

//		/// <summary>The integration.</summary>
//		/// <see cref="http://lolengine.net/blog/2011/12/14/understanding-motion-in-games"/>
//		public void Integrate(float deltaTime)
//		{
//			// Compute force and torque from force generators.
//			foreach(var fg in ForceGenerators)
//				fg.ComputeForce(this);

//			// Force >> Linear Velocity >> Position
//			var oldLinearVelocity = LinearVelocity;
//			LinearVelocity += (Force * _inverseMass * deltaTime);
//			Position += ((oldLinearVelocity + LinearVelocity) * 0.5F * deltaTime);

//			// Torque >> Angular Velocity >> Orientation.
//			var oldAngularVelocity = AngularVelocity;
//			AngularVelocity += (Torque * _inverseAngularMass * deltaTime);
//			//Orientation = System.Numerics.Quaternion.CreateFromYawPitchRoll(AngularVelocity.X, AngularVelocity.Y, AngularVelocity.Z) * Orientation;

//			//OLD for trouble shooting 
//			var newAngularVelocity = (oldAngularVelocity + AngularVelocity) * 0.5F * deltaTime;
//			Orientation = System.Numerics.Quaternion.CreateFromYawPitchRoll(newAngularVelocity.X, newAngularVelocity.Y, newAngularVelocity.Z) * Orientation;

//			System.Numerics.Quaternion.Normalize(Orientation);

//			Force = System.Numerics.Vector3.Zero;
//			Torque = System.Numerics.Vector3.Zero;
//		}

//		public override string ToString()
//		{
//			return base.ToString();// + "(P:" + Position.ToString("F3") + ", O:" + Orientation.GetEulerAngles().ToStringF3() + ")";
//		}
//	}
//}

//public interface IForceGenerator2D
//{
//	void ComputeForce(RigidBody2D body, RigidBodies2D bodies);
//}

//[System.Runtime.InteropServices.ComVisible(false)]
//public class ForceGenerators2D
//	: System.Collections.Generic.List<IForceGenerator2D>
//{
//	public virtual void ComputeForces(RigidBody2D body, RigidBodies2D bodies)
//	{
//		foreach (IForceGenerator2D fg in this)
//			fg.ComputeForce(body, bodies);
//	}

//	#region "IForceGenerator2D Presets (Boundary, Drag, Gravity and 5 SteeringBehaviors)"
//	public class Boundary : IForceGenerator2D
//	{
//		public enum OutOfBoundsAction
//		{
//			BounceBack,
//			Unhindered,
//			WrapAround
//		}

//		public OutOfBoundsAction Action { get; set; }

//		public System.Numerics.Vector2 Location { get; set; }
//		public System.Numerics.Vector2 Size { get; set; }

//		public Boundary()
//			: this(OutOfBoundsAction.Unhindered, System.Numerics.Vector2.Zero, System.Numerics.Vector2.Zero)
//		{
//		}
//		public Boundary(OutOfBoundsAction action, System.Numerics.Vector2 location, System.Numerics.Vector2 size)
//		{
//			Action = action;

//			Location = location;
//			Size = size;
//		}

//		public void ComputeForce(RigidBody2D body, RigidBodies2D bodies)
//		{
//			if (Action == OutOfBoundsAction.BounceBack)
//			{
//				float left = Location.X, right = Location.X + Size.X, top = Location.Y, bottom = Location.Y + Size.Y;

//				if (body.Position.X < left)
//				{
//					body.Position.X = left + (left - body.Position.X);
//					body.Velocity.X = System.Math.Abs(body.Velocity.X * body.CoefficientOfRestitution);
//				}
//				else if (body.Position.X > right)
//				{
//					body.Position.X = right - (body.Position.X - right);
//					body.Velocity.X = -System.Math.Abs(body.Velocity.X * body.CoefficientOfRestitution);
//				}

//				if (body.Position.Y < top)
//				{
//					body.Position.Y = top + (top - body.Position.Y);
//					body.Velocity.Y = System.Math.Abs(body.Velocity.Y * body.CoefficientOfRestitution);
//				}
//				else if (body.Position.Y > bottom)
//				{
//					body.Position.Y = bottom - (body.Position.Y - bottom);
//					body.Velocity.Y = -System.Math.Abs(body.Velocity.Y * body.CoefficientOfRestitution);
//				}
//			}
//			else if (Action == OutOfBoundsAction.WrapAround)
//			{
//				float left = Location.X, right = Location.X + Size.X, top = Location.Y, bottom = Location.Y + Size.Y;

//				if (body.Position.X < left)
//					body.Position.X = right - (left - body.Position.X);
//				else if (body.Position.X > right)
//					body.Position.X = left + (body.Position.X - right);

//				if (body.Position.Y < top)
//					body.Position.Y = bottom - (top - body.Position.Y);
//				else if (body.Position.Y > bottom)
//					body.Position.Y = top + (body.Position.Y - bottom);
//			}
//		}
//	}

//	public class Drag : IForceGenerator2D
//	{
//		public const float DensityOfAir = 1.29f;
//		public const float DensityOfWater = 1.94f;

//		public float DragDensity = DensityOfAir;

//		public void ComputeForce(RigidBody2D body, RigidBodies2D bodies)
//		{
//			float commonDrag = 0.5f * body.CoefficientOfDrag * DragDensity * body.AverageArea;

//			float vMagnitude = body.Velocity.Length();
//			float avMagnitude = System.Math.Abs(body.AngularVelocity);

//			body.Force += (body.Velocity * (commonDrag * -(vMagnitude + avMagnitude)));

//			body.Torque += (body.AngularVelocity * commonDrag * -System.Math.Max(avMagnitude, (avMagnitude + vMagnitude) * 0.67f));
//		}
//	}

//	public class Gravity : IForceGenerator2D
//	{
//		public const float EarthGravity = 9.780f;
//		public const float MarsGravity = 3.711f;
//		public const float MoonGravity = 1.622f;

//		public System.Numerics.Vector2 GravitationalPull;

//		public Gravity() : this(new System.Numerics.Vector2(0f, EarthGravity)) { }
//		public Gravity(System.Numerics.Vector2 gravitationalPull) { GravitationalPull = gravitationalPull; }

//		public void ComputeForce(RigidBody2D body, RigidBodies2D bodies)
//		{
//			body.Force += (GravitationalPull / body.OneOverMass);
//		}
//	}

//	public static class SteeringBehaviors
//	{
//		public class FlockCentering : IForceGenerator2D
//		{
//			public float CohesionWeight { get; set; }

//			public FlockCentering(float cohesionWeight = 0.01f) { CohesionWeight = cohesionWeight; }

//			public void ComputeForce(RigidBody2D body, RigidBodies2D bodies)
//			{
//				body.Force += ((bodies.Centroid - body.Position) * (CohesionWeight * body.Mass));
//				//body.Force += ((body.Neighborhood.AveragePosition - body.Position) * CohesionWeight);
//			}
//		}

//		public class NeighborhoodDistancing : IForceGenerator2D
//		{
//			public float SeparationWeight { get; set; }

//			public NeighborhoodDistancing(float separationWeight = 15f) { SeparationWeight = separationWeight; }

//			public void ComputeForce(RigidBody2D body, RigidBodies2D bodies)
//			{
//				body.Force += (body.Neighborhood.AverageDifference * (SeparationWeight * body.Mass));
//			}
//		}

//		public class NeighborhoodVelocityAlignment : IForceGenerator2D
//		{
//			public float AlignmentWeight { get; set; }

//			public NeighborhoodVelocityAlignment(float alignmentWeigth = 0.00125f) { AlignmentWeight = alignmentWeigth; }

//			public void ComputeForce(RigidBody2D body, RigidBodies2D bodies)
//			{
//				body.Force += (body.Neighborhood.AverageVelocity * AlignmentWeight);
//			}
//		}

//		public class PointOfInterest : IForceGenerator2D
//		{
//			public bool Enabled { get; set; }
//			public System.Numerics.Vector2 POI { get; set; }
//			public float Ratio { get; set; }

//			public PointOfInterest(float ratio = 0.017f)
//			{
//				Enabled = false;
//				Ratio = ratio;
//				POI = new System.Numerics.Vector2();
//			}

//			public void ComputeForce(RigidBody2D body, RigidBodies2D bodies)
//			{
//				if (Enabled)
//				{
//					body.Force += ((POI - body.Position) * (Ratio * body.Mass));
//				}
//			}
//		}

//		public class RandomWandering : IForceGenerator2D
//		{
//			public float WanderingWeight { get; set; }

//			public RandomWandering(float wanderingWeight = 1f) { WanderingWeight = wanderingWeight; }

//			public void ComputeForce(RigidBody2D body, RigidBodies2D bodies)
//			{
//				body.Force += (new System.Numerics.Vector2((float)Math.Random.NextDouble() - 0.5f, (float)Math.Random.NextDouble() - 0.5f) * (WanderingWeight * body.Mass));
//			}
//		}
//	}
//	#endregion
//}

//#region "RigidBody2D/RigidBodies2D"
//public class RigidBody2D
//{
//	public float AverageArea { get { return _volume / 3f; } }

//	private float _boundingRadius = 1f;
//	public float BoundingRadius { get { return _boundingRadius; } set { _boundingRadius = value; ReProcess(); } }

//	private float _coefficientOfDrag = 0.35f;
//	public float CoefficientOfDrag { get { return _coefficientOfDrag; } set { _coefficientOfDrag = value; ReProcess(); } }

//	private float _coefficientOfRestitution = 0.95f;
//	public float CoefficientOfRestitution { get { return _coefficientOfRestitution; } set { _coefficientOfRestitution = value; ReProcess(); } }

//	private float _density = 1f;
//	public float Density { get { return _density; } set { _density = value; ReProcess(); } }

//	private System.Numerics.Vector3 _dimension = System.Numerics.Vector3.One;
//	public System.Numerics.Vector3 Dimension { get { return _dimension; } set { _dimension = value; ReProcess(); } }

//	public float _mass = 1f;
//	public float Mass { get { return _mass; } }

//	private float _momentOfInertia = 1f;
//	public float MomentOfInertia { get { return _momentOfInertia; } }

//	private float _oneOverMass = 1f;
//	public float OneOverMass { get { return _oneOverMass; } }

//	private float _oneOverMomentOfInertia;
//	public float OneOverMomentOfInertia { get { return _oneOverMomentOfInertia; } }

//	//private double _roughAreaOfResistance;
//	//public double RoughAreaOfResistance { get { return _roughAreaOfResistance; } }

//	private float _volume = 1f;
//	public float Volume { get { return _volume; } }

//	/// <summary>Represents the position of the body.</summary>
//	public System.Numerics.Vector2 Position = new System.Numerics.Vector2();
//	/// <summary>Represents the orientation of the body.</summary>
//	public float Orientation = 0f;

//	/// <summary>Represents the velocity of the body.</summary>
//	public System.Numerics.Vector2 Velocity = System.Numerics.Vector2.Zero;
//	/// <summary>Represents the angular velocity of the body.</summary>
//	public float AngularVelocity = 0f;

//	/// <summary>Represents the force effect on the body.</summary>
//	public System.Numerics.Vector2 Force = System.Numerics.Vector2.Zero;
//	/// <summary>Represents the torque effect on the body.</summary>
//	public float Torque = 0f;

//	public ForceGenerators2D ForceGenerators = new ForceGenerators2D();

//	public Neighborhood2D Neighborhood = new Neighborhood2D();

//	public RigidBody2D()
//	{
//		ReProcess();

//		Position = new System.Numerics.Vector2();
//		Orientation = 0f;

//		Velocity = new System.Numerics.Vector2();
//		AngularVelocity = 0f;

//		Force = new System.Numerics.Vector2();
//		Torque = 0f;
//	}

//	private void ReProcess()
//	{
//		_volume = _dimension.X * _dimension.Y * _dimension.Z;

//		_mass = _volume * _density;

//		_momentOfInertia = (_mass * (_dimension.X * _dimension.X + _dimension.Y * _dimension.Y)) / 12f;

//		_oneOverMass = 1f / _mass;

//		_oneOverMomentOfInertia = 1f / _momentOfInertia;
//	}

//	public virtual void ComputeForces(ForceGenerators2D forceGenerators, RigidBodies2D bodies)
//	{
//		forceGenerators.ComputeForces(this, bodies);

//		ForceGenerators.ComputeForces(this, bodies);
//	}

//	public virtual void Integrate(float deltaTime)
//	{
//		Velocity += (Force * (deltaTime * OneOverMass));

//		AngularVelocity = AngularVelocity + (deltaTime * OneOverMomentOfInertia) * Torque;

//		Position += (Velocity * deltaTime);

//		Orientation = (Orientation + deltaTime * AngularVelocity);// % 360d;

//		Force = new System.Numerics.Vector2();
//		Torque = 0f;
//	}

//	public override string ToString()
//	{
//		return string.Format("{0:F3}, {1:F3} | {2:F3} | {3:F3}, {4:F3}", Position.X, Position.Y, Orientation, Velocity.X, Velocity.Y);
//	}
//}

//[System.Runtime.InteropServices.ComVisible(false)]
//public class RigidBodies2D
//	: System.Collections.Generic.List<RigidBody2D>
//{
//	public System.Numerics.Vector2 Centroid { get; set; }

//	public System.Numerics.Vector2 CalculateCentroid()
//	{
//		Centroid = new System.Numerics.Vector2();
//		foreach (RigidBody2D body in this)
//			Centroid += body.Position;
//		Centroid /= Count;
//		return Centroid;
//	}

//	public ForceGenerators2D ForceGenerators = new ForceGenerators2D();

//	public virtual void Update(float deltaTime)
//	{
//		Centroid = CalculateCentroid();

//		foreach (RigidBody2D body in this)
//		{
//			body.Neighborhood.FindNeighbors(body, this);

//			body.ComputeForces(ForceGenerators, this);

//			body.Integrate(deltaTime);

//			// HandleCollisions(body, this) { }
//		}
//	}
//}
//#endregion

//#region "Neighborhood2D"
//[System.Runtime.InteropServices.ComVisible(false)]
//public class Neighborhood2D
//	: RigidBodies2D
//{
//	public System.Numerics.Vector2 AverageDifference = new System.Numerics.Vector2();
//	public System.Numerics.Vector2 AverageVelocity = new System.Numerics.Vector2();
//	public System.Numerics.Vector2 AveragePosition = new System.Numerics.Vector2();

//	public RigidBody2D ClosestNeighbor = null;
//	public float ClosestNeighborDistance = float.NaN;
//	public System.Numerics.Vector2 ClosestNeighborVector = default(System.Numerics.Vector2);

//	public bool Enabled { get; set; }

//	public System.Numerics.Vector2 PositionDifference = new System.Numerics.Vector2();
//	public System.Numerics.Vector2 PositionSum = new System.Numerics.Vector2();
//	public System.Numerics.Vector2 VelocitySum = new System.Numerics.Vector2();

//	public float PerimeterRadius = 21.5f;

//	public void FindNeighbors(RigidBody2D body, RigidBodies2D bodies)
//	{
//		if (!Enabled)
//			return;

//		Clear();

//		AverageVelocity = default(System.Numerics.Vector2);
//		AveragePosition = default(System.Numerics.Vector2);

//		ClosestNeighbor = null;
//		ClosestNeighborDistance = float.MaxValue;
//		ClosestNeighborVector = default(System.Numerics.Vector2);

//		foreach (RigidBody2D boid in bodies)
//		{
//			if (!boid.Equals(body))
//			{
//				System.Numerics.Vector2 point = boid.Position - body.Position;
//				float distance = point.Length();

//				if (distance < PerimeterRadius)
//				{
//					Add(boid);
//					PositionDifference -= point;
//					PositionSum += boid.Velocity;
//					VelocitySum += boid.Velocity;
//				}

//				if (distance < ClosestNeighborDistance)
//				{
//					ClosestNeighbor = boid;
//					ClosestNeighborDistance = distance;
//					ClosestNeighborVector = point;
//				}
//			}
//		}

//		if (Count == 0)
//			Add(ClosestNeighbor);

//		AveragePosition = PositionSum / Count;
//		AverageVelocity = VelocitySum / Count;
//	}
//}
//#endregion
