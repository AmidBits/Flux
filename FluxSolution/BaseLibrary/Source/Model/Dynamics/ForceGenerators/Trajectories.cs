namespace Flux.Model.Dynamics.Trajectories
{
	public class TrajectoryFlat2D
	{
		protected double m_g = 9.80665;
		/// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
		public double GravitationalAcceleration { get => m_g; set => m_g = value; }

		protected double m_initialAngle;
		/// <summary>Initial angle in radians (RAD).</summary>
		public double InitialAngle { get => m_initialAngle; set => m_initialAngle = value; }

		protected double m_initialVelocity;
		/// <summary>Initial velocity in meters per second (M/S).</summary>
		public double InitialVelocity { get => m_initialVelocity; set => m_initialVelocity = value; }

		public TrajectoryFlat2D(double initialAngle, double initialVelocity)
		{
			m_initialAngle = initialAngle;
			m_initialVelocity = initialVelocity;
		}

		public virtual double GetX(double time)
			=> m_initialVelocity * System.Math.Cos(m_initialAngle) * time;
		public virtual double GetY(double time)
			=> m_initialVelocity * System.Math.Sin(m_initialAngle) * time - m_g * time * time / 2;
		public virtual double GetVelocityX(double time)
			=> m_initialVelocity * System.Math.Cos(m_initialAngle);
		public virtual double GetVelocityY(double time)
			=> m_initialVelocity * System.Math.Sin(m_initialAngle) - m_g * time;
		public virtual double GetVelocity(double time)
			=> m_initialVelocity * m_initialVelocity - 2 * m_g * time * m_initialVelocity * System.Math.Sin(m_initialAngle) + m_g * m_g * time * time;
		public virtual double GetMaxHeight()
			=> System.Math.Pow(System.Math.Sin(m_initialAngle), 2) * m_initialVelocity * m_initialVelocity / (2 * m_g);
		public virtual double GetRange()
			=> m_initialVelocity * GetTime() * System.Math.Cos(m_initialAngle);
		public virtual double GetTime()
			=> 2 * m_initialVelocity * System.Math.Sin(m_initialAngle) / m_g;

		public override string ToString()
			=> $"<{GetType().Name}: H={GetMaxHeight():N1} m, R={GetRange():N1} m, T={GetTime():N1} s>";
	}

	public class TrajectoryUphill2D // 
		: TrajectoryFlat2D
	{
		private double m_verticalDifference;
		/// <summary>The difference of vertical level in meters (M).</summary>
		public double VerticalDifference { get => m_verticalDifference; set => m_verticalDifference = value; }

		public TrajectoryUphill2D(double initialAngle, double initialVelocity, double verticalDifference)
			: base(initialAngle, initialVelocity)
		{
			m_verticalDifference = verticalDifference;
		}

		public override double GetTime()
			=> m_initialVelocity * System.Math.Sin(m_initialAngle) / m_g + System.Math.Sqrt(2 * (GetMaxHeight() - m_verticalDifference) / m_g);
	}

	public class TrajectoryDownhill2D // 
		: TrajectoryFlat2D
	{
		private double m_verticalDifference;
		/// <summary>The difference of vertical level in meters (M).</summary>
		public double VerticalDifference { get => m_verticalDifference; set => m_verticalDifference = value; }

		public TrajectoryDownhill2D(double initialAngle, double initialVelocity, double verticalDifference)
			: base(initialAngle, initialVelocity)
		{
			m_verticalDifference = verticalDifference;
		}

		public override double GetMaxHeight()
			=> m_verticalDifference + m_initialVelocity * m_initialVelocity * System.Math.Pow(System.Math.Sin(InitialAngle), 2) / (2 * m_g);
		public override double GetTime()
			=> m_initialVelocity * System.Math.Sin(m_initialAngle) / m_g + System.Math.Sqrt(2 * GetMaxHeight() / m_g);
	}

	public class TrajectoryDropped2D // Projectile dropped from a moving system.
		: TrajectoryFlat2D
	{
		protected double m_droppedHeight;
		// The height when dropped.
		public double DroppedHeight { get => m_droppedHeight; set => m_droppedHeight = value; }

		public TrajectoryDropped2D(double initialAngle, double initialVelocity, double droppedHeight)
		: base(initialAngle, initialVelocity)
		{
			m_droppedHeight = droppedHeight;
		}

		public override double GetX(double time)
			=> m_initialVelocity * time;
		public override double GetY(double time)
			=> GetHeight(time) - m_g * time * time / 2;
		public override double GetVelocityX(double time)
			=> m_initialVelocity;
		public override double GetVelocityY(double time)
			=> -m_g * time;
		public override double GetVelocity(double time)
			=> System.Math.Sqrt(m_initialVelocity * m_initialVelocity + m_g * m_g * time * time);
		public double GetHeight(double time)
			=> m_g * time * time / 2;
		public override double GetMaxHeight()
			=> m_droppedHeight;
		public override double GetRange()
			=> m_initialVelocity * GetTime();
		public override double GetTime()
			=> System.Math.Sqrt(2 * GetMaxHeight() / m_g);
	}
}
