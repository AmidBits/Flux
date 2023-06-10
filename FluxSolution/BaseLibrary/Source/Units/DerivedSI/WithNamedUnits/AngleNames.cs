namespace Flux
{
  public static partial class UnitsExtensionMethods
  {
    public static bool Is(this Units.AngleNames source, Units.Angle angle) => source switch
    {
      Units.AngleNames.ZeroAngle => angle == Units.Angle.Zero,
      Units.AngleNames.AcuteAngle => angle < Units.Angle.QuarterTurn,
      Units.AngleNames.RightAngle => angle == Units.Angle.QuarterTurn,
      Units.AngleNames.ObtuseAngle => angle > Units.Angle.QuarterTurn && angle < Units.Angle.HalfTurn,
      Units.AngleNames.StraightAngle => angle == Units.Angle.HalfTurn,
      Units.AngleNames.ReflexAngle => angle > Units.Angle.HalfTurn && angle < Units.Angle.FullTurn,
      Units.AngleNames.PerigonAngle => angle == Units.Angle.FullTurn,
      Units.AngleNames.ObliqueAngle => (angle % Units.Angle.QuarterTurn) != Units.Angle.Zero,
      Units.AngleNames.Unknown => angle > Units.Angle.FullTurn,
      _ => false,
    };
  }

  namespace Units
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
