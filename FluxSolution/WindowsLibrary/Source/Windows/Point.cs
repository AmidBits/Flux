using System.Linq;

namespace Flux.Wpf
{
	public static partial class Windows
	{
		public static System.Windows.Point Point(this System.Random random)
		{
			return new System.Windows.Point(random.NextDouble(), random.NextDouble());
		}

		#region System.Windows.Point
		public static System.Windows.Point ToPoint(this System.Numerics.Vector2 self)
		{
			return new System.Windows.Point(self.X, self.Y);
		}
		public static System.Numerics.Vector2 ToVector2(this System.Windows.Point self)
		{
			return new System.Numerics.Vector2((float)self.X, (float)self.Y);
		}
		public static System.Windows.Point ToVector3(this System.Numerics.Vector3 self)
		{
			return new System.Windows.Point(self.X, self.Y);
		}
		public static System.Numerics.Vector3 ToVector3(this System.Windows.Point self)
		{
			return new System.Numerics.Vector3((float)self.X, (float)self.Y, 0);
		}

		/// <summary>Returns a new point by adding the point to the original.</summary>
		public static System.Windows.Point Add(this System.Windows.Point self, System.Windows.Point point)
		{
			return new System.Windows.Point(self.X + point.X, self.Y + point.Y);
		}
		/// <summary>Returns a new point by adding the scalar to the original.</summary>
		public static System.Windows.Point Add(this System.Windows.Point self, double scalar)
		{
			return new System.Windows.Point(self.X + scalar, self.Y + scalar);
		}
		/// <summary>Returns the cross product. This is also known as the perp-dot product.</summary>
		public static double Cross(this System.Windows.Point self, System.Windows.Point point)
		{
			return (self.X * point.Y - self.Y * point.X);
		}
		/// <summary>Returns a new point by dividing the original with the point.</summary>
		public static System.Windows.Point Divide(this System.Windows.Point self, System.Windows.Point point)
		{
			return new System.Windows.Point(self.X / point.X, self.Y / point.Y);
		}
		/// <summary>Returns a new point by dividing the original with the scalar.</summary>
		public static System.Windows.Point Divide(this System.Windows.Point self, double scalar)
		{
			return new System.Windows.Point(self.X / scalar, self.Y / scalar);
		}
		/// <summary>Returns a scalar dot product.</summary>
		public static double Dot(this System.Windows.Point self, System.Windows.Point point)
		{
			return ((self.X * point.X) + (self.Y * point.Y));
		}
		/// <summary>Returns the angle between the two vectors.</summary>
		public static double GetAngleTo(this System.Windows.Point self, System.Windows.Point point, bool inDegrees = false)
		{
			var cos = self.Dot(point);
			var sin = self.Cross(point);

			var angleInRadians = System.Math.Atan2(cos, sin);

			if (inDegrees)
				return Quantity.Angle.ConvertRadianToDegree(angleInRadians);

			return angleInRadians;
		}
		/// <summary>Returns the angular rotation of the vector. Zero is to the top.</summary>
		public static double GetAngularRotation(this System.Windows.Point self, bool inDegrees = false) => self.PointToAngularRotation(inDegrees);
		/// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise.</summary>
		public static System.Windows.Point GetPerpendicularCcw(this System.Windows.Point self)
		{
			return new System.Windows.Point(-self.Y, self.X);
		}
		/// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise.</summary>
		public static System.Windows.Point GetPerpendicularCw(this System.Windows.Point self)
		{
			return new System.Windows.Point(self.Y, -self.X);
		}
		/// <summary>Returns whether the point is inside the ellipse.</summary>
		public static bool InsideEllipse(this System.Windows.Point self, double width, double height)
		{
			return (System.Math.Pow(self.X / width, 2.0) + System.Math.Pow(self.Y / height, 2.0) <= 1.0);
		}
		/// <summary>Returns whether the point is inside the polygon (array of points). The method "closes" the polygon automatically.</summary>
		public static bool InsidePolygon(this System.Windows.Point self, System.Collections.Generic.IList<System.Windows.Point> points)
		{
			return points.InclusionTest(self) != 0d;
		}
		/// <summary>Returns whether the point is Left|On|Right of an infinite line "a to b".</summary>
		public static int IsLeft(this System.Windows.Point self, System.Windows.Point a, System.Windows.Point b)
		{
			return (int)((b.X - a.X) * (self.Y - a.Y) - (self.X - a.X) * (b.Y - a.Y));
		}
		/// <summary>Returns the length (the magnitude) of the vector.</summary>
		public static double Length(this System.Windows.Point self)
		{
			return System.Math.Sqrt(self.X * self.X + self.Y * self.Y);
		}
		/// <summary>Returns a new point by multiplying the original with the point.</summary>
		public static System.Windows.Point Multiply(this System.Windows.Point self, System.Windows.Point point)
		{
			return new System.Windows.Point(self.X * point.X, self.Y * point.Y);
		}
		/// <summary>Returns a new point by multiplying the original with the scalar.</summary>
		public static System.Windows.Point Multiply(this System.Windows.Point self, double scalar)
		{
			return new System.Windows.Point(self.X * scalar, self.Y * scalar);
		}
		/// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise.</summary>
		public static System.Windows.Point Negate(this System.Windows.Point self)
		{
			return new System.Windows.Point(-self.X, -self.Y);
		}
		/// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise.</summary>
		public static System.Windows.Point Normalize(this System.Windows.Point self)
		{
			return self.Divide(self.Length());
		}
		/// <summary>Returns the perp-dot product. This is also known as the cross product.</summary>
		public static double PerpDot(this System.Windows.Point self, System.Windows.Point point)
		{
			return (self.X * point.Y - self.Y * point.X);
		}
		/// <summary>Returns the point rotated by the angle specified.</summary>
		public static System.Windows.Point Rotate(this System.Windows.Point self, double relativeRotationAngle, bool inDegrees = false)
		{
			if (inDegrees)
				relativeRotationAngle = Quantity.Angle.ConvertDegreeToRadian(relativeRotationAngle);

			var cos = System.Math.Cos(relativeRotationAngle);
			var sin = System.Math.Sin(relativeRotationAngle);

			return new System.Windows.Point(self.X * cos - self.Y * sin, self.X * sin + self.Y * cos);
		}
		/// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line. Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
		public static int SideTest(this System.Windows.Point self, System.Windows.Point p1, System.Windows.Point p2)
		{
			return System.Math.Sign((p2.X - p1.X) * (self.Y - p1.Y) - (self.X - p1.X) * (p2.Y - p1.Y));
		}
		/// <summary>Returns a new point by subtracting the point from the original.</summary>
		public static System.Windows.Point Subtract(this System.Windows.Point self, System.Windows.Point point)
		{
			return new System.Windows.Point(self.X - point.X, self.Y - point.Y);
		}
		/// <summary>Returns a new point by subtracting the scalar from the original.</summary>
		public static System.Windows.Point Subtract(this System.Windows.Point self, double scalar)
		{
			return new System.Windows.Point(self.X - scalar, self.Y - scalar);
		}
		#endregion

		#region System.Collections.Generic..<System.Windows.Point>
		/// <summary>Returns a sequence of points from a PointCollection using optional indices.</summary>
		public static System.Collections.Generic.IEnumerable<System.Windows.Point> FromPointCollection(this System.Windows.Media.PointCollection self, params int[] indices)
		{
			foreach (var p in (indices.Length > 0 ? self.Where((p, i) => indices.Contains(i)) : self))
			{
				yield return p;
			}
		}
		/// <summary>Returns a new polygon as PointCollection from the polygon using optional indices.No indices results in a copy of the polygon.</summary>
		public static System.Windows.Media.PointCollection ToPointCollection(this System.Collections.Generic.IEnumerable<System.Windows.Point> self, params int[] indices)
		{
			var pc = new System.Windows.Media.PointCollection();

			foreach (var p in (indices.Length > 0 ? self.Where((p, i) => indices.Contains(i)) : self))
			{
				pc.Add(p);
			}

			return pc;
		}

		/// <summary>Returns the computed area of the polygon.</summary>
		public static double Area(this System.Collections.Generic.IList<System.Windows.Point> polygon)
		{
			//if (polygon.Count == 3)
			//    return (polygon[1].X - polygon[0].X) * (polygon[2].Y - polygon[0].Y) - (polygon[2].X - polygon[0].X) * (polygon[1].Y - polygon[0].Y);
			//else if (polygon.Count == 4)
			//    return ((polygon[2].X - polygon[0].X) * (polygon[3].Y - polygon[1].Y) - (polygon[3].X - polygon[1].X) * (polygon[2].Y - polygon[0].Y)) / 2;
			//else
			//{
			var area = 0.0;

			for (var i = 1; i < polygon.Count; i++)
			{
				area += polygon[i].X * (polygon[i + 1].Y - polygon[i - 1].Y);
			}

			area += polygon[0].X * (polygon[1].Y - polygon[polygon.Count - 1].Y);

			return area / 2.0;
			//}
		}
		/// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon.</summary>
		public static System.Windows.Point GetCentroid(this System.Collections.Generic.IEnumerable<System.Windows.Point> polygon)
		{
			double x = 0, y = 0;
			int count = 0;
			foreach (var point in polygon)
			{
				x += point.X;
				y += point.Y;
				count++;
			}
			return new System.Windows.Point(x / count, y / count);
		}
		/// <summary>Returns all midpoints (halfway) points of the polygon. Including original points if the entire set (twice the number of points).</summary>
		public static System.Collections.Generic.IEnumerable<System.Windows.Point> GetMidways(this System.Collections.Generic.IEnumerable<System.Windows.Point> polygon, bool includeOriginalPoints = false)
		{
			using var enumerator = polygon.GetEnumerator();
			
			if (enumerator.MoveNext())
			{
				var first = enumerator.Current;

				var previous = first;

				while (enumerator.MoveNext())
				{
					if (includeOriginalPoints)
					{
						yield return previous;
					}

					yield return new System.Windows.Point((previous.X + enumerator.Current.X) * 0.5, (previous.Y + enumerator.Current.Y) * 0.5);

					previous = enumerator.Current;
				}

				if (includeOriginalPoints)
				{
					yield return previous;
				}

				yield return new System.Windows.Point((previous.X + first.X) * 0.5, (previous.Y + first.Y) * 0.5);
			}
		}
		/// <summary>Determines the inclusion of a point in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
		/// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
		public static int InclusionTest(this System.Collections.Generic.IList<System.Windows.Point> polygon, System.Windows.Point point)
		{
			int wn = 0;

			System.Windows.Point a, b;

			for (int i = 0; i < polygon.Count; i++)
			{
				a = polygon[i];
				b = (i == polygon.Count - 1 ? polygon[0] : polygon[i + 1]);

				if (a.Y <= point.Y)
				{
					if (b.Y > point.Y)
					{
						if (point.SideTest(a, b) > 0)
						{
							wn++;
						}
					}
				}
				else
				{
					if (b.Y <= point.Y)
					{
						if (point.SideTest(a, b) < 0)
						{
							wn--;
						}
					}
				}
			}

			return wn;
		}
		/// <summary>Determines whether this polygon is convex.</summary>
		public static bool IsConvex(this System.Collections.Generic.IList<System.Windows.Point> polygon)
		{
			bool negative = false, positive = false;

			for (int i = 0; i < polygon.Count; i++)
			{
				var c = polygon[i];
				var a = polygon[(i == 0 ? polygon.Count - 1 : i - 1)];
				a.X -= c.X;
				a.Y -= c.Y;
				var b = polygon[(i == polygon.Count - 1 ? 0 : i + 1)];
				b.X -= c.X;
				b.Y -= c.Y;

				var angle = System.Math.Acos(a.Dot(b));

				if (angle < 0d)
				{
					negative = true;
				}
				else
				{
					positive = true;
				}

				if (negative && positive)
				{
					return false;
				}
			}

			return (negative ^ positive);
		}
		/// <summary>Returns the computed area of the polygon.</summary>
		public static double Orientation(this System.Collections.Generic.IList<System.Windows.Point> self)
		{
			int imin = 0;
			double xmin = self[imin].X;
			double ymin = self[imin].Y;

			for (int i = 1; i < self.Count; i++)
			{
				if (self[i].Y > ymin)
				{
					continue;
				}

				if (self[i].Y == ymin)
				{
					if (self[i].X < xmin)
					{
						continue;
					}
				}

				imin = i;
				xmin = self[i].X;
				ymin = self[i].Y;
			}

			if (imin == 0)
			{
				return self[1].IsLeft(self[self.Count - 1], self[0]);
			}
			else
			{
				return self[imin + 1].IsLeft(self[imin - 1], self[imin]);
			}
		}
		///// <summary>Returns a new set of polygons from a polygon.</summary>
		//public static System.Collections.Generic.IList<System.Collections.Generic.IList<System.Windows.Point>> Slice(this System.Collections.Generic.IList<System.Windows.Point> self, int count)
		//{
		//	var sp = new System.Collections.Generic.List<System.Collections.Generic.IList<System.Windows.Point>>();
		//	var pc = self.GetCentroid();
		//	for (int i = 0; i < self.Count; i++)
		//		sp.Add(new System.Collections.Generic.List<System.Windows.Point>() { pc, self[i], self[(i + 1) % self.Count] });
		//	return sp;
		//}

		///// <summary>Returns a new set of triangles from the polygon centroid to its points.</summary>
		//public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Windows.Point>> SplitCentroidTriangleFan(this System.Collections.Generic.IList<System.Windows.Point> self)
		//{
		//	var pc = self.GetCentroid();
		//	for (int i = 0; i < self.Count; i++)
		//		yield return new System.Collections.Generic.List<System.Windows.Point>() { pc, self[i], self[(i + 1) % self.Count] };
		//}
		///// <summary>Returns a new set of quadrilaterals from the polygon centroid to its halfways.</summary>
		//public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Windows.Point>> SplitCentroidQuadrilateralFan(this System.Collections.Generic.IList<System.Windows.Point> self)
		//{
		//	var pc = self.GetCentroid();
		//	var mw = self.GetMidways().ToList();
		//	for (int i = 0; i < self.Count; i++)
		//		yield return new System.Collections.Generic.List<System.Windows.Point>() { pc, mw[i > 0 ? i - 1 : mw.Count - 1], self[i], mw[i] };
		//}
		///// <summary>Returns a new set of triangles from any polygon point to all other points.</summary>
		//public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Windows.Point>> SplitFirstTriangleFan(this System.Collections.Generic.IList<System.Windows.Point> self)
		//{
		//	for (int i = 1; i < self.Count - 1; i++)
		//		yield return new System.Collections.Generic.List<System.Windows.Point>() { self[0], self[i], self[(i + 1) % self.Count] };
		//}
		///// <summary>Returns a new set of polygons by splitting the polygon at two points.</summary>
		//public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Windows.Point>> SplitFirstPointHalfs(this System.Collections.Generic.IList<System.Windows.Point> self, double midwayMultiplier = 0.5)
		//{
		//	if (self.Count.IsEven())
		//	{
		//		int half = self.Count / 2;
		//		yield return self.Take(half + 1).ToList();
		//		yield return self.Skip(half).Append(self.First()).ToList();
		//	}
		//	else
		//	{
		//		int half = (self.Count + 1) / 2;
		//		var a = self.Take(half).ToList();
		//		var c = a.Last().Add(self.First()).Multiply(midwayMultiplier);
		//		a.Add(c);
		//		yield return a;
		//		var b = self.Skip(half).Append(a.First()).ToList();
		//		b.Insert(0, c);
		//		yield return b;
		//	}
		//}

		/// <summary>Returns a new set of polygons by splitting the polygon at two points.</summary>
		public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Windows.Point>> SplitInHalf(this System.Collections.Generic.IList<System.Windows.Point> self)
		{
			int half = self.Count / 2;

			if ((self.Count & 0b1) == 0)
			{
				yield return self.Take(half + 1).ToList();
				yield return self.Skip(half).Append(self.First()).ToList();
			}
			else
			{
				half++;

				var a = self.Take(half);
				var b = self.Skip(half).Append(self.First());

				var c = a.Last().Add(b.First()).Divide(2.0);

				yield return a.Append(c).ToList();
				yield return b.Prepend(c).ToList();
			}
		}
		/// <summary>Returns a new set of quadrilaterals from the polygon centroid to its halfways.</summary>
		public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Windows.Point>> SplitQuadrilateralFan(this System.Collections.Generic.IList<System.Windows.Point> self)
		{
			var pc = self.GetCentroid();
			var mw = self.GetMidways().ToList();
			for (int i = 0; i < self.Count; i++)
			{
				yield return new System.Collections.Generic.List<System.Windows.Point>() { pc, mw[i > 0 ? i - 1 : mw.Count - 1], self[i], mw[i] };
			}
		}
		/// <summary>Returns a new set of triangles from the polygon centroid to its points.</summary>
		public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Windows.Point>> SplitTriangleFan(this System.Collections.Generic.IList<System.Windows.Point> self)
		{
			var pc = self.GetCentroid();
			for (int i = 0; i < self.Count; i++)
			{
				yield return new System.Collections.Generic.List<System.Windows.Point>() { pc, self[i], self[(i + 1) % self.Count] };
			}
		}
		/// <summary>Returns a new set of triangles from any polygon point to all other points.</summary>
		public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Windows.Point>> SplitTriangleFan(this System.Collections.Generic.IList<System.Windows.Point> self, int center)
		{
			if (center < 0 || center >= self.Count - 1)
			{
				throw new System.IndexOutOfRangeException();
			}

			for (int i = center + 1; i < (center + self.Count) - 1; i++)
			{
				yield return new System.Collections.Generic.List<System.Windows.Point>() { self[center], self.ElementAt(i % self.Count), self.ElementAt((i + 1) % self.Count) };
			}
		}

		/// <summary>Returns a new set of triangular polygons from the specified polygon.</summary>
		//public static void Triangulate(System.Collections.Generic.IList<System.Numerics.Vector2> polygon)
		public static System.Collections.Generic.IEnumerable<System.Windows.Point[]> Triangulate(this System.Collections.Generic.IList<System.Windows.Point> polygon)
		{
			var copy = polygon.ToList();

			var triangle = new System.Windows.Point[3];

			while (copy.Count > 3)
			{
				for (int i = 0; i < copy.Count; i++)
				{
					triangle[0] = copy[i]; // c
					triangle[1] = copy[(i == copy.Count - 1 ? 0 : i + 1)]; // b
					triangle[2] = copy[(i == 0 ? copy.Count - 1 : i - 1)]; // a

					var angle = System.Math.Acos(triangle[2].Subtract(triangle[0]).Normalize().Dot(triangle[1].Subtract(triangle[0]).Normalize()));

					if (angle > 0 && angle < System.Math.PI)
					{
						yield return triangle;

						copy.RemoveAt(i);

						break;
					}
				}
			}
		}
		#endregion
	}
}
