namespace Flux
{
  public static partial class Em
  {
    public static bool Is(this Quantities.AngleNames source, Quantities.Angle angle) => source switch
    {
      Quantities.AngleNames.ZeroAngle => angle == Quantities.Angle.Zero,
      Quantities.AngleNames.AcuteAngle => angle < Quantities.Angle.QuarterTurn,
      Quantities.AngleNames.RightAngle => angle == Quantities.Angle.QuarterTurn,
      Quantities.AngleNames.ObtuseAngle => angle > Quantities.Angle.QuarterTurn && angle < Quantities.Angle.HalfTurn,
      Quantities.AngleNames.StraightAngle => angle == Quantities.Angle.HalfTurn,
      Quantities.AngleNames.ReflexAngle => angle > Quantities.Angle.HalfTurn && angle < Quantities.Angle.FullTurn,
      Quantities.AngleNames.PerigonAngle => angle == Quantities.Angle.FullTurn,
      Quantities.AngleNames.ObliqueAngle => (angle % Quantities.Angle.QuarterTurn) != Quantities.Angle.Zero,
      Quantities.AngleNames.Unknown => angle > Quantities.Angle.FullTurn,
      _ => false,
    };
  }

  namespace Quantities
  {
    [System.Flags]
    public enum AngleNames
    {
      Unknown,
      /// <summary>An angle equal to 0° or not turned is called a zero angle.</summary>
      ZeroAngle = 1,
      /// <summary>An angle smaller than a right angle (less than 90°) is called an acute angle ("acute" meaning "sharp").</summary>
      AcuteAngle = 2,
      /// <summary>An angle equal to 1/4 turn (90°, or π/2 radians) is called a right angle. Two lines that form a right angle are said to be normal, orthogonal, or perpendicular.</summary>
      RightAngle = 4,
      /// <summary>An angle larger than a right angle and smaller than a straight angle (between 90° and 180°, or π/2 and π radians) is called an obtuse angle ("obtuse" meaning "blunt").</summary>
      ObtuseAngle = 8,
      /// <summary>An angle equal to 1/2 turn (180°, or π radians) is called a straight angle.</summary>
      StraightAngle = 16,
      /// <summary>An angle larger than a straight angle but less than 1 turn (between 180° and 360°, or π and 2π radians) is called a reflex angle.</summary>
      ReflexAngle = 32,
      /// <summary>An angle equal to 1 turn (360°, or 2π radians) is called a full angle, complete angle, round angle or a perigon.</summary>
      PerigonAngle = 64,
      /// <summary>An angle that is not a multiple of a right angle is called an oblique angle.</summary>
      ObliqueAngle = 128
    }
  }
}
