using System;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX
{

	/// <summary>A ray.</summary>
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	public struct Ray : IEquatable<Ray>
	{

		/// <summary>Defines the default threshold value used in <see cref="Ray"/>, <see cref="Plane"/> and <see cref="BoundingFrustum"/> intersection detection.</summary>
		public const float DefaultIntersectionThreshold = 1E-05f; // Ray, Plane, BoundingFrustum
		
		/// <summary>Defines the default threshold value used in <see cref="BoundingBox"/> and <see cref="Vector3"/> intersection detection.</summary>
		public const float DefaultBoxIntersectionThreshold = 1E-06f; // Vector3, BoundingBox

		// TODO - add threshold support to intersection detection methods


		/// <summary>The starting position (=source) of the ray.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		public Vector3 Position;

		/// <summary>The direction of the ray; must be normalized.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		public Vector3 Direction;



		/// <summary>Initializes a new <see cref="Ray"/>.</summary>
		/// <param name="position">The starting point of the ray.</param>
		/// <param name="direction">The direction of the ray; must be normalized.</param>
		public Ray( Vector3 position, Vector3 direction )
		{
			Position = position;
			Direction = direction;
		}


		
		#region Intersects

		/// <summary>Determines whether this <see cref="Ray"/> intersects a point.</summary>
		/// <param name="point">A <see cref="Vector3"/>.</param>
		/// <param name="result">Receives the distance this <see cref="Ray"/> intersects the <paramref name="point"/> at, or NaN if there is no intersection.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref Vector3 point, out float result )
		{
			Vector3 vector;
			Vector3.Subtract( ref point, ref Position, out vector );
			
			float RdotP;
			Vector3.Dot( ref Direction, ref vector, out RdotP );	// assumes the ray direction is normalized

			if( RdotP <= 0.0f )
			{
				result = float.NaN;
				return;
			}

			result = vector.Length;

			if( RdotP / result < 1.0f - DefaultBoxIntersectionThreshold )
				result = float.NaN;
		}

		/// <summary>Returns the distance this <see cref="Ray"/> intersects a point at, or NaN if there is no intersection.</summary>
		/// <param name="point">A <see cref="Vector3"/>.</param>
		/// <returns>Returns the distance this <see cref="Ray"/> intersects the <paramref name="point"/> at, or NaN if there is no intersection.</returns>
		public float Intersects( Vector3 point )
		{
			Vector3 vector;
			Vector3.Subtract( ref point, ref Position, out vector );

			float RdotP;
			Vector3.Dot( ref Direction, ref vector, out RdotP );	// assumes the ray direction is normalized

			if( RdotP <= 0.0f )
				return float.NaN;

			var result = vector.Length;
			
			if( RdotP / result < 1.0f - DefaultBoxIntersectionThreshold )
				return float.NaN;

			return result;
		}


		/// <summary>Determines whether this <see cref="Ray"/> intersects another ray.</summary>
		/// <param name="ray">A <see cref="Ray"/>.</param>
		/// <param name="result">Receives the distance this <see cref="Ray"/> intersects the other <paramref name="ray"/> at, or NaN if there is no intersection.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref Ray ray, out float result )
		{
			result = 0.0f;
			if( ray.Equals( this ) )
				return;

			var rayPosition = ray.Position;
			var rayDirection = ray.Direction;

			Vector3 planeNormal;
			Vector3.Cross( ref Direction, ref rayDirection, out planeNormal );

			var x = rayPosition.X - Position.X;
			var y = rayPosition.Y - Position.Y;
			var z = rayPosition.Z - Position.Z;

			var d = x * rayDirection.Y * planeNormal.Z + y * rayDirection.Z * planeNormal.X + z * rayDirection.X * planeNormal.Y - x * rayDirection.Z * planeNormal.Y - y * rayDirection.X * planeNormal.Z - z * rayDirection.Y * planeNormal.X;
			result = d / planeNormal.LengthSquared;

			var rayDirection2 = ( Position + result * Direction ) - rayPosition;
			var rayLength = rayDirection2.Length;

			float dot;
			Vector3.Dot( ref rayDirection2, ref rayDirection, out dot );
			if( Math.Abs( dot / rayLength ) < 1.0f - DefaultIntersectionThreshold )
				result = float.NaN;
		}

		/// <summary>Returns the distance this <see cref="Ray"/> intersects another ray at, or NaN if there is no intersection.</summary>
		/// <param name="ray">A <see cref="Ray"/>.</param>
		/// <returns>Returns the distance this <see cref="Ray"/> intersects the other <paramref name="ray"/> at, or NaN if there is no intersection.</returns>
		public float Intersects( Ray ray )
		{
			if( ray.Equals( this ) )
				return 0.0f;

			Vector3 planeNormal;
			Vector3.Cross( ref Direction, ref ray.Direction, out planeNormal );

			var x = ray.Position.X - Position.X;
			var y = ray.Position.Y - Position.Y;
			var z = ray.Position.Z - Position.Z;

			var rayDirection = ray.Direction;

			var d = x * rayDirection.Y * planeNormal.Z + y * rayDirection.Z * planeNormal.X + z * rayDirection.X * planeNormal.Y - x * rayDirection.Z * planeNormal.Y - y * rayDirection.X * planeNormal.Z - z * rayDirection.Y * planeNormal.X;
			var result = d / planeNormal.LengthSquared;

			rayDirection = ( Position + result * Direction ) - ray.Position;
			rayDirection.Normalize();

			float dot;
			Vector3.Dot( ref rayDirection, ref ray.Direction, out dot );
			if( Math.Abs( dot ) < 1.0f - DefaultIntersectionThreshold )
				result = float.NaN;
			
			return result;
		}


		/// <summary>Determines whether this <see cref="Ray"/> intersects a <see cref="Plane"/>.</summary>
		/// <param name="plane">A <see cref="Plane"/>.</param>
		/// <param name="result">Receives the distance this <see cref="Ray"/> intersects the specified <see cref="Plane"/> at, or NaN if there is no intersection.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref Plane plane, out float result )
		{
			result = float.NaN;
			
			var planeNormal = plane.Normal;
			var NdotD = planeNormal.X * Direction.X + planeNormal.Y * Direction.Y + planeNormal.Z * Direction.Z;
			if( Math.Abs( NdotD ) < DefaultIntersectionThreshold )
				return;
			
			var NdotP = planeNormal.X * Position.X + planeNormal.Y * Position.Y + planeNormal.Z * Position.Z;
			var distance = ( -plane.Distance - NdotP ) / NdotD;
			if( distance < -DefaultIntersectionThreshold )
				return;

			result = distance;
		}

		/// <summary>Returns the distance this <see cref="Ray"/> intersects a <see cref="Plane"/> at, or NaN if there is no intersection.</summary>
		/// <param name="plane">A <see cref="Plane"/>.</param>
		/// <returns>Returns the distance this <see cref="Ray"/> intersects the specified <see cref="Plane"/> at, or NaN if there is no intersection.</returns>
		public float Intersects( Plane plane )
		{
			var planeNormal = plane.Normal;
			var NdotD = planeNormal.X * Direction.X + planeNormal.Y * Direction.Y + planeNormal.Z * Direction.Z;
			if( Math.Abs( NdotD ) < DefaultIntersectionThreshold )
				return float.NaN;

			var NdotP = planeNormal.X * Position.X + planeNormal.Y * Position.Y + planeNormal.Z * Position.Z;
			var distance = ( -plane.Distance - NdotP ) / NdotD;
			if( distance < -DefaultIntersectionThreshold )
				return float.NaN;

			return distance;
		}


		/// <summary>Determines whether this <see cref="Ray"/> intersects a <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives the distance the ray intersects the <see cref="BoundingBox"/> at, or NaN if there is no intersection.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref BoundingBox box, out float result )
		{
			result = float.NaN;
			
			var boxMin = box.Min;
			var boxMax = box.Max;
			var maxDistance = 0.0f;
			var minDistance = float.MaxValue;

			if( Math.Abs( Direction.X ) < DefaultBoxIntersectionThreshold )
			{
				if( Position.X < boxMin.X || Position.X > boxMax.X )
					return;
			}
			else
			{
				var oneOverDirectionX = 1.0f / Direction.X;
				var a = ( boxMin.X - Position.X ) * oneOverDirectionX;
				var b = ( boxMax.X - Position.X ) * oneOverDirectionX;
				if( a > b )
				{
					var swap = a;
					a = b;
					b = swap;
				}
				maxDistance = Math.Max( a, maxDistance );
				minDistance = Math.Min( b, minDistance );
				if( maxDistance > minDistance )
					return;
			}

			if( Math.Abs( Direction.Y ) < DefaultBoxIntersectionThreshold )
			{
				if( Position.Y < boxMin.Y || Position.Y > boxMax.Y )
					return;
			}
			else
			{
				var oneOverDirectionY = 1.0f / Direction.Y;
				var a = ( boxMin.Y - Position.Y ) * oneOverDirectionY;
				var b = ( boxMax.Y - Position.Y ) * oneOverDirectionY;
				if( a > b )
				{
					var swap = a;
					a = b;
					b = swap;
				}
				maxDistance = Math.Max( a, maxDistance );
				minDistance = Math.Min( b, minDistance );
				if( maxDistance > minDistance )
					return;
			}

			if( Math.Abs( Direction.Z ) < DefaultBoxIntersectionThreshold )
			{
				if( Position.Z < boxMin.Z || Position.Z > boxMax.Z )
					return;
			}
			else
			{
				var oneOverDirectionZ = 1.0f / Direction.Z;
				var a = ( boxMin.Z - Position.Z ) * oneOverDirectionZ;
				var b = ( boxMax.Z - Position.Z ) * oneOverDirectionZ;
				if( a > b )
				{
					var swap = a;
					a = b;
					b = swap;
				}
				maxDistance = Math.Max( a, maxDistance );
				minDistance = Math.Min( b, minDistance );
				if( maxDistance > minDistance )
					return;
			}
			
			result = maxDistance;
		}

		/// <summary>Returns the distance this <see cref="Ray"/> intersects a <see cref="BoundingBox"/> at, or NaN if there is no intersection.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns the distance the ray intersects the <see cref="BoundingBox"/> at, or NaN if there is no intersection.</returns>
		public float Intersects( BoundingBox box )
		{
			var boxMin = box.Min;
			var boxMax = box.Max;
			var maxDistance = 0.0f;
			var minDistance = float.MaxValue;

			if( Math.Abs( Direction.X ) < DefaultBoxIntersectionThreshold )
			{
				if( Position.X < boxMin.X || Position.X > boxMax.X )
					return float.NaN;
			}
			else
			{
				var oneOverDirectionX = 1.0f / Direction.X;
				var a = ( boxMin.X - Position.X ) * oneOverDirectionX;
				var b = ( boxMax.X - Position.X ) * oneOverDirectionX;
				if( a > b )
				{
					var swap = a;
					a = b;
					b = swap;
				}
				maxDistance = Math.Max( a, maxDistance );
				minDistance = Math.Min( b, minDistance );
				if( maxDistance > minDistance )
					return float.NaN;
			}

			if( Math.Abs( Direction.Y ) < DefaultBoxIntersectionThreshold )
			{
				if( Position.Y < boxMin.Y || Position.Y > boxMax.Y )
					return float.NaN;
			}
			else
			{
				var oneOverDirectionY = 1.0f / Direction.Y;
				var a = ( boxMin.Y - Position.Y ) * oneOverDirectionY;
				var b = ( boxMax.Y - Position.Y ) * oneOverDirectionY;
				if( a > b )
				{
					var swap = a;
					a = b;
					b = swap;
				}
				maxDistance = Math.Max( a, maxDistance );
				minDistance = Math.Min( b, minDistance );
				if( maxDistance > minDistance )
					return float.NaN;
			}

			if( Math.Abs( Direction.Z ) < DefaultBoxIntersectionThreshold )
			{
				if( Position.Z < boxMin.Z || Position.Z > boxMax.Z )
					return float.NaN;
			}
			else
			{
				var oneOverDirectionZ = 1.0f / Direction.Z;
				var a = ( boxMin.Z - Position.Z ) * oneOverDirectionZ;
				var b = ( boxMax.Z - Position.Z ) * oneOverDirectionZ;
				if( a > b )
				{
					var swap = a;
					a = b;
					b = swap;
				}
				maxDistance = Math.Max( a, maxDistance );
				minDistance = Math.Min( b, minDistance );
				if( maxDistance > minDistance )
					return float.NaN;
			}

			return maxDistance;
		}


		/// <summary>Determines whether this <see cref="Ray"/> intersects a specified <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="result">Receives the distance the ray intersects the <see cref="BoundingSphere"/> at, or NaN if there is no intersection.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref BoundingSphere sphere, out float result )
		{
			var sphereCenter = sphere.Center;
			var diffX = sphereCenter.X - Position.X;
			var diffY = sphereCenter.Y - Position.Y;
			var diffZ = sphereCenter.Z - Position.Z;

			var distanceSquared = diffX * diffX + diffY * diffY + diffZ * diffZ;
			var radiusSquared = sphere.Radius * sphere.Radius;

			if( distanceSquared <= radiusSquared )
			{
				result = 0.0f;
				return;
			}

			var PdotD = diffX * Direction.X + diffY * Direction.Y + diffZ * Direction.Z;
			if( PdotD < 0.0f )
			{
				result = float.NaN;
				return;
			}

			var diffSquared = distanceSquared - PdotD * PdotD;
			if( diffSquared > radiusSquared )
			{
				result = float.NaN;
				return;
			}

			result = PdotD - (float)Math.Sqrt( (double)( radiusSquared - diffSquared ) );
		}

		/// <summary>Returns the distance this <see cref="Ray"/> intersects a <see cref="BoundingSphere"/> at, or NaN if there is no intersection.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <returns>Returns the distance the ray intersects the <see cref="BoundingSphere"/> at, or NaN if there is no intersection.</returns>
		public float Intersects( BoundingSphere sphere )
		{
			var diffX = sphere.Center.X - Position.X;
			var diffY = sphere.Center.Y - Position.Y;
			var diffZ = sphere.Center.Z - Position.Z;
			
			var distanceSquared = diffX * diffX + diffY * diffY + diffZ * diffZ;
			var radiusSquared = sphere.Radius * sphere.Radius;
			
			if( distanceSquared <= radiusSquared )
				return 0.0f;

			var dot = diffX * Direction.X + diffY * Direction.Y + diffZ * Direction.Z;
			if( dot < 0.0f )
				return float.NaN;
			
			var diffSquared = distanceSquared - dot * dot;
			if( diffSquared > radiusSquared )
				return float.NaN;
			
			return dot - (float)Math.Sqrt( (double)( radiusSquared - diffSquared ) );
		}


		/// <summary>Determines whether this <see cref="Ray"/> intersects a <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A valid <see cref="BoundingFrustum"/>.</param>
		/// <param name="result">Receives the distance the ray intersects the <see cref="BoundingFrustum"/> at, or NaN if there is no intersection.</param>
		/// <exception cref="ArgumentException"/>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref BoundingFrustum frustum, out float result )
		{
			var planes = frustum.planes;
			if( planes == null || planes.Length != BoundingFrustum.PlaneCount )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			ContainmentType containmentType;
			frustum.Contains( ref Position, out containmentType );
			if( containmentType == ContainmentType.Contains )
			{
				result = 0.0f;
				return;
			}

			var minRatio = float.MaxValue;
			var maxRatio = float.MinValue;

			result = float.NaN;
			
			float NdotD, NdotP, ratio;
			Plane plane;
			for( var i = 0; i < BoundingFrustum.PlaneCount; i++ )
			{
				plane = planes[ i ];
				
				Vector3.Dot( ref Direction, ref plane.Normal, out NdotD );
				
				Vector3.Dot( ref Position, ref plane.Normal, out NdotP );
				NdotP += plane.Distance;

				if( Math.Abs( NdotD ) < DefaultIntersectionThreshold )
				{
					if( NdotP > 0.0f )
						return;
				}
				else
				{
					ratio = -NdotP / NdotD;
					if( NdotD < 0.0f )
					{
						if( ratio > minRatio )
							return;

						if( ratio > maxRatio )
							maxRatio = ratio;
					}
					else
					{
						if( ratio < maxRatio )
							return;

						if( ratio < minRatio )
							minRatio = ratio;
					}
				}
			}

			if( maxRatio >= 0.0f )
			{
				result = maxRatio;
				return;
			}

			if( minRatio >= 0.0f )
			{
				result = minRatio;
				return;
			}
		}

		/// <summary>Returns the distance this <see cref="Ray"/> intersects a <see cref="BoundingFrustum"/> at, or NaN if there is no intersection.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <returns>Returns the distance this <see cref="Ray"/> intersects the <see cref="BoundingFrustum"/> at, or NaN if there is no intersection.</returns>
		public float Intersects( BoundingFrustum frustum )
		{
			if( frustum.planes == null || frustum.planes.Length != BoundingFrustum.PlaneCount )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			ContainmentType containmentType;
			frustum.Contains( ref Position, out containmentType );
			if( containmentType == ContainmentType.Contains )
				return 0.0f;

			var minRatio = float.MaxValue;
			var maxRatio = float.MinValue;

			float NdotD, NdotP, ratio;
			Plane plane;
			for( var i = 0; i < BoundingFrustum.PlaneCount; i++ )
			{
				plane = frustum.planes[ i ];

				Vector3.Dot( ref Direction, ref plane.Normal, out NdotD );
				Vector3.Dot( ref Position, ref plane.Normal, out NdotP );
				NdotP += plane.Distance;

				if( Math.Abs( NdotD ) < DefaultIntersectionThreshold )
				{
					if( NdotP > 0.0f )
						return float.NaN;
				}
				else
				{
					ratio = -NdotP / NdotD;
					if( NdotD < 0.0f )
					{
						if( ratio > minRatio )
							return float.NaN;

						if( ratio > maxRatio )
							maxRatio = ratio;
					}
					else
					{
						if( ratio < maxRatio )
							return float.NaN;

						if( ratio < minRatio )
							minRatio = ratio;
					}
				}
			}

			if( maxRatio >= 0.0f )
				return maxRatio;

			if( minRatio >= 0.0f )
				return minRatio;

			return float.NaN;
		}

		#endregion Intersects



		/// <summary>Returns a hash code for this <see cref="Ray"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Ray"/>.</returns>
		public override int GetHashCode()
		{
			return Position.GetHashCode() ^ Direction.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Ray"/> equals another <see cref="Ray"/>.</summary>
		/// <param name="other">A <see cref="Ray"/>.</param>
		/// <returns>Returns true if the rays are equal, otherwise returns false.</returns>
		public bool Equals( Ray other )
		{
			return Position.Equals( ref other.Position ) && Direction.Equals( ref other.Direction );
		}


		/// <summary>Returns a value indicating whether this <see cref="Ray"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Ray"/> which equals this <see cref="Ray"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Ray ) && this.Equals( (Ray)obj );
		}


		/// <summary>Returns a string representing this <see cref="Ray"/>.</summary>
		/// <returns>Returns a string representing this <see cref="Ray"/>.</returns>
		public override string ToString()
		{
			return "{Position: " + Position.ToString() + ", Direction: " + Direction.ToString() + "}";
		}



		/// <summary>The «zero» (and invalid) <see cref="Ray"/>.</summary>
		public static readonly Ray Zero;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="ray">A <see cref="Ray"/>.</param>
		/// <param name="other">A <see cref="Ray"/>.</param>
		/// <returns>Returns true if the specified rays are equal, otherwise returns false.</returns>
		public static bool operator ==( Ray ray, Ray other )
		{
			return ray.Position.Equals( ref other.Position ) && ray.Direction.Equals( ref other.Direction );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="ray">A <see cref="Ray"/>.</param>
		/// <param name="other">A <see cref="Ray"/>.</param>
		/// <returns>Returns true if the specified rays are not equal, otherwise returns false.</returns>
		public static bool operator !=( Ray ray, Ray other )
		{
			return !( ray.Position.Equals( ref other.Position ) && ray.Direction.Equals( ref other.Direction ) );
		}

		#endregion Operators

	}

}